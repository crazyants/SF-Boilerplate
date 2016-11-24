using Microsoft.AspNetCore.Http;
using SF.Core.Services;
using System;
using System.Threading.Tasks;


namespace SF.Core.Security
{
    /// <summary>
    /// 当前用户中间件
    /// </summary>
    public class CurrentUserMiddleware
    {
        private readonly ICurrentUser _currentUser;
        private readonly RequestDelegate _next;
        public CurrentUserMiddleware(RequestDelegate next, ICurrentUser currentUser)
        {
            _next = next;
            _currentUser = currentUser;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            string userName = null;

            if (httpContext != null && httpContext.User != null)
            {
                var identity = httpContext.User.Identity;
                if (identity != null && identity.IsAuthenticated)
                {

                    if (string.IsNullOrEmpty(userName))
                    {
                        userName = identity.Name;
                    }

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
