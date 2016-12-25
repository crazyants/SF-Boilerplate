using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using SF.Core.Entitys;
using SF.Web.Security.Converters;

namespace SF.Web.Security
{
    /// <summary>
    /// This authorization handler ensures that implied permissions are checked.
    /// </summary>
    public class RolesPermissionsHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly UserManager<UserEntity> _userManagerFactory;
        private readonly RoleManager<RoleEntity> _roleManager;
        private readonly IPermissionScopeService _permissionScopeService;

        public RolesPermissionsHandler(UserManager<UserEntity> userManagerFactory,
                RoleManager<RoleEntity> roleManager,
              IPermissionScopeService permissionScopeService)
        {
            _userManagerFactory = userManagerFactory;
            _roleManager = roleManager;
            _permissionScopeService = permissionScopeService;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.HasSucceeded)
            {
                // This handler is not revoking any pre-existing grants.
                return;
            }

            // Determine which set of permissions would satisfy the access check
            var grantingNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            PermissionNames(requirement.Permission, grantingNames);

            // Determine what set of roles should be examined by the access check
            var rolesToExamine = new List<string> { "Anonymous" };

            if (context.User.Identity.IsAuthenticated)
            {
                rolesToExamine.Add("Authenticated");
                // Add roles from the user
                foreach (var claim in context.User.Claims)
                {
                    if (claim.Type == ClaimTypes.Role)
                    {
                        rolesToExamine.Add(claim.Value);
                    }
                }
            }

            foreach (var roleName in rolesToExamine)
            {
                var role = await _roleManager.FindByNameAsync(roleName);

                if (role != null)
                {
                    var permissions = role.RolePermissions.Select(rp => rp.ToCoreModel(_permissionScopeService)).ToArray();
                    // var permissions = role.RolePermissions.Where(x => x.ClaimType == Permission.ClaimType);

                    foreach (var permission in permissions)
                    {
                        string permissionName = permission.Name;

                        if (grantingNames.Contains(permissionName))
                        {
                            context.Succeed(requirement);
                            return;
                        }
                    }
                }
            }
        }

        private static void PermissionNames(Permission permission, HashSet<string> stack)
        {
            // The given name is tested
            stack.Add(permission.Name);

            //// Iterate implied permissions to grant, it present
            //if (dbUser.Roles != null && dbUser.Roles.Any())
            //{
            //    foreach (var impliedBy in permission.ImpliedBy)
            //    {
            //        // Avoid potential recursion
            //        if (stack.Contains(impliedBy.Name))
            //        {
            //            continue;
            //        }

            //        // Otherwise accumulate the implied permission names recursively
            //        PermissionNames(impliedBy, stack);
            //    }
            //}
        }
    }
}
