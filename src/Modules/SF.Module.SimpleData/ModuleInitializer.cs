
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using SF.Core.Abstraction;
using SF.Core;
using SF.Core.Data;
using SF.Core.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace SF.Module.SimpleData
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
