using Microsoft.AspNetCore.Http;
using SF.Core.Abstraction.Resolvers;
using System;
using System.Threading.Tasks;


namespace SF.Web.Middleware
{
    /// <summary>
    /// 当前用户中间件
    /// </summary>
    public class CurrentUserMiddleware
    {
        private readonly ICurrentUserResolver _currentUser;
        private readonly RequestDelegate _next;
        public CurrentUserMiddleware(RequestDelegate next, ICurrentUserResolver currentUser)
        {
            _next = next;
            _currentUser = currentUser;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            string userName = null;

            //if (httpContext != null && httpContext.User != null)
            //{
            //    var identity = httpContext.User.Identity;
            //    if (identity != null && identity.IsAuthenticated)
            //    {

            //        if (string.IsNullOrEmpty(userName))
            //        {
            //            userName = identity.Name;
            //        }

            //    }
            //}

            if (httpContext?.User?.Identity?.Name != null)
            {
                if (string.IsNullOrEmpty(userName))
                {
                    var identity = httpContext.User.Identity;
                    userName = identity.Name;
                }
            }

            if (string.IsNullOrEmpty(userName))
            {
                userName = "unknown";
            }
            if (_currentUser != null)
            {
                _currentUser.UserName = userName;
            }

            await _next.Invoke(httpContext);
        }
    }
}
