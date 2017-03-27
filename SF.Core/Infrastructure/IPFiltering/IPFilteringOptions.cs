
namespace SF.Core.Infrastructure.IPFiltering
{
    #region Usings

    using System.Collections.Generic;
    using System.Net;

    #endregion

    /// <summary>
    /// The IP filtering options.
    /// </summary>
    public class IPFilteringOptions
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the blacklist.
        /// </summary>
        public IList<string> Blacklist { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the default block level.
        /// </summary>
        public DefaultBlockLevel DefaultBlockLevel { get; set; } = DefaultBlockLevel.All;

        /// <summary>
        /// Gets or sets the http status code.
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.NotFound;

        /// <summary>
        /// Gets or sets the whitelist.
        /// </summary>
        public IList<string> Whitelist { get; set; } = new List<string>();

        #endregion
    }
}