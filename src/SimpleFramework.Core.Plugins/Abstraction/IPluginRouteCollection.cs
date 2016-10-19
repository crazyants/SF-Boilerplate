using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleFramework.Core.Plugins.Abstraction
{
   public interface IPluginRoutes
    {
        IRouteBuilder RouteBuider { get; }
    }
}
