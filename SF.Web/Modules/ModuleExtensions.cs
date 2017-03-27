using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using SF.Core.Abstraction.Domain;
using SF.Core.Abstraction.GenericServices;
using SF.Core.Infrastructure.Modules.Builder;
using SF.Web.Base.DataContractMapper;
using SF.Web.Modules.Data;

namespace SF.Web.Modules
{
    public static class ModuleExtensions
    {
        /// <summary>
        ///  插件控制器注册
        /// </summary>
        /// <param name="app"></param>
        public static void AddmoduleCustomizedMvc(this IApplicationBuilder applicationBuilder, IConfigurationRoot configurationRoot, IHostingEnvironment hostingEnvironment)
        {
            string extensionsPath = configurationRoot["Modules:Path"];
            var moduleManagers = applicationBuilder.ApplicationServices.GetService<IModuleManager>();

            foreach (var modlule in moduleManagers.GetModule())
            {
                moduleManagers.InstallModule(modlule);
            }

        }
        /// <summary>
        /// 插件基本服务注册
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static void AddModules(this IServiceCollection services)
        {

            services.Configure<RazorViewEngineOptions>(options =>
            {

            });
            services.AddSingleton<IModulesUnitOfWork, ModulesUnitOfWork>();
            services.AddSingleton<IModuleManager, ModuleManager>();
            services.AddScoped<IModuleConfigBuilder, JsonModuleBuilder>();

        }

    }
}
