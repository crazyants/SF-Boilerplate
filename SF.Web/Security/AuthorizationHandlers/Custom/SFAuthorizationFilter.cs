using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using SF.Core.Errors.Exceptions;
using SF.Web.Security.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Web.Security.AuthorizationHandlers.Custom
{
    public class SFAuthorizationFilter : IAsyncAuthorizationFilter
    {
        public ILogger Logger { get; set; }

        private readonly IAuthorizationHelper _authorizationHelper;


        public SFAuthorizationFilter(
            IAuthorizationHelper authorizationHelper
            )
        {
            _authorizationHelper = authorizationHelper;
            Logger = NullLogger.Instance;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            // Allow Anonymous skips all authorization
            if (context.Filters.Any(item => item is IAllowAnonymousFilter))
            {
                return;
            }

            try
            {
                //TODO: Avoid using try/catch, use conditional checking
                await _authorizationHelper.AuthorizeAsync(context.ActionDescriptor.GetMethodInfo());
            }
            catch (ValidationException ex)
            {
                Logger.LogWarning(ex.ToString(), ex);


                //if (ActionResultHelper.IsObjectResult(context.ActionDescriptor.GetMethodInfo().ReturnType))
                //{
                //    context.Result = new ObjectResult(new AjaxResponse(_errorInfoBuilder.BuildForException(ex), true))
                //    {
                //        StatusCode = context.HttpContext.User.Identity.IsAuthenticated
                //            ? (int)System.Net.HttpStatusCode.Forbidden
                //            : (int)System.Net.HttpStatusCode.Unauthorized
                //    };
                //}
                //else
                //{
                context.Result = new ChallengeResult();
                //}
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString(), ex);


                //if (ActionResultHelper.IsObjectResult(context.ActionDescriptor.GetMethodInfo().ReturnType))
                //{
                //    context.Result = new ObjectResult(new AjaxResponse(_errorInfoBuilder.BuildForException(ex)))
                //    {
                //        StatusCode = (int)System.Net.HttpStatusCode.InternalServerError
                //    };
                //}
                //else
                //{
                //TODO: How to return Error page?
                context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.InternalServerError);
                // }
            }
        }
    }
}
