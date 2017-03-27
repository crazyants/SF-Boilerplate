/*******************************************************************************
* 命名空间: SF.Web.Security
*
* 功 能： N/A
* 类 名： SiteUserClaimsPrincipalFactory
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/15 9:49:07 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SF.Data;
using SF.Entitys;
using SF.Web.Security.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace SF.Web.Security
{
    public class SiteUserClaimsPrincipalFactory<TUser, TRole> : UserClaimsPrincipalFactory<TUser, TRole>
          where TUser : UserEntity
          where TRole : RoleEntity
    {
        public SiteUserClaimsPrincipalFactory(
            IBaseUnitOfWork baseUnitOfWork,
            UserManager<TUser> userManager,
            RoleManager<TRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor,
            IEnumerable<ICustomClaimProvider> customClaimProviders
            )
            : base(userManager, roleManager, optionsAccessor)
        { 
            this.baseUnitOfWork = baseUnitOfWork;
            options = optionsAccessor.Value;
            this.customClaimProviders = customClaimProviders;
        }

         
        private IBaseUnitOfWork baseUnitOfWork;
        private IdentityOptions options;
        private IEnumerable<ICustomClaimProvider> customClaimProviders;

        public override async Task<ClaimsPrincipal> CreateAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            var userId = await UserManager.GetUserIdAsync(user);
            var userName = await UserManager.GetUserNameAsync(user);

            var id = new ClaimsIdentity(
                options.Cookies.ApplicationCookie.AuthenticationScheme,
                Options.ClaimsIdentity.UserNameClaimType,
                Options.ClaimsIdentity.RoleClaimType);

            id.AddClaim(new Claim(Options.ClaimsIdentity.UserIdClaimType, userId));
            id.AddClaim(new Claim("sub", userId)); //JwtClaimTypes.Subject
            id.AddClaim(new Claim(Options.ClaimsIdentity.UserNameClaimType, userName));
            id.AddClaim(new Claim("name", userName)); //needed by identtyserver integration

            if (UserManager.SupportsUserSecurityStamp)
            {
                id.AddClaim(new Claim(Options.ClaimsIdentity.SecurityStampClaimType,
                    await UserManager.GetSecurityStampAsync(user)));
            }

            if (UserManager.SupportsUserRole)
            {
                var roles = await UserManager.GetRolesAsync(user);
                foreach (var roleName in roles)
                {
                    id.AddClaim(new Claim(Options.ClaimsIdentity.RoleClaimType, roleName));
                    if (RoleManager.SupportsRoleClaims)
                    {
                        var role = await RoleManager.FindByNameAsync(roleName);
                        if (role != null)
                        {
                            id.AddClaims(await RoleManager.GetClaimsAsync(role));
                        }
                    }
                }
            }
            if (UserManager.SupportsUserClaim)
            {
                id.AddClaims(await UserManager.GetClaimsAsync(user));
            }

            foreach (var provider in customClaimProviders)
            {
                await provider.AddClaims(user, id);
            }

            var principal = new ClaimsPrincipal(id);

            if (principal.Identity is ClaimsIdentity)
            {
                var identity = (ClaimsIdentity)principal.Identity;

                var displayNameClaim = new Claim("DisplayName", user.DisplayName);
                if (!identity.HasClaim(displayNameClaim.Type, displayNameClaim.Value))
                {
                    identity.AddClaim(displayNameClaim);
                }

                var emailClaim = new Claim(ClaimTypes.Email, user.Email);
                if (!identity.HasClaim(emailClaim.Type, emailClaim.Value))
                {
                    identity.AddClaim(emailClaim);
                }

                var site = await baseUnitOfWork.BaseWorkArea.SiteSettings.Query().AsNoTracking().SingleOrDefaultAsync(
                 x => x.Id.Equals(user.SiteId))
                 .ConfigureAwait(false);
                if (site != null)
                {
                    var siteGuidClaim = new Claim("SiteGuid", site.Id.ToString());

                    if (!identity.HasClaim(siteGuidClaim.Type, siteGuidClaim.Value))
                    {
                        identity.AddClaim(siteGuidClaim);
                    }
                }

                if (principal.IsInRole("Administrators"))
                {
                    if (site != null && site.IsServerAdminSite)
                    {
                        Claim serverAdminRoleClaim = new Claim(ClaimTypes.Role, "ServerAdmins");
                        identity.AddClaim(serverAdminRoleClaim);
                    }
                }
            }

            return principal;
        }
    }
}
