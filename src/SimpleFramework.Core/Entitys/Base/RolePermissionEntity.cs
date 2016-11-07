using SimpleFramework.Core.Common;
using SimpleFramework.Core.Abstraction.Entitys;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace SimpleFramework.Core.Entitys
{
    public class RolePermissionEntity : AuditableEntity
    {
        public RolePermissionEntity()
        {
              Scopes = new List<PermissionScopeEntity>();
        }

        public long RoleId { get; set; }
        public long PermissionId { get; set; }
        public RoleEntity Role { get; set; }
        public PermissionEntity Permission { get; set; }
        public virtual IList<PermissionScopeEntity> Scopes { get; set; }
    }
}
