using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SF.Web.Security.Attributes;

namespace SF.Web.Security.Filters
{
    public class AdminZoneFilter : IActionFilter, IFilterMetadata
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller as Controller;
            if (controller != null)
            {
                if(controller.GetType().Name.StartsWith("Admin"))
                {
                    AdminAttribute.Apply(context.HttpContext);
                }
            }
        }
    }
}
