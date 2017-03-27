
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using SF.Core;
using SF.Module.ActivityLog.Data;
using SF.Web.Modules;
using SF.Core.Infrastructure.Modules;

namespace SF.Module.ActivityLog
{
    public class ModuleInitializer : ModuleInitializerBase, IModuleInitializer
    {
        public override IEnumerable<KeyValuePair<int, Action<IServiceCollection>>> ConfigureServicesActionsByPriorities
        {
            get
            {
                return new Dictionary<int, Action<IServiceCollection>>()
                {
                    [10000] = this.AddActivityService
                };
            }
        }
        public void AddActivityService(IServiceCollection services)
        {
            services.AddTransient<IActivityUnitOfWork, ActivityUnitOfWork>();


        }


    }
}
