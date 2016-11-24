using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.Localization
{
    public class GlobalResourceOptions
    {
        /// <summary>
        /// if true (the default) then the GlobalResourceStringLocalizer will first try to get the localized string from global resources,
        /// ie stored in the resx folder of the main web app, only if it is not found will it fallback to try to get the resource from embedded resources inside the
        /// class library. This allow snot only localization but customization because you can drop in a resx file to set the strings
        /// and it will be used in preference to any embedded resource.
        /// set to false if you only want to use global resources as fallback, in that case it will try first to get the resource from resx embedded in the lib and only
        /// if not found will it try the global resources
        /// </summary>
        public bool TryGlobalFirst { get; set; } = true;
    }
}
