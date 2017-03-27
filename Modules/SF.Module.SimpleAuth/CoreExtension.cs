using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using SF.Core;
using SF.Module.SimpleAuth.Models;
using SF.Module.SimpleAuth.Services;
using SF.Module.SimpleAuth.Tenants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SF.Web.Modules;
using SF.Core.Infrastructure.Modules;

namespace SF.Module.SimpleAuth
{
    public class CoreExtension : ModuleInitializerBase
    {
        public override IEnumerable<KeyValuePair<int, Action<IServiceCollection>>> ConfigureServicesActionsByPriorities
        {
            get
            {
                return new Dictionary<int, Action<IServiceCollection>>()
                {
                    [0] = this.AddCoreServices
                };
            }
        }

        public override IEnumerable<KeyValuePair<int, Action<IApplicationBuilder>>> ConfigureActionsByPriorities
        {
            get
            {
                return new Dictionary<int, Action<IApplicationBuilder>>()
                {
                    [0] = this.UseMvc,
                };
            }
        }

        #region MyRegion IServiceCollection

        /// <summary>
        /// 添加全局服务注册
        /// </summary>
        /// <param name="services"></param>
        public void AddCoreServices(IServiceCollection services)
        {
            services.Configure<MultiTenancyOptions>(configurationRoot.GetSection("MultiTenancy"));
            services.AddMultitenancy<AppTenant, CachingAppTenantResolver>();

            //services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.Configure<SimpleAuthSettings>(configurationRoot.GetSection("SimpleAuthSettings"));
            services.AddScoped<IUserLookupProvider, AppTenantUserLookupProvider>();
            services.Configure<List<SimpleAuthUser>>(configurationRoot.GetSection("Users"));
            services.AddScoped<IAuthSettingsResolver, AppTenantAuthSettingsResolver>();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddScoped<IUserLookupProvider, DefaultUserLookupProvider>(); // single tenant
            services.TryAddScoped<IPasswordHasher<SimpleAuthUser>, PasswordHasher<SimpleAuthUser>>();
            services.TryAddScoped<IAuthSettingsResolver, DefaultAuthSettingsResolver>();
            services.AddScoped<SignInManager, SignInManager>();

            services.AddMvc()
              .AddRazorOptions(options =>
              {
                  options.AddEmbeddedViewsForSimpleAuth();
                  options.ViewLocationExpanders.Add(new TenantViewLocationExpander());
              });
        }


        #endregion

        #region MyRegion IApplicationBuilder

        public void UseMvc(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMultitenancy<AppTenant>();

            applicationBuilder.UsePerTenant<AppTenant>((ctx, builder) =>
            {
                var authCookieOptions = new CookieAuthenticationOptions();
                authCookieOptions.AuthenticationScheme = ctx.Tenant.AuthenticationScheme;
                authCookieOptions.LoginPath = new PathString("/login");
                authCookieOptions.AccessDeniedPath = new PathString("/");
                authCookieOptions.AutomaticAuthenticate = true;
                authCookieOptions.AutomaticChallenge = true;
                // authCookieOptions.CookieName = ctx.Tenant.AuthenticationScheme;
                authCookieOptions.CookieName = $"{ctx.Tenant.Id}.application";
                builder.UseCookieAuthentication(authCookieOptions);

            });

        }


        #endregion


    }
}