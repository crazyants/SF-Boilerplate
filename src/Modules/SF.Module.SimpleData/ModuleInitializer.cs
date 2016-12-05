
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
using Microsoft.EntityFrameworkCore.Infrastructure;
using SF.Module.SimpleData.Data;

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
            services.AddDbContext<UnicornsContext>((serviceProvider, options) =>
               options.UseSqlServer("Server=.;Database=Unicorns_Base;uid=sa;pwd=wharton@168;Pooling=True;Min Pool Size=1;Max Pool Size=100;Trusted_Connection=True;MultipleActiveResultSets=true;Integrated Security=false;",
                    b => b.MigrationsAssembly("SF.WebHost"))
                      .UseInternalServiceProvider(serviceProvider));

            services.AddSingleton<ISimpleDataUnitOfWork, SimpleDataUnitOfWork>();
        }


    }
}
