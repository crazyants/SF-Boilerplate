using System;
using System.Linq;
using SF.Entitys;
using SF.Core.Common;
using SF.Core.Extensions;
using System.Collections.Generic;
using AutoMapper;

namespace SF.Web.Security.Converters
{
    public static class RoleConverter
    {
        public static Role ToCoreModel(this RoleEntity source)
        {
            var result = new Role();
            result = Mapper.Map<RoleEntity, Role>(source);

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
 
    }
}
