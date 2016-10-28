using System.Security.Claims;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SimpleFramework.Core.Data;
using SimpleFramework.Core.Entitys;

namespace SimpleFramework.Core.Extensions
{
    public class SimplRoleStore: RoleStore<RoleEntity, CoreDbContext, long, UserRoleEntity, IdentityRoleClaim<long>>
    {
        public SimplRoleStore(CoreDbContext context) : base(context)
        {
        }

        protected override IdentityRoleClaim<long> CreateRoleClaim(RoleEntity role, Claim claim)
        {
            return new IdentityRoleClaim<long> { RoleId = role.Id, ClaimType = claim.Type, ClaimValue = claim.Value };
        }
    }
}
