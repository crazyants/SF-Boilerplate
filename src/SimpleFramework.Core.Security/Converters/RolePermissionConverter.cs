using System;
using System.Collections.ObjectModel;
using System.Linq;
using Omu.ValueInjecter;
using SimpleFramework.Core.Security;
using SimpleFramework.Core.Entitys;
using SimpleFramework.Core.Common;
using SimpleFramework.Core.Extensions;
using System.Collections.Generic;

namespace SimpleFramework.Core.Security.Converters
{
    public static class RolePermissionConverter
    {
        public static Permission ToCoreModel(this RolePermissionEntity source, IPermissionScopeService scopeService)
        {
            var result = new Permission();
            result.InjectFrom(source.Permission);
            result.AssignedScopes = source.Scopes.Select(x => new { source = x, target = scopeService.GetScopeByTypeName(x.Type) })
                                                  .Where(x=> x.target != null)
                                                  .Select(x=> x.source.ToCoreModel(x.target))
                                                  .ToArray();
            result.AvailableScopes = scopeService.GetAvailablePermissionScopes(result.Id).ToArray();
            return result;
        }

     
        public static RolePermissionEntity ToRolePemissionDataModel(this Permission source)
        {
            var result = new RolePermissionEntity();
            result.PermissionId = source.Id;
            if (source.AssignedScopes != null)
            {
                result.Scopes = new List<PermissionScopeEntity>(source.AssignedScopes.Where(x=>!String.IsNullOrEmpty(x.Scope)).Select(x => x.ToDataModel()));
            }
            return result;
        }

        public static void Patch(this RolePermissionEntity source, RolePermissionEntity target)
        {
            if (target == null)
                throw new ArgumentNullException("target");

            if (!source.Scopes.IsNullCollection())
            {
                var comparer = AnonymousComparer.Create((PermissionScopeEntity x) => x.Scope);
                source.Scopes.Patch(target.Scopes, comparer, (sourceItem, targetItem) => { });
            }
        }
    }
}
