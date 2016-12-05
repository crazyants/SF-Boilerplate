using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using SF.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using SF.Web.Captcha.Contracts;
using SF.Web.Captcha.Providers;

namespace SF.Web.Captcha
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
            services.TryAddSingleton<ICaptchaStorageProvider, CookieCaptchaStorageProvider>();
            services.TryAddSingleton<IHumanReadableIntegerProvider, DynamicHumanReadableIntegerProvider>();
            services.TryAddSingleton<IRandomNumberProvider, RandomNumberProvider>();
            services.TryAddSingleton<ICaptchaImageProvider, DynamicCaptchaImageProvider>();
            services.TryAddSingleton<ICaptchaProtectionProvider, CaptchaProtectionProvider>();
        }

 
        #endregion
        #region MyRegion IApplicationBuilder


        

        private void UseMvc(IApplicationBuilder applicationBuilder)
        {
            

        }


        #endregion

     
    }
}