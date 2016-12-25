using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SF.Web.Security.Filters
{
    /// <summary>
    /// Represents an attribute that forces an unsecured HTTP request to be re-sent over HTTPS and adds HSTS headers to secured requests.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class RequireHstsAttribute : RequireHttpsAttribute
    {
        #region Fields
        private readonly uint _maxAge;
        #endregion

        #region Constants
        private const string _strictTransportSecurityHeader = "Strict-Transport-Security";
        private const string _maxAgeDirectiveFormat = "max-age={0}";
        private const string _includeSubDomainsDirective = "; includeSubDomains";
        private const string _preloadDirective = "; preload";

        private const int _minimumPreloadMaxAge = 10886400;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the time (in seconds) that the browser should remember that this resource is only to be accessed using HTTPS.
        /// </summary>
        public uint MaxAge { get { return _maxAge; } }

        /// <summary>
        /// Gets or sets the value indicating if this rule applies to all subdomains as well.
        /// </summary>
        public bool IncludeSubDomains { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if subscription to HSTS preload list (https://hstspreload.appspot.com/) should be confirmed.
        /// </summary>
        public bool Preload { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the RequireHstsAttribute class.
        /// </summary>
        /// <param name="maxAge">The time (in seconds) that the browser should remember that this resource is only to be accessed using HTTPS.</param>
        public RequireHstsAttribute(uint maxAge)
            : base()
        {
            _maxAge = maxAge;
            IncludeSubDomains = false;
            Preload = false;
        }
        #endregion

        #region IAuthorizationFilter Members
        /// <summary>
        /// Determines whether a request is secured (HTTPS). If it is sets the Strict-Transport-Security header. If it is not calls the HandleNonHttpsRequest method.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException(nameof(filterContext));
            }

            if (filterContext.HttpContext.Request.IsHttps)
            {
                if (Preload && (MaxAge < _minimumPreloadMaxAge))
                {
                    throw new InvalidOperationException("In order to confirm HSTS preload list subscription expiry must be at least eighteen weeks (10886400 seconds).");
                }

                if (Preload && !IncludeSubDomains)
                {
                    throw new InvalidOperationException("In order to confirm HSTS preload list subscription subdomains must be included.");
                }

                StringBuilder headerBuilder = new StringBuilder();
                headerBuilder.AppendFormat(_maxAgeDirectiveFormat, _maxAge);

                if (IncludeSubDomains)
                {
                    headerBuilder.Append(_includeSubDomainsDirective);
                }

                if (Preload)
                {
                    headerBuilder.Append(_preloadDirective);
                }

                filterContext.HttpContext.Response.Headers.Add(_strictTransportSecurityHeader, headerBuilder.ToString());
            }
            else
            {
                HandleNonHttpsRequest(filterContext);
            }
        }
        #endregion
    }
}
