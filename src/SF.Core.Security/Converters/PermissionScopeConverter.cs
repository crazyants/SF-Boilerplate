
using AutoMapper;
using SF.Core.Entitys;

namespace SF.Core.Security.Converters
{
    public static class PermissionScopeConverter
    {
        public static PermissionScope ToCoreModel(this  PermissionScopeEntity source, PermissionScope permissionScope)
        {
         
            permissionScope = Mapper.Map<PermissionScopeEntity, PermissionScope>(source);
            return permissionScope;
        }

        public static PermissionScopeEntity ToDataModel(this PermissionScope source)
        {
            var result = new PermissionScopeEntity();
            result = Mapper.Map<PermissionScope, PermissionScopeEntity>(source);
            return result;
        }

    }
}
