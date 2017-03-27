
using SF.Entitys.Abstraction;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace SF.Entitys
{
    public class RolePermissionEntity : BaseEntity
    {
        public RolePermissionEntity()
        {
        }

        public long RoleId { get; set; }
        public long PermissionId { get; set; }
        public RoleEntity Role { get; set; }
        public PermissionEntity Permission { get; set; }

    }
}
