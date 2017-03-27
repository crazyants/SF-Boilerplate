using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using SF.Core.Common;
using SF.Core.Errors;
using SF.Core.Errors.Exceptions;
using SF.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using SF.Web.Extensions;

namespace SF.Web.Base.Controllers
{
    public abstract class ControllerBase : Controller
    {
        private readonly IExceptionMapper _exceptionMapper;

        protected ControllerBase(IServiceCollection service, ILogger<Controller> logger)
        {
            this.Logger = logger;
            this.Service = service;
            _exceptionMapper = service.BuildServiceProvider().GetService<IExceptionMapper>();
        }

        protected ILogger<Controller> Logger { get; private set; }
        protected IServiceCollection Service { get; private set; }

        protected virtual IActionResult BadRequestResult(ModelStateDictionary modelState)
        {
            // ToDo : does this have to be logged ?
            //return new BadRequestObjectResult(modelState);
            var errorMessages = modelState.AllModelStateErrors();
            var message = string.Join(",", errorMessages.Select(x => x.ErrorMessage));
            return Content(new AjaxResult { state = ResultType.error.ToString(), message = message }.ToJson());
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
            return BadRequestResult(modelState);

        }


        protected virtual IActionResult BadRequestResult(IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> validationResults)
        {
            // ToDo : does this have to be logged ?
            var modelState = new ModelStateDictionary();
            foreach (var validationResult in validationResults)
            {
                modelState.AddModelError(String.Empty, validationResult.ErrorMessage);
            }
            return BadRequestResult(modelState);

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
                var error = new ServerErrorException(string.Format(message, args));
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = _exceptionMapper.Resolve(error).ToString() }.ToJson());
            }
            else
            {
                var errorMessage = String.Format(message, args);
                Logger.LogError("{0} : {1}", errorMessage, ExceptionHelper.GetAllToStrings(ex));
                var error = new ServerErrorException(string.Format("{0} : {1}", errorMessage, ex.Message));
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = _exceptionMapper.Resolve(error).ToString() }.ToJson());
            }
        }

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
            var error = new NotFoundException(string.Format(message, args));
            return new ObjectResult(_exceptionMapper.Resolve(error)) { StatusCode = 404 };
        }


        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        protected virtual ActionResult Success(string message)
        {
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = message }.ToJson());
        }
        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        protected virtual ActionResult Success(string message, object data)
        {
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = message, data = data }.ToJson());
        }
        /// <summary>
        /// 返回失败消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        protected virtual ActionResult Error(string message)
        {
            return Content(new AjaxResult { state = ResultType.error.ToString(), message = message }.ToJson());
        }
    }
}
