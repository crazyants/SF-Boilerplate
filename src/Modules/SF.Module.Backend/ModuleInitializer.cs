
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Scrutor;
using SF.Core;
using SF.Core.Abstraction.Domain;
using SF.Core.Abstraction.UI.Backends;
using SF.Module.Backend.Domain.DataItem.Rule;
using SF.Module.Backend.Domain.DataItem.Service;
using SF.Module.Backend.Services;
using SF.Module.Backend.Services.Implementation;
using SF.Web.Common.Base.DataContractMapper;
using SF.Web.Security;
using System;
using System.Collections.Generic;

namespace SF.Module.Backend
{
    public class ModuleInitializer : ModuleInitializerBase, IModuleInitializer
    {
        public override IBackendMetadata BackendMetadata
        {
            get
            {
                return new BackendMetadata();
            }
        }
        public override IEnumerable<KeyValuePair<int, Action<IServiceCollection>>> ConfigureServicesActionsByPriorities
        {
            get
            {
                return new Dictionary<int, Action<IServiceCollection>>()
                {
                    [10000] = this.AddCoreServices
                };
            }
        }

        public override IEnumerable<KeyValuePair<int, Action<IApplicationBuilder>>> ConfigureActionsByPriorities
        {
            get
            {
                return new Dictionary<int, Action<IApplicationBuilder>>()
                {
                    [10000] = this.UseMvc,
                };
            }
        }

        /// <summary>
        /// 全局服务注册
        /// </summary>
        /// <param name="services"></param>
        private void AddCoreServices(IServiceCollection services)
        {
            services.TryAddScoped<IMediaService, LocalMediaService>();
            services.TryAddScoped<IUrlSlugService, UrlSlugService>();
            services.TryAddScoped<IDataItemService, DataItemService>();
            services.AddSingleton<IPermissionProvider, BackendPermissionProvider>();
            
        }
        /// <summary>
        /// 全局构建
        /// </summary>
        /// <param name="applicationBuilder"></param>
        private void UseMvc(IApplicationBuilder applicationBuilder)
        {

        }

    }
}
