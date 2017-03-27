/*******************************************************************************
* 命名空间: SF.Web.Attributes
*
* 功 能： N/A
* 类 名： ApiExceptionFilter
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/2 11:19:18 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using SF.Core.Common;
using SF.Core.Errors.Exceptions;
using SF.Core.Extensions;
using SF.Web.Extensions;
using SF.Web.Models;
using System;
using System.Net;

namespace SF.Web.Attributes
{
    public class HandlerExceptionFilter : ExceptionFilterAttribute
    {
        private ILogger<HandlerExceptionFilter> _Logger;
        public HandlerExceptionFilter(ILogger<HandlerExceptionFilter> logger)
        {
            _Logger = logger;
        }
        public override void OnException(ExceptionContext context)
        {
           
            var msg = "";
            if (context.Exception is UnauthorizedAccessException)
            {
                msg = "Unauthorized Access";
                
                // handle logging here
                _Logger.LogWarning("Unauthorized Access in Controller Filter.");
            }
            else
            {
                // Unhandled errors
#if !DEBUG
                 msg = "An unhandled error occurred.";                
                string stack = null;
#else
                msg = context.Exception.GetBaseException().Message;
                string stack = context.Exception.StackTrace;
#endif
               
                // handle logging here
                _Logger.LogWarning($"Application thrown error: { msg}", context.Exception);
            }
            context.HttpContext.Response.StatusCode = context.Exception.GetStatusCode(context.HttpContext);
            _Logger.LogError(new EventId(0), context.Exception, msg);
            if (context.HttpContext.Request.IsAjaxRequest())
                context.Result = new ContentResult() { Content = (new AjaxResult { state = ResultType.error.ToString(), message = msg }).ToJson() };

            base.OnException(context);
        }

      
    }
}
