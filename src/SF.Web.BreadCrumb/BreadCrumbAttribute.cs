using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace SF.Web.BreadCrumb
{
    /// <summary>
    /// BreadCrumb Action Filter. It can be applied to action methods or controllers.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class BreadCrumbAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Use this property to remove all of the previous items of the current stack
        /// </summary>
        public bool ClearStack { get; set; }

        /// <summary>
        /// An optional glyph icon of the current item
        /// </summary>
        public string GlyphIcon { get; set; }

        /// <summary>
        /// If UseDefaultRouteUrl is set to true, this property indicated all of the route items should be removed from the final URL
        /// </summary>
        public bool RemoveAllDefaultRouteValues { get; set; }

        /// <summary>
        /// If UseDefaultRouteUrl is set to true, this property indicated which route items should be removed from the final URL
        /// </summary>
        public string[] RemoveRouteValues { get; set; }

        /// <summary>
        /// Title of the current item
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// A constant URL of the current item. If UseDefaultRouteUrl is set to true, its value will be ignored
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// This property is useful when you need a back functionality. If it's true, the Url value will be previous Url using UrlReferrer
        /// </summary>
        public bool UsePreviousUrl { get; set; }

        /// <summary>
        /// This property is useful for controller level bread crumbs. If it's true, the Url value will be calculated automatically from the DefaultRoute
        /// </summary>
        public bool UseDefaultRouteUrl { get; set; }

        /// <summary>
        /// Adds the current item to the stack
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (shouldIgnore(filterContext))
            {
                return;
            }

            if (ClearStack)
            {
                filterContext.HttpContext.ClearBreadCrumbs();
            }

            var url = string.IsNullOrWhiteSpace(Url) ? filterContext.HttpContext.Request.GetEncodedUrl() : Url;

            if (UseDefaultRouteUrl)
            {
                url = getDefaultControllerActionUrl(filterContext);
            }

            if (UsePreviousUrl)
            {
                url = filterContext.HttpContext.Request.Headers["Referrer"];
            }

            filterContext.HttpContext.AddBreadCrumb(new BreadCrumb
            {
                Url = url,
                Title = Title,
                Order = Order,
                GlyphIcon = GlyphIcon
            });

            base.OnActionExecuting(filterContext);
        }

        private static bool shouldIgnore(ActionExecutingContext filterContext)
        {
            return !string.Equals(filterContext.HttpContext.Request.Method, "GET", StringComparison.OrdinalIgnoreCase);
        }

        private string getDefaultControllerActionUrl(ActionExecutingContext filterContext)
        {
            var defaultRoute = filterContext.RouteData.Routers.OfType<Route>().FirstOrDefault();
            if (defaultRoute == null)
            {
                throw new InvalidOperationException("The default route of this controller not found.");
            }

            var defaultAction = defaultRoute.Defaults["action"] as string;
            if (defaultAction == null)
            {
                throw new InvalidOperationException("The default action of this controller not found.");
            }

            if (RemoveAllDefaultRouteValues)
            {
                return new UrlHelper(filterContext).ActionWithoutRouteValues(defaultAction);
            }

            if (RemoveRouteValues == null || !RemoveRouteValues.Any())
            {
                return new UrlHelper(filterContext).Action(defaultAction);
            }

            return new UrlHelper(filterContext).ActionWithoutRouteValues(defaultAction, RemoveRouteValues);
        }
    }
}