
namespace SF.Core.Infrastructure.IPFiltering.DependencyInjection
{
    #region Usings

    using System;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    using SF.Core.Infrastructure.IPFiltering.Internal;

    #endregion

    /// <summary>
    /// The service collection extensions.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        #region Public Methods

        /// <summary>
        /// Adds IP filtering services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">
        /// The <see cref="IServiceCollection"/> to add services to.
        /// </param>
        /// <returns>
        /// The <see cref="IServiceCollection"/> so that additional calls can be chained.
        /// </returns>
        public static IServiceCollection AddIPFiltering(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddOptions();

            services.TryAddSingleton<IIPAddressFinder, IPAddressFinder>();
            services.TryAddSingleton<IIPAddressChecker, IPAddressChecker>();

            return services;
        }

        /// <summary>
        /// Adds IP filtering services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">
        /// The <see cref="IServiceCollection"/> to add services to.
        /// </param>
        /// <param name="configurationSection">
        /// The configuration file section.
        /// </param>
        /// <returns>
        /// The <see cref="IServiceCollection"/> so that additional calls can be chained.
        /// </returns>
        public static IServiceCollection AddIPFiltering(this IServiceCollection services, IConfiguration configurationSection)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configurationSection == null)
            {
                throw new ArgumentNullException(nameof(configurationSection));
            }

            services.AddOptions();

            services.Configure<IPFilteringOptions>(configurationSection);

            services.TryAddSingleton<IIPAddressFinder, IPAddressFinder>();
            services.TryAddSingleton<IIPAddressChecker, IPAddressChecker>();

            return services;
        }

        #endregion
    }
}