/*******************************************************************************
* 命名空间: SF.Core.Extensions
*
* 功 能： N/A
* 类 名： ClaimsPrincipalExtensions
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/15 9:45:18 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SF.Core.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// 获取用户所有权限
        /// </summary>
        /// <param name="principal"></param>
        /// <param name="allowedRolesCsv"></param>
        /// <returns></returns>
        public static bool IsInRoles(this ClaimsPrincipal principal, string allowedRolesCsv)
        {
            if (string.IsNullOrWhiteSpace(allowedRolesCsv)) { return true; } // empty indicates no role filtering
            string[] roles;
            // in some cases we are using semicolon separated not comma
            if (allowedRolesCsv.Contains(";"))
            {
                roles = allowedRolesCsv.Split(';');
            }
            else
            {
                roles = allowedRolesCsv.Split(',');
            }



            if (roles.Length == 0) { return true; }

            if (!principal.Identity.IsAuthenticated) { return false; }

            foreach (string role in roles)
            {
                if (role.Length == 0) continue;
                if (role == "All Users") { return true; }
                if (principal.IsInRole(role)) { return true; }
            }


            return false;

        }
        /// <summary>
        /// 获取用户显示名称
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static string GetDisplayName(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }
            var claim = principal.FindFirst("DisplayName");
            return claim != null ? claim.Value : null;
        }
        /// <summary>
        /// 获取用户邮箱
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static string GetEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }
            var claim = principal.FindFirst(ClaimTypes.Email);
            return claim != null ? claim.Value : null;
        }
        /// <summary>
        /// 获取用户ID
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }
            var claim = principal.FindFirst(ClaimTypes.NameIdentifier);
            return claim != null ? claim.Value : null;
        }
        /// <summary>
        /// 获取用户Name
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static string GetUserName(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }
            var claim = principal.FindFirst(ClaimTypes.Name);
            return claim != null ? claim.Value : null;
        }
    }
}
