using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SF.Core.Infrastructure.Plugins.Abstraction;

namespace SF.Core.Infrastructure.Plugins
{
    public static class MapPluginRoutesExtensions
    {

        public static void MapPluginRoute(this ICollection<IPluginRoute> routeCollections,
                                             string name,
                                             string template,
                                             IPlugin plugin)

        {
            MapPluginRoute(routeCollections, name, template, null, plugin);
        }

        public static void MapPluginRoute(this ICollection<IPluginRoute> routeCollections,
                                             string name,
                                             string template,
                                             object defaults,
                                             IPlugin plugin)
        {
             MapPluginRoute(routeCollections,name, template, defaults, null, plugin);
        }

        public static void MapPluginRoute(this ICollection<IPluginRoute> routeCollections,
                                             string name,
                                             string template,
                                             object defaults,
                                             object constraints,
                                             IPlugin plugin)
        {
            //var inlineConstraintResolver = routeCollectionBuilder.RouteBuider
            //     .ServiceProvider
            //     .GetRequiredService<IInlineConstraintResolver>();


            //routeCollectionBuilder.Add(new Route(
            //    name,
            //    template,
            //    new RouteValueDictionary(defaults),
            //    new RouteValueDictionary(constraints),
            //    new RouteValueDictionary(new { Namespace = plugin.AssemblyName }) ,
            //    inlineConstraintResolver));



            routeCollections.Add(new PluginRoute(
                name,
                template,
                new RouteValueDictionary(defaults),
                new RouteValueDictionary(constraints),
                new RouteValueDictionary(new { Namespace = plugin.AssemblyName })));

        }


    }
}
