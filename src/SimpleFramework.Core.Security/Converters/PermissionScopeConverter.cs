using System;
using System.Collections.ObjectModel;
using System.Linq;
using Omu.ValueInjecter;
using SimpleFramework.Core.Security;
using SimpleFramework.Core.Entitys;

namespace SimpleFramework.Core.Security.Converters
{
    public static class PermissionScopeConverter
    {
        public static PermissionScope ToCoreModel(this  PermissionScopeEntity source, PermissionScope permissionScope)
        {
            permissionScope.InjectFrom(source);
            return permissionScope;
        }

        public static PermissionScopeEntity ToDataModel(this PermissionScope source)
        {
            var result = new PermissionScopeEntity();
            result.InjectFrom(source);
            return result;
        }

    }
}
