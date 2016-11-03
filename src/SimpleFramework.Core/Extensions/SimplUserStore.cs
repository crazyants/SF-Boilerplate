using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SimpleFramework.Core.Data;
using SimpleFramework.Core.Entitys;

namespace SimpleFramework.Core.Extensions
{
    public class SimplUserStore : UserStore<UserEntity, RoleEntity, CoreDbContext, long, IdentityUserClaim<long>, UserRoleEntity,
        IdentityUserLogin<long>, IdentityUserToken<long>>
    {
        public SimplUserStore(CoreDbContext context) : base(context)
        {
        }

        protected override UserRoleEntity CreateUserRole(UserEntity user, RoleEntity role)
        {
            return new UserRoleEntity()
            {
                UserId = user.Id,
                RoleId = role.Id
            };
        }

        protected override IdentityUserClaim<long> CreateUserClaim(UserEntity user, Claim claim)
        {
            var userClaim = new IdentityUserClaim<long> { UserId = user.Id };
            userClaim.InitializeFromClaim(claim);
            return userClaim;
        }

        protected override IdentityUserLogin<long> CreateUserLogin(UserEntity user, UserLoginInfo login)
        {
            return new IdentityUserLogin<long>
            {
                UserId = user.Id,
                ProviderKey = login.ProviderKey,
                LoginProvider = login.LoginProvider,
                ProviderDisplayName = login.ProviderDisplayName
            };
        }

        protected override IdentityUserToken<long> CreateUserToken(UserEntity user, string loginProvider, string name, string value)
        {
            return new IdentityUserToken<long>
            {
                UserId = user.Id,
                LoginProvider = loginProvider,
                Name = name,
                Value = value
            };
        }
    }
}
