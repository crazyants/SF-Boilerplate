
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using SimpleFramework.Core.Abstraction;
using SimpleFramework.Core;

namespace SimpleFramework.Module.Localization
{
    public class ModuleInitializer : ModuleInitializerBase, IModuleInitializer
    {
        public override IEnumerable<KeyValuePair<int, Action<IServiceCollection>>> ConfigureServicesActionsByPriorities
        {
            get
            {
                return new Dictionary<int, Action<IServiceCollection>>()
                {
                    [0] = this.AddLocalizationService
                };
            }
        }
        public void AddLocalizationService(IServiceCollection services)
        {
            var globalFirst = this.configurationRoot.GetSection("GlobalResourceOptions").GetValue<bool>("TryGlobalFirst");

            if (!globalFirst)
            {
                //设置资源来源EF
                services.AddSingleton<IStringLocalizerFactory, EfStringLocalizerFactory>();
            }
            else
            {
                services.Configure<GlobalResourceOptions>(Options =>
                {
                    Options.TryGlobalFirst = globalFirst;
                });
                services.AddSingleton<IStringLocalizerFactory, GlobalResourceManagerStringLocalizerFactory>();

                services.AddLocalization(options => options.ResourcesPath = "GlobalResources");
            }

        }


    }
}
