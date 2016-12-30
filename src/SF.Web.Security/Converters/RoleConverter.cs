using System;
using System.Linq;
using SF.Core.Entitys;
using SF.Core.Common;
using SF.Core.Extensions;
using System.Collections.Generic;
using AutoMapper;

namespace SF.Web.Security.Converters
{
    public static class RoleConverter
    {
        public static Role ToCoreModel(this RoleEntity source, IPermissionScopeService scopeService)
        {
            var result = new Role();
            result = Mapper.Map<RoleEntity, Role>(source);
            result.Permissions = source.RolePermissions.Select(rp => rp.ToCoreModel(scopeService)).ToArray();
            return result;
        }

        public static RoleEntity ToDataModel(this Role source)
        {
            var result = new RoleEntity
            {
                Name = source.Name,
                Description = source.Description,
            };

            result.Id = source.Id;

            result.RolePermissions = new NullCollection<RolePermissionEntity>();

            if (source.Permissions != null)
            {
                result.RolePermissions = new List<RolePermissionEntity>(source.Permissions.Select(x => x.ToRolePemissionDataModel()));
            }
            return result;
        }

        public static UserRoleEntity ToAssignmentDataModel(this Role source)
        {
            var result = new UserRoleEntity();
            result.RoleId = source.Id;
            return result;
        }

        public static void Patch(this RoleEntity source, RoleEntity target)
        {
            if (target == null)
                throw new ArgumentNullException("target");

            target = Mapper.Map<RoleEntity, RoleEntity>(source);
            if (!source.RolePermissions.IsNullCollection())
            {
                var comparer = AnonymousComparer.Create((RolePermissionEntity rp) => rp.PermissionId);
                source.RolePermissions.Patch(target.RolePermissions, comparer, (sourceItem, targetItem) => sourceItem.Patch(targetItem));
            }

        }
    }
}
