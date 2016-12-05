using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace SF.Core.RateLimit
{
    public class CoreExtension : ModuleInitializerBase
    {
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

        #region MyRegion IServiceCollection
        
        /// <summary>
        /// 添加全局服务注册
        /// </summary>
        /// <param name="services"></param>
        public void AddCoreServices(IServiceCollection services)
        {
            
            //configure ip rate limiting middle-ware
            services.Configure<IpRateLimitOptions>(configurationRoot.GetSection("IpRateLimiting"));
            services.Configure<IpRateLimitPolicies>(configurationRoot.GetSection("IpRateLimitPolicies"));
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();

            //configure client rate limiting middleware
            services.Configure<ClientRateLimitOptions>(configurationRoot.GetSection("ClientRateLimiting"));
            services.Configure<ClientRateLimitPolicies>(configurationRoot.GetSection("ClientRateLimitPolicies"));
            services.AddSingleton<IClientPolicyStore, MemoryCacheClientPolicyStore>();

            var opt = new ClientRateLimitOptions();
            ConfigurationBinder.Bind(configurationRoot.GetSection("ClientRateLimiting"), opt);
        }

 
        #endregion
        #region MyRegion IApplicationBuilder


        

        private void UseMvc(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseIpRateLimiting();
            applicationBuilder.UseClientRateLimiting();

        }


        #endregion

     
    }
}