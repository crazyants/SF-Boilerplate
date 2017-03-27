/*******************************************************************************
* 命名空间: SF.Web.Middleware
*
* 功 能： N/A
* 类 名： HotlinkingPreventionMiddleware
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/2 14:14:16 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.IO;
using System.Threading.Tasks;

namespace SF.Web.Middleware
{
    public class HotlinkingPreventionMiddleware
    {
        private readonly string _wwwrootFolder;
        private readonly RequestDelegate _next;
        public HotlinkingPreventionMiddleware(RequestDelegate next, IHostingEnvironment env)
        {
            _wwwrootFolder = env.WebRootPath;
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            var applicationUrl = $"{context.Request.Scheme}://{context.Request.Host.Value}";
            var headersDictionary = context.Request.Headers;
            var urlReferrer = headersDictionary[HeaderNames.Referer].ToString();
            if (!string.IsNullOrEmpty(urlReferrer) && !urlReferrer.StartsWith(applicationUrl))
            {
                var unauthorizedImagePath = Path.Combine(_wwwrootFolder, "Images/Unauthorized.png");

                await context.Response.SendFileAsync(unauthorizedImagePath);
            }

            await _next(context);
        }
    }
}
