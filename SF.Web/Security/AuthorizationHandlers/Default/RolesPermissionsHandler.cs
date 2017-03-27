using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using SF.Entitys;
using SF.Web.Security.Converters;
using SF.Web.Security.Extensions;

namespace SF.Web.Security
{
    /// <summary>
    /// This authorization handler ensures that implied permissions are checked.
    /// </summary>
    public class RolesPermissionsHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly UserManager<UserEntity> _userManagerFactory;
        private readonly RoleManager<RoleEntity> _roleManager;
        private readonly ISecurityService _securityService;

        public RolesPermissionsHandler(UserManager<UserEntity> userManagerFactory,
                RoleManager<RoleEntity> roleManager,
                ISecurityService securityService)
        {
            _userManagerFactory = userManagerFactory;
            _roleManager = roleManager;
            _securityService = securityService;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.HasSucceeded)
            {
                // This handler is not revoking any pre-existing grants.
                return;
            }
            var user = await _securityService.GetCurrentUser(UserDetails.Full);

            // Determine which set of permissions would satisfy the access check
            var grantingNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            PermissionNames(requirement.Permission, grantingNames);

            user.Authorize(false, grantingNames.ToArray());

            context.Succeed(requirement);
            
            
        }

        private static void PermissionNames(Permission permission, HashSet<string> stack)
        {
            // The given name is tested
            stack.Add(permission.Name);
        }
    }
}
