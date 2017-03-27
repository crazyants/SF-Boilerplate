using Microsoft.AspNetCore.Routing;
using SF.Core.Infrastructure.Plugins.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Core.Infrastructure.Plugins
{


    public class PluginRoute : IPluginRoute
    {
        private readonly string _routeName;
        private readonly string _routeTemplate;
        private readonly RouteValueDictionary _defaults;
        private readonly IDictionary<string, object> _constraints;
        private readonly RouteValueDictionary _dataTokens;

        public string RouteName { get { return _routeName; } }
        public string RouteTemplate { get { return _routeTemplate; } }
        public RouteValueDictionary Defaults { get { return _defaults; } }
        public IDictionary<string, object> Constraints { get { return _constraints; } }
        public RouteValueDictionary DataTokens { get { return _dataTokens; } }


        public PluginRoute(
            string routeTemplate)
            : this(
                routeTemplate,
                defaults: null,
                constraints: null,
                dataTokens: null
              )
        {
        }

        public PluginRoute(
            string routeTemplate,
            RouteValueDictionary defaults,
            IDictionary<string, object> constraints,
            RouteValueDictionary dataTokens)
            : this(null, routeTemplate, defaults, constraints, dataTokens)
        {
        }

        public PluginRoute(
            string routeName,
            string routeTemplate,
            RouteValueDictionary defaults,
            IDictionary<string, object> constraints,
            RouteValueDictionary dataTokens)
        {
            _routeName = routeName;
            _routeTemplate = routeTemplate;
            _defaults = defaults;
            _constraints = constraints;
            _dataTokens = dataTokens;
        }



    }
}
