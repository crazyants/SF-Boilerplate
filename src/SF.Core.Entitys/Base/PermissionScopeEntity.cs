using SF.Core.Entitys.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SF.Core.Entitys
{
    public class PermissionScopeEntity : BaseEntity
    {
        [Required]
        [StringLength(1024)]
        public string Scope { get; set; }

        [StringLength(255)]
        public string Type { get; set; }

        [StringLength(1024)]
        public string Label { get; set; }

        #region Navigation properties
        public long RolePermissionId { get; set; }
        public RolePermissionEntity RolePermission { get; set; } 
        #endregion
    }
}
