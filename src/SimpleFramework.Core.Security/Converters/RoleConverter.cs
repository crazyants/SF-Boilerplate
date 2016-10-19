using System;
using System.Collections.ObjectModel;
using System.Linq;
using Omu.ValueInjecter;
using SimpleFramework.Core.Security;
using SimpleFramework.Core.Entitys;
using SimpleFramework.Infrastructure.Common;
using SimpleFramework.Infrastructure.Extensions;

namespace SimpleFramework.Core.Security.Converters
{
    public static class RoleConverter
    {
        public static Role ToCoreModel(this RoleEntity source, IPermissionScopeService scopeService)
        {
            var result = new Role();
            result.InjectFrom(source);
  
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
                result.RolePermissions = new ObservableCollection<RolePermissionEntity>(source.Permissions.Select(x=> x.ToRolePemissionDataModel()));
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

            var patchInjection = new PatchInjection<RoleEntity>(x => x.Name, x => x.Description);
            target.InjectFrom(patchInjection, source);

            if (!source.RolePermissions.IsNullCollection())
            {
                var comparer = AnonymousComparer.Create((RolePermissionEntity rp) => rp.PermissionId);
                source.RolePermissions.Patch(target.RolePermissions, comparer, (sourceItem, targetItem) => sourceItem.Patch(targetItem));
            }

        }
    }
}
