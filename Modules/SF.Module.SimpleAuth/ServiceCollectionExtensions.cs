
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using SF.Module.SimpleAuth.Controllers;
using SF.Module.SimpleAuth.Models;
using SF.Module.SimpleAuth.Services;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
      
        /// <summary>
        /// This method adds an embedded file provider to the RazorViewOptions to be able to load the SimpleAuth related views.
        /// If you download and install the views below your view folder you don't need this method and you can customize the views.
        /// You can get the views from https://github.com/joeaudette/cloudscribe.Web.SimpleAuth/tree/master/src/cloudscribe.Web.SimpleAuth/Views
        /// </summary>
        /// <param name="options"></param>
        /// <returns>RazorViewEngineOptions</returns>
        public static RazorViewEngineOptions AddEmbeddedViewsForSimpleAuth(this RazorViewEngineOptions options)
        {
            options.FileProviders.Add(new EmbeddedFileProvider(
                    typeof(LoginController).GetTypeInfo().Assembly,
                    "SF.Module.SimpleAuth"
                ));

            return options;
        }
    }
}
