using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;

namespace SF.Web.Security.Rendering
{
    /// <summary>
    /// Provides support for Content Security Policy Level 2 protected elements.
    /// </summary>
    public static class HtmlHelperContentSecurityPolicyExtensions
    {
        /// <summary>
        /// Writes an opening script tag to the response, and sets attributes related to Content Security Policy
        /// </summary>
        /// <param name="htmlHelper">The IHtmlHelper instance this method extends.</param>
        /// <returns></returns>
        public static IDisposable BeginCspScript(this IHtmlHelper htmlHelper)
        {
            return BeginCspScript(htmlHelper, null);
        }

        /// <summary>
        /// Writes an opening script tag to the response, and sets attributes related to Content Security Policy
        /// </summary>
        /// <param name="htmlHelper">The IHtmlHelper instance this method extends.</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element</param>
        /// <returns></returns>
        public static IDisposable BeginCspScript(this IHtmlHelper htmlHelper, object htmlAttributes)
        {
            return BeginCspScript(htmlHelper, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// Writes an opening script tag to the response, and sets attributes related to Content Security Policy
        /// </summary>
        /// <param name="htmlHelper">The IHtmlHelper instance this method extends.</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element</param>
        /// <returns></returns>
        public static IDisposable BeginCspScript(this IHtmlHelper htmlHelper, IDictionary<string, object> htmlAttributes)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException(nameof(htmlHelper));
            }

            return new ContentSecurityPolicyInlineElement(htmlHelper.ViewContext, ContentSecurityPolicyInlineElement.ScriptTagName, htmlAttributes);
        }

        /// <summary>
        /// Writes an opening style tag to the response, and sets attributes related to Content Security Policy
        /// </summary>
        /// <param name="htmlHelper">The IHtmlHelper instance this method extends</param>
        /// <returns></returns>
        public static IDisposable BeginCspStyle(this IHtmlHelper htmlHelper)
        {
            return BeginCspStyle(htmlHelper, null);
        }

        /// <summary>
        /// Writes an opening style tag to the response, and sets attributes related to Content Security Policy
        /// </summary>
        /// <param name="htmlHelper">The IHtmlHelper instance this method extends</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element</param>
        /// <returns></returns>
        public static IDisposable BeginCspStyle(this IHtmlHelper htmlHelper, object htmlAttributes)
        {
            return BeginCspStyle(htmlHelper, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// Writes an opening style tag to the response, and sets attributes related to Content Security Policy
        /// </summary>
        /// <param name="htmlHelper">The IHtmlHelper instance this method extends</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element</param>
        /// <returns></returns>
        public static IDisposable BeginCspStyle(this IHtmlHelper htmlHelper, IDictionary<string, object> htmlAttributes)
        {
            return new ContentSecurityPolicyInlineElement(htmlHelper.ViewContext, ContentSecurityPolicyInlineElement.StyleTagName, htmlAttributes);
        }
    }
}
