
namespace SF.Core.Infrastructure.IPFiltering
{
    #region Usings

    using System.Net;

    #endregion

    /// <summary>
    /// The IPAddressChecker interface.
    /// </summary>
    public interface IIPAddressChecker
    {
        #region Public Methods

        /// <summary>
        /// Checks if IP address is allowed.
        /// </summary>
        /// <param name="ipAddress">
        /// The IP address.
        /// </param>
        /// <param name="options">
        /// The IP filtering options.
        /// </param>
        /// <returns>
        /// Returns <see langword="true"/> if IP address is allowed, otherwise <see langword="false"/>.
        /// </returns>
        bool IsAllowed(IPAddress ipAddress, IPFilteringOptions options);

        #endregion
    }
}