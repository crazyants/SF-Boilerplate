
using SF.Web.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.Demo
{
    /// <summary>
    /// 全局
    /// </summary>
    public class BackendPermissionProvider
    {
        public static readonly Permission DataItemView = new Permission("DataItem.View", "字典分类.查看");
        public static readonly Permission DataItemAdd = new Permission("DataItem.Add", "字典分类.新增");
        public static readonly Permission DataItemEdit = new Permission("DataItem.Edit", "字典分类.编辑");
        public static readonly Permission DataItemDelete = new Permission("DataItem.Delete", "字典分类.删除");
        public static readonly Permission AreaView = new Permission("Area.View", "区域.查看");
        public static readonly Permission AreaAdd = new Permission("Area.Add", "区域.新增");
        public static readonly Permission AreaEdit = new Permission("Area.Edit", "区域.编辑");
        public static readonly Permission AreaDelete = new Permission("Area.Delete", "区域.删除");
        public static readonly Permission Super = new Permission("Super", "超级权限");

    }
}
