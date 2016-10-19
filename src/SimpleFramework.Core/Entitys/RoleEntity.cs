using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SimpleFramework.Infrastructure.Common;
using SimpleFramework.Infrastructure.Entitys;
using System.Collections.ObjectModel;


namespace SimpleFramework.Core.Entitys
{
    public class RoleEntity : IdentityRole<long, UserRoleEntity, IdentityRoleClaim<long>>, IEntityWithTypedId<long>
    {
        public RoleEntity()
        {
            RolePermissions = new NullCollection<RolePermissionEntity>();
        }

        public RoleEntity(string name)
        {
            Name = name;
        }
        public string Description { get; set; }

        public virtual ObservableCollection<RolePermissionEntity> RolePermissions { get; set; }
    }
}
