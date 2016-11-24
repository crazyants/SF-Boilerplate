using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using SF.Core.Common;
using SF.Core.Errors;
using SF.Core.Errors.Exceptions;
using SF.Core.Web.Base.Datatypes;
using System;
using SF.Core.Web.Models;

namespace SF.Core.Web.Base.Controllers
{
    public abstract class ControllerBase : Controller
    {
        private readonly IExceptionMapper _exceptionMapper;
        protected ControllerBase(IServiceCollection service, ILogger<Controller> logger)
        {
            this.Logger = logger;
            _exceptionMapper = service.BuildServiceProvider().GetService<IExceptionMapper>();
        }

        protected ILogger<Controller> Logger { get; private set; }


        protected virtual IActionResult BadRequestResult(ModelStateDictionary modelState)
        {
            // ToDo : does this have to be logged ?
            return new BadRequestObjectResult(modelState);
        }

        protected virtual IActionResult BadRequestResult(ValidationException validationEx)
        {
            // ToDo : does this have to be logged ?
            var modelState = new ModelStateDictionary();
            foreach (var messages in validationEx.Messages.Values)
            {
                foreach (var message in messages)
                {
                    modelState.AddModelError(String.Empty, message);
                }
            }
            return new BadRequestObjectResult(modelState);

        }

        protected virtual IActionResult CreatedAtRouteResult(string routeName, object routeValues, object value)
        {
            return new CreatedAtRouteResult(routeName, routeValues, value);
        }

        protected virtual IActionResult InternalServerError(Exception ex, string message, params object[] args)
        {
            if (ex == null)
            {
                Logger.LogError(message, args);
                var error = new Exception(string.Format(message, args));
                return new ObjectResult(_exceptionMapper.Resolve(error)) { StatusCode = 500 };
            }
            else
            {

                var errorMessage = String.Format(message, args);
                Logger.LogError("{0} : {1}", errorMessage, ExceptionHelper.GetAllToStrings(ex));
                var error = new Exception(string.Format("{0} : {1}", errorMessage, ex.Message));
                return new ObjectResult(_exceptionMapper.Resolve(error)) { StatusCode = 500 };
            }
        }

        //protected virtual IActionResult InternalServerError(Exception error)
        //{
        //    if (error == null) throw new ArgumentNullException("error", "error is null");
        //    foreach (var message in error.Message)
        //        Logger.LogError(error.Message);
        //    return new ObjectResult(error) { StatusCode = 500 };
        //}

        protected virtual IActionResult OkResult()
        {
            return new StatusCodeResult(200);
        }

        protected virtual IActionResult OkResult(object value)
        {
            return new ObjectResult(value) { StatusCode = 200 };
        }

        protected virtual IActionResult NotFoundResult(string message, params object[] args)
        {
            Logger.LogWarning(message, args);
            var error = new Exception(string.Format(message, args));
            return new ObjectResult(_exceptionMapper.Resolve(error)) { StatusCode = 404 };
        }


        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        protected virtual ActionResult Success(string message)
        {
            return Content(new AjaxResult { state = ResultType.success, message = message }.ToJson());
        }
        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        protected virtual ActionResult Success(string message, object data)
        {
            return Content(new AjaxResult { state = ResultType.success, message = message, data = data }.ToJson());
        }
        /// <summary>
        /// 返回失败消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        protected virtual ActionResult Error(string message)
        {
            return Content(new AjaxResult { state = ResultType.error, message = message }.ToJson());
        }
    }
}
