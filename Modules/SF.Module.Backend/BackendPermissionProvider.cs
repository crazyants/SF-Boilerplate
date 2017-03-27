
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
    public class BackendPermissionProvider : IPermissionProvider
    {
        public static readonly Permission DataItemView = new Permission("DataItem.View", "字典分类.查看");
        public static readonly Permission DataItemAdd = new Permission("DataItem.Add", "字典分类.新增");
        public static readonly Permission DataItemEdit = new Permission("DataItem.Edit", "字典分类.编辑");
        public static readonly Permission DataItemDelete = new Permission("DataItem.Delete", "字典分类.删除");
        public static readonly Permission AreaView = new Permission("Area.View", "区域.查看");
        public static readonly Permission AreaAdd = new Permission("Area.Add", "区域.新增");
        public static readonly Permission AreaEdit = new Permission("Area.Edit", "区域.编辑");
        public static readonly Permission AreaDelete = new Permission("Area.Delete", "区域.删除");
        public IEnumerable<Permission> GetPermissions()
        {
            return new[]
            {
                DataItemAdd,
                DataItemEdit,
                DataItemDelete,
                AreaView,
                AreaAdd,
                AreaEdit,
                AreaDelete,
            };
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[]
            {
                new PermissionStereotype
                {
                    Name = "Administrators",
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
