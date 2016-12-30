using System.Collections.Generic;

namespace SF.Web.Security
{
    /// <summary>
    /// 全局
    /// </summary>
    public class GobalPermissionProvider : IPermissionProvider
    {
        public static readonly Permission AccessAdminPanel = new Permission("AccessAdminPanel", "访问管理面板");
        public static readonly Permission SecurityCallApi = new Permission("SecurityCallApi", "安全调用API");

        public IEnumerable<Permission> GetPermissions()
        {
            return new[]
            {
                AccessAdminPanel,
                SecurityCallApi,
            };
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[]
            {
                new PermissionStereotype
                {
                    Name = "Administrators",
                    Permissions = GetPermissions()
                }
            };
        }

    }
}
