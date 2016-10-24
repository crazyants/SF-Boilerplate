using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleFramework.Core.UI;
using System;
using System.Collections.Generic;

namespace SimpleFramework.Core
{
    public interface IModuleInitializer
    {
        //void Init(IServiceCollection services, IConfigurationRoot configuration);

        IEnumerable<KeyValuePair<int, Action<IServiceCollection>>> ConfigureServicesActionsByPriorities { get; }

        IEnumerable<KeyValuePair<int, Action<IApplicationBuilder>>> ConfigureActionsByPriorities { get; }

        void SetServiceProvider(IServiceProvider serviceProvider);
        void SetConfigurationRoot(IConfigurationRoot configurationRoot);


        IEnumerable<KeyValuePair<int, Action<IMvcBuilder>>> AddMvcActionsByPriorities { get; }

        IEnumerable<KeyValuePair<int, Action<IRouteBuilder>>> UseMvcActionsByPriorities { get; }


        IBackendMetadata BackendMetadata { get; }
    }
}
