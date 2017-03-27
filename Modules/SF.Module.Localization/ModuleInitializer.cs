
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using SF.Core.Abstraction;
using SF.Core;
using SF.Module.Localization.Data;
using SF.Data;
using SF.Core.Interceptors;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SF.Web.Modules;
using SF.Core.Infrastructure.Modules;

namespace SF.Module.Localization
{
    public class ModuleInitializer : ModuleInitializerBase, IModuleInitializer
    {
        public override IEnumerable<KeyValuePair<int, Action<IServiceCollection>>> ConfigureServicesActionsByPriorities
        {
            get
            {
                return new Dictionary<int, Action<IServiceCollection>>()
                {
                    [10000] = this.AddLocalizationService
                };
            }
        }
        public void AddLocalizationService(IServiceCollection services)
        {
            services.AddTransient<IResourceUnitOfWork, ResourceUnitOfWork>();

            var globalFirst = this.configurationRoot.GetSection("GlobalResourceOptions").GetValue<bool>("TryGlobalFirst");

            if (!globalFirst)
            {
                //设置资源来源EF
                services.TryAddScoped<IStringLocalizerFactory, EfStringLocalizerFactory>();
            }
            else
            {
                services.Configure<GlobalResourceOptions>(Options =>
                {
                    Options.TryGlobalFirst = globalFirst;
                });
                services.TryAddScoped<IStringLocalizerFactory, GlobalResourceManagerStringLocalizerFactory>();

                services.AddLocalization(options => options.ResourcesPath = "GlobalResources");
            }

        }


    }
}
