using SimpleFramework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using SimpleFramework.Web.SiteMap;
using SimpleFramework.Module.Navigation.SiteMap;
using SimpleFramework.Infrastructure.UI;
using Microsoft.AspNetCore.Builder;

namespace SimpleFramework.Module.Navigation
{
    public class ModuleInitializer : ModuleInitializerBase, IModuleInitializer
    {


        public override IEnumerable<KeyValuePair<int, Action<IServiceCollection>>> ConfigureServicesActionsByPriorities
        {
            get
            {
                return new Dictionary<int, Action<IServiceCollection>>()
                {
                    [0] = this.AddNavigationService
                };
            }
        }

        public override IEnumerable<KeyValuePair<int, Action<IApplicationBuilder>>> ConfigureActionsByPriorities
        {
            get
            {
                return new Dictionary<int, Action<IApplicationBuilder>>()
                {
                    [0] = this.UseNavigation
                };
            }
        }


        private void AddNavigationService(IServiceCollection services)
        {
            services.AddScoped<ISiteMapNodeService, NavigationTreeSiteMapNodeService>();
            services.AddCloudscribeNavigation();

            services.Configure<MvcOptions>(options =>
            {

                options.CacheProfiles.Add("SiteMapCacheProfile",
                     new CacheProfile
                     {
                         Duration = 100
                     });
            });
        }
        private void UseNavigation(IApplicationBuilder applicationBuilder)
        {

        }
    }
}
