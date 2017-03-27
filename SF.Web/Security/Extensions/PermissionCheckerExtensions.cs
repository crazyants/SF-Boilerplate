using SF.Core.Errors.Exceptions;
using SF.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SF.Web.Security.Extensions
{
    public static class PermissionCheckerExtensions
    {
        /// <summary>
        /// Authorizes current user for given permission or permissions,
        /// throws <see cref="AbpAuthorizationException"/> if not authorized.
        /// </summary>
        /// <param name="permissionChecker">Permission checker</param>
        /// <param name="requireAll">
        /// If this is set to true, all of the <see cref="permissionNames"/> must be granted.
        /// If it's false, at least one of the <see cref="permissionNames"/> must be granted.
        /// </param>
        /// <param name="permissionNames">Name of the permissions to authorize</param>
        /// <exception cref="AbpAuthorizationException">Throws authorization exception if</exception>
        public static void Authorize(this ApplicationUserExtended permissionChecker, bool requireAll, params string[] permissionNames)
        {
            if (IsGranted(permissionChecker, requireAll, permissionNames))
            {
                return;
            }
            if (requireAll)
            {
                throw new UnauthorizedAccessException("没有权限访问，必须所有这些权限都被授权。");
            }
            else
            {
                throw new UnauthorizedAccessException("没有权限访问，至少有一个必须被授予这些权限。");

            }
        }
        /// <summary>
        /// Checks if current user is granted for given permission.
        /// </summary>
        /// <param name="permissionChecker">Permission checker</param>
        /// <param name="requiresAll">True, to require all given permissions are granted. False, to require one or more.</param>
        /// <param name="permissionNames">Name of the permissions</param>
        public static bool IsGranted(this ApplicationUserExtended permissionChecker, bool requiresAll, params string[] permissionNames)
        {
            if (permissionNames.IsNullOrEmpty())
            {
                return true;
            }

            if (requiresAll)
            {
                foreach (var permissionName in permissionNames)
                {
                    foreach (var permission1 in permissionChecker.Permissions)
                        if (permission1.Equals(permissionName, StringComparison.CurrentCultureIgnoreCase))
                            return true;

                    return false;
                }

                return true;
            }
            else
            {
                foreach (var permissionName in permissionNames)
                {
                    foreach (var permission1 in permissionChecker.Permissions)
                        if (permission1.Equals(permissionName, StringComparison.CurrentCultureIgnoreCase))
                            return true;

                }

                return false;
            }
        }
    }
}
