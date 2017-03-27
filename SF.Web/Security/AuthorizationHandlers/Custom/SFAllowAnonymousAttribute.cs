using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Web.Security.AuthorizationHandlers.Custom
{
    /// <summary>
    /// Used to allow a method to be accessed by any user.
    /// Suppress <see cref="AbpAuthorizeAttribute"/> defined in the class containing that method.
    /// </summary>
    public class AbpAllowAnonymousAttribute : Attribute, ISFAllowAnonymousAttribute
    {

    }
}
