using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace SimpleFramework.Web.BreadCrumb
{
    /// <summary>
    /// UrlHelper Extensions
    /// </summary>
    public static class UrlHelperExtensions
    {
        /// <summary>
        /// Creates a URL without its route values
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="action"></param>
        /// <param name="removeRouteValues"></param>
        /// <returns></returns>
        public static string ActionWithoutRouteValues(this IUrlHelper helper, string action, string[] removeRouteValues = null)
        {
            var routeValues = helper.ActionContext.RouteData.Values;
            var routeValueKeys = routeValues.Keys.Where(o => o != "controller" && o != "action").ToList();

            // Temporarily remove route values
            var oldRouteValues = new Dictionary<string, object>();

            foreach (var key in routeValueKeys)
            {
                if (removeRouteValues != null && !removeRouteValues.Contains(key))
                {
                    continue;
                }

                oldRouteValues[key] = routeValues[key];
                routeValues.Remove(key);
            }

            // Generate URL
            var url = helper.Action(action);

            // Reinsert route values
            foreach (var keyValuePair in oldRouteValues)
            {
                routeValues.Add(keyValuePair.Key, keyValuePair.Value);
            }

            return url;
        }
    }
}