/*******************************************************************************
* 命名空间: SimpleFramework.Core.Web.Attributes
*
* 功 能： N/A
* 类 名： ApiExceptionFilter
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/2 11:19:18 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using SimpleFramework.Core.Common;
using System;

namespace SimpleFramework.Core.Web.Attributes
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
                context.HttpContext.Response.StatusCode = 401;

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
                context.HttpContext.Response.StatusCode = 500;

                // handle logging here
                _Logger.LogWarning($"Application thrown error: { msg}", context.Exception);
            }
            _Logger.LogError(new EventId(0), context.Exception, msg);
            // always return a JSON result
            context.Result = new ContentResult() { Content = (new AjaxResult { state = ResultType.error.ToString(), message = context.Exception.Message }).ToJson() };

            base.OnException(context);
        }
    }
}
