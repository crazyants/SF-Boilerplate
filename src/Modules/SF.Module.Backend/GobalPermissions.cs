using SF.Web.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.Backend
{
    /// <summary>
    /// 全局
    /// </summary>
    public class GobalPermissions : IPermissionProvider
    {
        public static readonly Permission AccessAdminPanel = new Permission("AccessAdminPanel", "访问管理面板");
        public static readonly Permission SecurityCallApi = new Permission("SecurityCallApi", "安全调用API");
        public static readonly Permission DataItemAdd = new Permission("DataItem.Add", "字典分类.新增.");
        public static readonly Permission DataItemEdit = new Permission("DataItem.Edit", "字典分类.编辑.");
        public static readonly Permission DataItemDelete = new Permission("DataItem.Delete", "字典分类.删除.");
        public IEnumerable<Permission> GetPermissions()
        {
            return new[]
            {
                AccessAdminPanel,
                SecurityCallApi,
                DataItemAdd,
                DataItemEdit,
                DataItemDelete,
            };
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[]
            {
                new PermissionStereotype
                {
                    Name = "SuperAdmin",
                    Description="超级管理员",
                    Permissions = GetPermissions()
                },
                 new PermissionStereotype
                {
                    Name = "Admin",
                    Description="普通管理员",
                    Permissions =    new[]
                    {
                        DataItemAdd,
                        DataItemEdit,
                        DataItemDelete,
                    }
                 }
         };
        }

    }
}
