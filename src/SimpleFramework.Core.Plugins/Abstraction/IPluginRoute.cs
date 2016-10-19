using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleFramework.Core.Plugins.Abstraction
{
    public interface IPluginRoute
    {
         string RouteName { get; }
         string RouteTemplate { get; }
         RouteValueDictionary Defaults { get; }
         IDictionary<string, object> Constraints { get; }
         RouteValueDictionary DataTokens { get; }
    }
}
