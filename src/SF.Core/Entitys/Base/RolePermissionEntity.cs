using SF.Core.Common;
using SF.Core.Abstraction.Entitys;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace SF.Core.Entitys
{
    public class RolePermissionEntity : BaseEntity
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
