
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using SimpleFramework.Core.Abstraction;
using SimpleFramework.Core;
using SimpleFramework.Core.Data;
using SimpleFramework.Core.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace SimpleFramework.Module.SimpleData
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
           
            services.AddEntityFrameworkSqlServer()
               .AddDbContext<UnicornsContext>((serviceProvider, options) =>
               {
                   options.UseSqlServer("")
                          .UseInternalServiceProvider(serviceProvider);

               });


        }


    }
}
