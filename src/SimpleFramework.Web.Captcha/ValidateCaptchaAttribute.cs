using System;
using SimpleFramework.Web.Captcha.Contracts;
using SimpleFramework.Web.Captcha.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleFramework.Core.Common;

namespace SimpleFramework.Web.Captcha
{
    /// <summary>
    /// Validate SFCaptcha Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class ValidateCaptchaAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// The language of captcha generator. It's default value is Persian.
        /// </summary>
        public Language CaptchaGeneratorLanguage { set; get; } = Language.Persian;

        /// <summary>
        /// Validation error message. It's default value is `لطفا کد امنیتی را به رقم وارد نمائید`.
        /// </summary>
        public string ErrorMessage { set; get; } = "لطفا کد امنیتی را به رقم وارد نمائید";

        /// <summary>
        /// The input number is not numeric error message. It's default value is `لطفا در قسمت کد امنیتی تنها عدد وارد نمائید`.
        /// </summary>
        public string IsNumericErrorMessage { set; get; } = "لطفا در قسمت کد امنیتی تنها عدد وارد نمائید";

        /// <summary>
        /// Captcha validator.
        /// </summary>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.CheckArgumentNull(nameof(filterContext));

            var loggerFactory= filterContext.HttpContext.RequestServices.GetService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<ValidateCaptchaAttribute>();

            if (!shouldValidate(filterContext))
            {
                logger.LogWarning($"Ignoring ValidateSFCaptcha during `{filterContext.HttpContext.Request.Method}`.");
                base.OnActionExecuting(filterContext);
                return;
            }

            var controllerBase = filterContext.Controller as ControllerBase;
            controllerBase.CheckArgumentNull(nameof(controllerBase));

            var form = filterContext.HttpContext.Request.Form;
            form.CheckArgumentNull(nameof(form));

            var captchaText = (string)form[CaptchaTagHelper.CaptchaHiddenInputName];
            if (string.IsNullOrEmpty(captchaText))
            {
                logger.LogWarning("CaptchaHiddenInput is empty.");
                controllerBase.ModelState.AddModelError(CaptchaTagHelper.CaptchaInputName, ErrorMessage);
                base.OnActionExecuting(filterContext);
                return;
            }

            var inputText = (string)form[CaptchaTagHelper.CaptchaInputName];
            if (string.IsNullOrEmpty(inputText))
            {
                logger.LogWarning("CaptchaInput is empty.");
                controllerBase.ModelState.AddModelError(CaptchaTagHelper.CaptchaInputName, ErrorMessage);
                base.OnActionExecuting(filterContext);
                return;
            }

            long inputNumber;
            if (!long.TryParse(inputText, out inputNumber))
            {
                logger.LogWarning("inputText is not a number.");
                controllerBase.ModelState.AddModelError(CaptchaTagHelper.CaptchaInputName, IsNumericErrorMessage);
                base.OnActionExecuting(filterContext);
                return;
            }

            var captchaEncryption = filterContext.HttpContext.RequestServices.GetService<ICaptchaProtectionProvider>();
            var decryptedText = captchaEncryption.Decrypt(captchaText);

            var humanReadableIntegerProvider = filterContext.HttpContext.RequestServices.GetService<IHumanReadableIntegerProvider>();
            var numberToText = humanReadableIntegerProvider.NumberToText(inputNumber, CaptchaGeneratorLanguage);
            if (decryptedText == null || !decryptedText.Equals(numberToText))
            {
                logger.LogWarning($"{decryptedText} != {numberToText}");
                controllerBase.ModelState.AddModelError(CaptchaTagHelper.CaptchaInputName, ErrorMessage);
                base.OnActionExecuting(filterContext);
                return;
            }

            if (!isValidCookie(filterContext.HttpContext, decryptedText, logger))
            {
                controllerBase.ModelState.AddModelError(CaptchaTagHelper.CaptchaInputName, ErrorMessage);
                base.OnActionExecuting(filterContext);
                return;
            }

            base.OnActionExecuting(filterContext);
        }

        private static bool isValidCookie(HttpContext context, string decryptedText, ILogger<ValidateCaptchaAttribute> logger)
        {
            var cookieToken = (string)context.Request.Form[CaptchaTagHelper.CaptchaHiddenTokenName];
            if (string.IsNullOrEmpty(cookieToken))
            {
                logger.LogWarning("CaptchaHiddenTokenName is empty.");
                return false;
            }

            var captchaEncryption = context.RequestServices.GetService<ICaptchaProtectionProvider>();
            cookieToken = captchaEncryption.Decrypt(cookieToken);
            if (string.IsNullOrEmpty(cookieToken))
            {
                logger.LogWarning("CaptchaHiddenTokenName is invalid.");
                return false;
            }

            var captchaStorageProvider = context.RequestServices.GetService<ICaptchaStorageProvider>();
            var cookieValue = captchaStorageProvider.GetValue(context.Request.HttpContext, cookieToken);
            if(string.IsNullOrWhiteSpace(cookieValue))
            {
                logger.LogWarning("isValidCookie:: cookieValue IsNullOrWhiteSpace.");
                return false;
            }

            var result = cookieValue.Equals(decryptedText);
            if(!result)
            {
                logger.LogWarning($"isValidCookie:: {cookieValue} != {decryptedText}");
            }
            return result;
        }

        private static bool shouldValidate(ActionContext context)
        {
            return string.Equals("POST", context.HttpContext.Request.Method, StringComparison.OrdinalIgnoreCase);
        }
    }
}