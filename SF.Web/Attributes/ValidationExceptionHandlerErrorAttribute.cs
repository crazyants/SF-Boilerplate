/*******************************************************************************
* 命名空间: SF.Web.Attributes
*
* 功 能： N/A
* 类 名： ValidationExceptionHandlerErrorAttribute
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/21 11:21:43 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SF.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Web.Attributes
{
    public class ValidationExceptionHandlerErrorAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            //only handle ValidationExceptions
            if ((context.Exception as ValidationException) != null)
            {
                var errorList = new List<ErrorViewModel>();
                foreach (var validationsfailures in (context.Exception as ValidationException).Errors)
                {
                    errorList.Add(new ErrorViewModel(validationsfailures.ErrorCode, validationsfailures.ErrorMessage));
                }
                var result = new JsonResult(errorList);
                result.ContentType = "application/json";
                // TODO: Pass additional detailed data via ViewData
                context.ExceptionHandled = true; // mark exception as handled
                context.Result = result;
                context.HttpContext.Response.StatusCode = 400;
            }
        }
    }
}
