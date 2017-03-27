using Microsoft.AspNetCore.Http;
using SF.Core.Common;
using SF.Entitys;
using SF.Web.Security.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SF.Web.Security.AuthorizationHandlers.Custom
{
    public class AuthorizationHelper : IAuthorizationHelper
    {
        private readonly ISiteContext _siteContext;
        private readonly ISecurityService _securityService;
        private readonly IHttpContextAccessor _contextAccessor;
        public AuthorizationHelper(SiteContext currentSite, ISecurityService securityService, IHttpContextAccessor contextAccessor)
        {
            _siteContext = currentSite;
            _contextAccessor = contextAccessor;
            _securityService = securityService;
        }
        public async Task AuthorizeAsync(IEnumerable<ISFAuthorizeAttribute> authorizeAttributes)
        {
            if (!_siteContext.EnabledAuthorization)
            {
                return;
            }
            var user = await _securityService.GetCurrentUser(UserDetails.Full);

            foreach (var authorizeAttribute in authorizeAttributes)
            {

                user.Authorize(authorizeAttribute.RequireAllPermissions, authorizeAttribute.Permissions);
            }
        }

        public async Task AuthorizeAsync(MethodInfo methodInfo)
        {
            if (!_siteContext.EnabledAuthorization)
            {
                return;
            }

            if (AllowAnonymous(methodInfo))
            {
                return;
            }

            //Authorize
            await CheckPermissions(methodInfo);
        }
        private async Task CheckPermissions(MethodInfo methodInfo)
        {
            var authorizeAttributes =
                ReflectionUtility.GetAttributesOfMemberAndDeclaringType(
                    methodInfo
                ).OfType<ISFAuthorizeAttribute>().ToArray();

            if (!authorizeAttributes.Any())
            {
                return;
            }

            await AuthorizeAsync(authorizeAttributes);
        }
        private static bool AllowAnonymous(MethodInfo methodInfo)
        {
            return ReflectionUtility.GetAttributesOfMemberAndDeclaringType(methodInfo)
                .OfType<ISFAllowAnonymousAttribute>().Any();
        }
    }
}
