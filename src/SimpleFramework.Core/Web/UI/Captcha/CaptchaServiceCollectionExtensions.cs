using SimpleFramework.Core.UI.Captcha.Contracts;
using SimpleFramework.Core.UI.Captcha.Providers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace SimpleFramework.Core.UI.Captcha
{
    /// <summary>
    ///  Captcha ServiceCollection Extensions
    /// </summary>
    public static class CaptchaServiceCollectionExtensions
    {
        /// <summary>
        /// Adds default DNTCaptcha providers.
        /// </summary>
        public static void AddDNTCaptcha(this IServiceCollection services)
        {

            services.TryAddSingleton<ICaptchaStorageProvider, CookieCaptchaStorageProvider>();
            services.TryAddSingleton<IHumanReadableIntegerProvider, DynamicHumanReadableIntegerProvider>();
            services.TryAddSingleton<IRandomNumberProvider, RandomNumberProvider>();
            services.TryAddSingleton<ICaptchaImageProvider, DynamicCaptchaImageProvider>();
            services.TryAddSingleton<ICaptchaProtectionProvider, CaptchaProtectionProvider>();
        }
    }
}