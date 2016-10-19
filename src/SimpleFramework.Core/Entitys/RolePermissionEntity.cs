using SimpleFramework.Infrastructure.Common;
using SimpleFramework.Infrastructure.Entitys;
using System.Collections.ObjectModel;

namespace SimpleFramework.Core.Entitys
{
    public class RolePermissionEntity : AuditableEntity
    {
        public RolePermissionEntity()
        {
              Scopes = new NullCollection<PermissionScopeEntity>();
        }

        public long RoleId { get; set; }
        public long PermissionId { get; set; }
        public RoleEntity Role { get; set; }
        public PermissionEntity Permission { get; set; }
        public virtual ObservableCollection<PermissionScopeEntity> Scopes { get; set; }
    }
}
