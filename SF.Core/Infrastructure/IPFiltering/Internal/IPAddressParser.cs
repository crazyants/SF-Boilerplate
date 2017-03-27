
namespace SF.Core.Infrastructure.IPFiltering.Internal
{
    #region Usings

    using System;
    using System.Net;

    #endregion

    /// <summary>
    /// The IP address parser.
    /// </summary>
    public static class IPAddressParser
    {
        #region Public Methods

        /// <summary>
        /// Parses address to exclude port number if any.
        /// </summary>
        /// <param name="address">
        /// The IP address.
        /// </param>
        /// <returns>
        /// Returns parsed <see cref="IPAddress"/> if address is in proper format, otherwise <see langword="null"/>.
        /// </returns>
        public static IPAddress Parse(string address)
        {
            IPAddress ipAddress;

            if (string.IsNullOrWhiteSpace(address))
            {
                return null;
            }

            // headers can contains port so we need to remove port number from ip address if any
            address = address.Trim();

            int end;

            // check ipv6 with possible port
            if (address.StartsWith("[", StringComparison.Ordinal))
            {
                end = address.IndexOf("]", StringComparison.Ordinal);
                return IPAddress.TryParse(address.Substring(0, end + 1), out ipAddress) ? ipAddress : null;
            }

            // only one : so ipv4 with port
            if (((end = address.IndexOf(":", StringComparison.Ordinal)) == address.LastIndexOf(":", StringComparison.Ordinal)) && (end >= 0))
            {
                return IPAddress.TryParse(address.Substring(0, end), out ipAddress) ? ipAddress : null;
            }

            // ipv4 or ipv6
            return IPAddress.TryParse(address, out ipAddress) ? ipAddress : null;
        }

        #endregion
    }
}