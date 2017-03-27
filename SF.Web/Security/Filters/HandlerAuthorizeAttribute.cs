using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Web.Security.Filters
{
    public class HandlerAuthorizeAttribute : ActionFilterAttribute
    {
        private readonly IAuthorizationService _authorizationService;
        private Permission _customMode;
        public HandlerAuthorizeAttribute(IAuthorizationService authorizationService, Permission mode)
        {
            _authorizationService = authorizationService;
            _customMode = mode;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
                var authorized = await _authorizationService.AuthorizeAsync(context.HttpContext.User, _customMode);
                if (!authorized)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

            await base.OnActionExecutionAsync(context, next);
        }

       
    }
}
