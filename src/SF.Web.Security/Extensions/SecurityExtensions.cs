using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using SF.Web.Security.Filters;
using SF.Web.Security.Attributes;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SF.Core;
using Microsoft.AspNetCore.Identity;
using SF.Core.Entitys;
using SF.Web.Security.Providers;

namespace SF.Web.Security
{
    public class SecurityExtensions : ModuleInitializerBase
    {
        public override IEnumerable<KeyValuePair<int, Action<IServiceCollection>>> ConfigureServicesActionsByPriorities
        {
            get
            {
                return new Dictionary<int, Action<IServiceCollection>>()
                {
                    [10000] = this.AddSecuritys,

                };
            }
        }

        public override IEnumerable<KeyValuePair<int, Action<IApplicationBuilder>>> ConfigureActionsByPriorities
        {
            get
            {
                return new Dictionary<int, Action<IApplicationBuilder>>()
                {
                    [0] = this.UseSecurity,
                };
            }
        }


        /// <summary>
        /// 插件基本服务注册
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public void AddSecuritys(IServiceCollection services)
        {
            //自定义Claim
            //services.TryAddScoped<ICustomClaimProvider, DoNothingCustomClaimProvider>();
            //services.TryAddScoped<IUserClaimsPrincipalFactory<UserEntity>, SiteUserClaimsPrincipalFactory<UserEntity,RoleEntity>>();

            services.AddScoped<IAuthorizationHandler, RolesPermissionsHandler>();
            services.AddScoped<IAuthorizationHandler, PermissionHandler>();

            services.TryAddTransient<IFilterProvider, DependencyFilterProvider>();
            services.TryAddTransient<IFilterMetadata, AdminFilter>();
            services.AddSingleton<IPermissionProvider, GobalPermissions>();
            services.AddSingleton<IPermissionScopeService, PermissionScopeService>();
            services.AddSingleton<ISecurityService, SecurityService>();
            services.AddSingleton<IPermissionProvider>(sp =>
            {
                return sp.GetRequiredService<GobalPermissions>();
            });
        }

        public void UseSecurity(IApplicationBuilder applicationBuilder)
        {
            // Ensure the shell tenants are loaded when a request comes in
            // and replaces the current service provider for the tenant's one.

        }

    }
}
