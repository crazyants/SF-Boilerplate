using Microsoft.AspNetCore.Routing;
using SimpleFramework.Core.Plugins.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleFramework.Core.Plugins
{
    public class PluginRoutes : IPluginRoutes
    {
        private IRouteBuilder _routeBuilder;

        public PluginRoutes(IRouteBuilder routeBuilder)
        {
            _routeBuilder = routeBuilder;
        }

        public IRouteBuilder RouteBuider
        {
            get
            {
                return _routeBuilder;
            }
        }
    }
}
