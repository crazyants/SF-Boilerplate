using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.FileProviders;
using Hangfire;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Reflection;
using System;

namespace SimpleFramework.WebHost.Extensions
{
    public static class ApplicationBuilderExtensions
    {
      
        /// <summary>
        /// 配置多语言信息
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCustomizedRequestLocalization(this IApplicationBuilder app)
        {
            var supportedCultures = new[]
            {
                new CultureInfo("zh"),
                new CultureInfo("en"),
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en", "en"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });
            return app;
        }
        /// <summary>
        /// Hangfire配置
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCustomizedHangfire(this IApplicationBuilder app, IConfigurationRoot configuration)
        {
         //   Hangfire.GlobalConfiguration.Configuration.UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection"));
            //app.UseHangfireDashboard();
            //app.UseHangfireServer();
            //
            return app;
        }
    }
}
