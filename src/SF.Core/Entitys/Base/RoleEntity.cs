using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SF.Core.Common;
using SF.Core.Abstraction.Entitys;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace SF.Core.Entitys
{
    public class RoleEntity : IdentityRole<long, UserRoleEntity, IdentityRoleClaim<long>>, IEntityWithTypedId<long>
    {
        public RoleEntity()
        {
            RolePermissions = new List<RolePermissionEntity>();
        }

        public RoleEntity(string name)
        {
            Name = name;
        }
        public string Description { get; set; }

        public virtual IList<RolePermissionEntity> RolePermissions { get; set; }
    }
}
