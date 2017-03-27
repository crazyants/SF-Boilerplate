
namespace SF.Core.Infrastructure.IPFiltering
{
    #region Usings

    using System;
    using System.Net;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    #endregion

    /// <summary>
    /// The IP filtering middleware.
    /// </summary>
    public class IPFilteringMiddleware
    {
        #region Fields

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// The next.
        /// </summary>
        private readonly RequestDelegate next;

        /// <summary>
        /// The options.
        /// </summary>
        private readonly IPFilteringOptions options;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="IPFilteringMiddleware"/> class.
        /// </summary>
        /// <param name="next">
        /// The <see cref="RequestDelegate"/> representing the next middleware in the pipeline.
        /// </param>
        /// <param name="loggerFactory">
        /// The logger factory.
        /// </param>
        /// <param name="options">
        /// The <see cref="IPFilteringOptions"/> representing the options for the <see cref="IPFilteringMiddleware"/>.
        /// </param>
        public IPFilteringMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IOptionsSnapshot<IPFilteringOptions> options)
        {
            this.next = next;
            this.logger = loggerFactory.CreateLogger<IPFilteringMiddleware>();
            this.options = options.Value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IPFilteringMiddleware"/> class.
        /// </summary>
        /// <param name="next">
        /// The <see cref="RequestDelegate"/> representing the next middleware in the pipeline.
        /// </param>
        /// <param name="loggerFactory">
        /// The logger factory.
        /// </param>
        /// <param name="options">
        /// The <see cref="IPFilteringOptions"/> representing the options for the <see cref="IPFilteringMiddleware"/>.
        /// </param>
        public IPFilteringMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IOptions<IPFilteringOptions> options)
        {
            this.next = next;
            this.logger = loggerFactory.CreateLogger<IPFilteringMiddleware>();
            this.options = options.Value;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Invokes middleware.
        /// </summary>
        /// <param name="context">
        /// The <see cref="HttpContext"/> context.
        /// </param>
        public async Task Invoke(HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var finder = context.RequestServices.GetRequiredService<IIPAddressFinder>();
            var checker = context.RequestServices.GetRequiredService<IIPAddressChecker>();

            IPAddress ipAddress = finder.Find(context);

            this.logger.LogDebug("Checking if IP: {ipAddress} address is allowed.", ipAddress);
            if (checker.IsAllowed(ipAddress, this.options))
            {
                this.logger.LogDebug("IP is allowed for further process.");
                await this.next.Invoke(context);
            }
            else
            {
                this.logger.LogInformation(1, "The IP Address: {BlokedIp} was blocked.", ipAddress);
                context.Response.StatusCode = (int)this.options.HttpStatusCode;
            }
        }

        #endregion
    }
}