using Microsoft.AspNetCore.Routing;
using SF.Core.Infrastructure.Plugins.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Core.Infrastructure.Plugins
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
