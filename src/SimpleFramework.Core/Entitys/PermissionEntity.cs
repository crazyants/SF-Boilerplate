using SimpleFramework.Core.Abstraction.Entitys;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleFramework.Core.Entitys
{
    public class PermissionEntity : AuditableEntity
    {
        public PermissionEntity()
        {
            RolePermissions = new ObservableCollection<RolePermissionEntity>();
        }
        [Required]
        [StringLength(256)]
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ObservableCollection<RolePermissionEntity> RolePermissions { get; set; }
    }
}
