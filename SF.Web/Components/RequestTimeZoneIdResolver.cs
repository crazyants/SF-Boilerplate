/*******************************************************************************
* 命名空间: SF.Web.Components
*
* 功 能： N/A
* 类 名： RequestTimeZoneIdResolver
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/15 9:44:05 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SaasKit.Multitenancy;
using SF.Core.Abstraction.Resolvers;
using SF.Entitys;
using SF.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SF.Web.Components
{
    public class RequestTimeZoneIdResolver : ITimeZoneIdResolver
    {
        public RequestTimeZoneIdResolver(
            IHttpContextAccessor contextAccessor,
            ITenantResolver<SiteContext> siteResolver,
            UserManager<UserEntity> userManager
            )
        {
            this.contextAccessor = contextAccessor;
            this.siteResolver = siteResolver;
            this.userManager = userManager;
        }

        private IHttpContextAccessor contextAccessor;
        private ITenantResolver<SiteContext> siteResolver;
        private UserManager<UserEntity> userManager;

        public async Task<string> GetUserTimeZoneId(CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            var context = contextAccessor.HttpContext;
            if (context.User.Identity.IsAuthenticated)
            {
                var user = await userManager.FindByIdAsync(context.User.GetUserId());
                if ((user != null) && (!string.IsNullOrEmpty(user.TimeZoneId)))
                {
                    return user.TimeZoneId;
                }
            }

            return await GetSiteTimeZoneId(cancellationToken);
        }

        public Task<string> GetSiteTimeZoneId(CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            var tenant = contextAccessor.HttpContext.GetTenant<SiteContext>();
            if (tenant != null)
            {
                if (!string.IsNullOrEmpty(tenant.TimeZoneId))
                    return Task.FromResult(tenant.TimeZoneId);
            }

            return Task.FromResult("America/New_York"); //default
        }

    }
}
