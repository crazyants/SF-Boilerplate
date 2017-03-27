
using SF.Entitys.Abstraction;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace SF.Entitys
{
    public class RoleModuleEntity : BaseEntity
    {
        public RoleModuleEntity()
        {
        }

        public long RoleId { get; set; }
        public long ModuleId { get; set; }
        public RoleEntity Role { get; set; }
        public ModuleEntity Module { get; set; }

    }
}
