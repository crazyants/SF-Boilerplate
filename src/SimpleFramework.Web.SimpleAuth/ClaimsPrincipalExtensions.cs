

using System;
using System.Security.Claims;

namespace SimpleFramework.Web.SimpleAuth.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetDisplayName(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }
            var claim = principal.FindFirst("DisplayName");
            return claim != null ? claim.Value : null;
        }

        public static string GetEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }
            var claim = principal.FindFirst(ClaimTypes.Email);
            return claim != null ? claim.Value : null;
        }
    }
}
