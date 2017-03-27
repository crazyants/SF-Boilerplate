
namespace SF.Core.Infrastructure.IPFiltering.Internal
{
    #region Usings

    using System.Net;

    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    #endregion

    /// <summary>
    /// The IP address finder.
    /// </summary>
    public class IPAddressFinder : IIPAddressFinder
    {
        #region Constants

        /// <summary>
        /// The X-Forwarded-For header.
        /// </summary>
        private const string ForwardedFor = "X-Forwarded-For";

        /// <summary>
        /// The X-Real-IP header.
        /// </summary>
        private const string RealIP = "X-Real-IP";

        #endregion

        #region Fields

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger logger;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="IPAddressFinder"/> class.
        /// </summary>
        /// <param name="loggerFactory">
        /// The logger factory.
        /// </param>
        public IPAddressFinder(ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<IPAddressFinder>();
        }

        #endregion

        #region Implemented Interfaces

        #region IIPAddressFinder

        /// <inheritdoc />
        public virtual IPAddress Find(HttpContext context)
        {
            string realIpHeader = context.Request.Headers[RealIP];
            IPAddress ipAddress = null;
            if (!string.IsNullOrWhiteSpace(realIpHeader))
            {
                ipAddress = IPAddressParser.Parse(realIpHeader);
            }

            if (ipAddress != null)
            {
                this.logger.LogDebug("Found IP: {ip} in {header}.", ipAddress, RealIP);
                return ipAddress;
            }

            var forwardedForHeader = context.Request.Headers.GetCommaSeparatedValues(ForwardedFor);

            if ((forwardedForHeader != null) && (forwardedForHeader.Length > 0))
            {
                // first address in X-Forwarded-For header is original one.
                // X-Forwarded-For: client, proxy1, proxy2
                ipAddress = IPAddressParser.Parse(forwardedForHeader[0]);

                if (ipAddress != null)
                {
                    this.logger.LogDebug("Found IP: {ip} in {header}.", ipAddress, ForwardedFor);
                    return ipAddress;
                }
            }

            ipAddress = context.Connection.RemoteIpAddress;

            this.logger.LogDebug("Found IP: {ip}.", ipAddress);
            return ipAddress;
        }

        #endregion

        #endregion
    }
}