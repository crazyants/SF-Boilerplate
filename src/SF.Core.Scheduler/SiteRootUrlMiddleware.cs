using System.Threading.Tasks;
using SF.Core.Scheduler.Contracts;
using Microsoft.AspNetCore.Http;
using SF.Core.Common;

namespace SF.Core.Scheduler
{
    /// <summary>
    /// DNTScheduler needs a ping service to keep it alive.
    /// This middleware provides the SiteRootUrl for the PingTask.
    /// </summary>
    public class SiteRootUrlMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IThisApplication _thisApplication;

        /// <summary>
        /// ctor.
        /// </summary>
        public SiteRootUrlMiddleware(RequestDelegate next, IThisApplication thisApplication)
        {
            thisApplication.CheckArgumentNull(nameof(thisApplication));

            _next = next;
            _thisApplication = thisApplication;
        }

        /// <summary>
        /// This middleware provides the SiteRootUrl for the PingTask.
        /// </summary>
        public Task Invoke(HttpContext context)
        {
            var applicationUrl = $"{context.Request.Scheme}://{context.Request.Host.Value}";
            _thisApplication.SiteRootUrl = applicationUrl;

            return _next(context);
        }
    }
}