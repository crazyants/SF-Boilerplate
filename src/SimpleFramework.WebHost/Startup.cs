using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleFramework.Core.Abstraction;
using SimpleFramework.WebHost.Extensions;
using SimpleFramework.Core.Plugins;
using simpleGlobal = SimpleFramework.Core;
using SimpleFramework.Core.Web;
using System.Reflection;
using System.Linq;
using SimpleFramework.Core;
using SimpleFramework.Core.Common;

namespace SimpleFramework.WebHost
{
    public class Startup
    {
        protected IServiceProvider serviceProvider;
        private readonly IHostingEnvironment _hostingEnvironment;
        protected IAssemblyProvider assemblyProvider;
        protected ILogger<Startup> logger;

        public Startup(IHostingEnvironment env, IServiceProvider serviceProvider)
  : this(env, serviceProvider, new AssemblyProvider(serviceProvider))
        {
            this.logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Startup>();
        }

        public Startup(IHostingEnvironment env, IServiceProvider serviceProvider, IAssemblyProvider assemblyProvider)
        {
            this.serviceProvider = serviceProvider;
            this.assemblyProvider = assemblyProvider;
            this._hostingEnvironment = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);

            // you can use whatever file name you like and it is probably a good idea to use a custom file name
            // just an a small extra protection in case hackers try some kind of attack based on knowing the name of the file
            // it should not be possible for anyone to get files outside of wwwroot using http requests
            // but every little thing you can do for stronger security is a good idea
            builder.AddJsonFile("simpleauthsettings.json", optional: true);

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        private IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            this.DiscoverAssemblies();

            simpleGlobal.GlobalConfiguration.WebRootPath = _hostingEnvironment.WebRootPath;
            simpleGlobal.GlobalConfiguration.ContentRootPath = _hostingEnvironment.ContentRootPath;

            services.AddAuditStorageProviders(Configuration, _hostingEnvironment);
            services.AddCloudscribePagination();

            foreach (IModuleInitializer extension in ExtensionManager.Extensions)
            {
                extension.SetServiceProvider(this.serviceProvider);
                extension.SetConfigurationRoot(this.Configuration);
            }

            foreach (Action<IServiceCollection> prioritizedConfigureServicesAction in this.GetPrioritizedConfigureServicesActions())
            {
                this.logger.LogInformation("Executing prioritized ConfigureServices action '{0}' of {1}", this.GetActionMethodInfo(prioritizedConfigureServicesAction));
                prioritizedConfigureServicesAction(services);
            }

            services.Build(Configuration, _hostingEnvironment).BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //  loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            // loggerFactory.AddDebug();

            if (env.IsDevelopment() || env.IsEnvironment("Development"))
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            app.UseCustomizedRequestLocalization();
            app.UseCustomizedHangfire(Configuration);

            foreach (Action<IApplicationBuilder> prioritizedConfigureAction in this.GetPrioritizedConfigureActions())
            {
                this.logger.LogInformation("Executing prioritized Configure action '{0}' of {1}", this.GetActionMethodInfo(prioritizedConfigureAction));
                prioritizedConfigureAction(app);
            }

        }


        /// <summary>
        /// 加载安装的模块信息
        /// </summary>
        private void DiscoverAssemblies()
        {
            string extensionsPath = this.Configuration["Modules:Path"];
            IEnumerable<Assembly> assemblies = this.assemblyProvider.GetAssemblies(
              string.IsNullOrEmpty(extensionsPath) ?
                null : this.serviceProvider.GetService<IHostingEnvironment>().ContentRootPath
            );
            ExtensionManager.SetAssemblies(assemblies);

            IEnumerable<ModuleInfo> modules = this.assemblyProvider.GetModules(
              string.IsNullOrEmpty(extensionsPath) ?
                null : this.serviceProvider.GetService<IHostingEnvironment>().ContentRootPath + extensionsPath);
            ExtensionManager.SetModules(modules);
        }

        /// <summary>
        /// 获取模块服务注册方法集合
        /// </summary>
        /// <returns></returns>
        private Action<IServiceCollection>[] GetPrioritizedConfigureServicesActions()
        {
            List<KeyValuePair<int, Action<IServiceCollection>>> configureServicesActionsByPriorities = new List<KeyValuePair<int, Action<IServiceCollection>>>();

            foreach (IModuleInitializer extension in ExtensionManager.Extensions)
                if (extension.ConfigureServicesActionsByPriorities != null)
                    configureServicesActionsByPriorities.AddRange(extension.ConfigureServicesActionsByPriorities);

            return this.GetPrioritizedActions(configureServicesActionsByPriorities);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Action<IApplicationBuilder>[] GetPrioritizedConfigureActions()
        {
            List<KeyValuePair<int, Action<IApplicationBuilder>>> configureActionsByPriorities = new List<KeyValuePair<int, Action<IApplicationBuilder>>>();

            foreach (IModuleInitializer extension in ExtensionManager.Extensions)
                if (extension.ConfigureActionsByPriorities != null)
                    configureActionsByPriorities.AddRange(extension.ConfigureActionsByPriorities);

            return this.GetPrioritizedActions(configureActionsByPriorities);
        }

        private Action<T>[] GetPrioritizedActions<T>(IEnumerable<KeyValuePair<int, Action<T>>> actionsByPriorities)
        {
            return actionsByPriorities
              .OrderBy(actionByPriority => actionByPriority.Key)
              .Select(actionByPriority => actionByPriority.Value)
              .ToArray();
        }

        private string[] GetActionMethodInfo<T>(Action<T> action)
        {
            MethodInfo methodInfo = action.GetMethodInfo();

            return new string[] { methodInfo.Name, methodInfo.DeclaringType.FullName };
        }
    }
}