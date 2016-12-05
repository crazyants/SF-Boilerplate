using System.Collections.Generic;

namespace SF.Core.Security
{
    /// <summary>
    /// 全局
    /// </summary>
    public class GobalPermissions : IPermissionProvider
    {
        public static readonly Permission AccessAdminPanel = new Permission("AccessAdminPanel", "访问管理面板");
        public static readonly Permission SecurityCallApi = new Permission("SecurityCallApi", "安全调用API");
        public static readonly Permission EditSetting = new Permission("EditSetting", "编辑配置信息.");

        public IEnumerable<Permission> GetPermissions()
        {
            return new[]
            {
                AccessAdminPanel,
                SecurityCallApi,
                EditSetting,
            };
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[]
            {
                new PermissionStereotype
                {
                    Name = "Administrator",
                    Permissions = GetPermissions()
                }
            };
        }

    }
}
