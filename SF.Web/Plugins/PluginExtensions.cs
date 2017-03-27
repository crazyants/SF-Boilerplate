using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SF.Core.Abstraction.Data;
using SF.Core.Infrastructure.Plugins.Abstraction;
using SF.Core.Infrastructure.Plugins.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SF.Core.Abstraction;
using SF.Web.Plugins.Data;
using SF.Data;
using SF.Core.Interceptors;
using SF.Core.Abstraction.UoW.Helper;
using SF.Core.Infrastructure.Plugins;

namespace SF.Web.Plugins
{
    public static class PluginExtensions/* : ModuleInitializerBase*/
    {
        //public override IEnumerable<KeyValuePair<int, Action<IServiceCollection>>> ConfigureServicesActionsByPriorities
        //{
        //    get
        //    {
        //        return new Dictionary<int, Action<IServiceCollection>>()
        //        {
        //            [9999] = this.AddPlugins,
        //            [10000] = this.AddPluginManager
        //        };
        //    }
        //}

        //public override IEnumerable<KeyValuePair<int, Action<IApplicationBuilder>>> ConfigureActionsByPriorities
        //{
        //    get
        //    {
        //        return new Dictionary<int, Action<IApplicationBuilder>>()
        //        {
        //            [9999] = this.UseMvcWithPlugin,
        //            [10000] = this.AddPluginCustomizedMvc
        //        };
        //    }
        //}


        /// <summary>
        /// 插件路由配置
        /// </summary>
        /// <param name="app"></param>
        /// <param name="target">目标路由</param>
        /// <param name="configureRoutes"></param>
        /// <returns></returns>
        public static void UseMvcWithPlugin(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMvc(
              routeBuilder =>
              {
                  var routes = new PluginRouteBuilder(applicationBuilder, routeBuilder.DefaultHandler)
                  {
                      DefaultHandler = applicationBuilder.ApplicationServices.GetRequiredService<MvcRouteHandler>(),
                      PluginManager = applicationBuilder.ApplicationServices.GetRequiredService<IPluginManager>(),
                  };
                  routes.Build();
              }
            );



        }
        /// <summary>
        ///  插件控制器注册
        /// </summary>
        /// <param name="app"></param>
        public static void AddPluginCustomizedMvc(this IApplicationBuilder applicationBuilder)
        {
            var pluginManagers = applicationBuilder.ApplicationServices.GetService<IPluginManager>();

            var pluginAssemblies = pluginManagers.ActiveAssemblies;

            foreach (var plugin in pluginManagers.ActivePluginAssemblies)
            {
                pluginManagers.ActivatePlugin(plugin.Item1);

            }

        }
        /// <summary>
        /// 插件基本服务注册
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static void AddPlugins(this IServiceCollection services)
        {

            services.AddTransient<IPluginSettingsManager, PluginSettingsManager>();
            services.AddTransient<IAssemblyManager, PluginAssemblyManager>();
            services.AddTransient<IRouteBuilder, PluginRouteBuilder>();
            services.AddSingleton<IActionDescriptorCollectionProvider, PluginActionDescriptorCollectionProvider>();

            services.Configure<RazorViewEngineOptions>(options => { options.ViewLocationExpanders.Add(new PluginViewLocationExpander()); });
            services.AddSingleton<IPluginsUnitOfWork, PluginsUnitOfWork>();
         
        }
        /// <summary>
        /// 插件管理服务注册
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configRoot"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public static void AddPluginManager(this IServiceCollection services, IConfigurationRoot configurationRoot, IHostingEnvironment hostingEnvironment)
        {

            services.AddSingleton<IPluginManager, PluginManager>(srcProvider =>
            {
                var assbly = srcProvider.GetRequiredService<IAssemblyManager>();
                var uow = srcProvider.GetRequiredService<IPluginsUnitOfWork>();
                var appMgr = srcProvider.GetRequiredService<ApplicationPartManager>();

                return new PluginManager(assbly,  configurationRoot,  hostingEnvironment, uow, appMgr);
            });

        }
        /// <summary>
        /// 测试插件数据
        /// </summary>
        /// <param name="services"></param>
        public static void RunTestData(this  IServiceProvider services)
        {
            var unitOfWork = services.GetService<IPluginsUnitOfWork>();
            unitOfWork.ExecuteAndCommit(uow =>
          {
              uow.Plugin.Add(new InstalledPlugin
              {
                  Active = false,
                  DateInstalled = DateTime.UtcNow,
                  Installed = true,
                  PluginAssemblyName = "SF.Plugin.Blog",
                  PluginName = "BlogPlugin",
                  PluginVersion = "1.0"
              });
          });

        }
    }
}
