using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Internal;
using Microsoft.Extensions.DependencyInjection;
using SF.Core.Infrastructure.Plugins.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Core.Infrastructure.Plugins
{
    public class PluginRouteBuilder : IRouteBuilder
    {
        public PluginRouteBuilder(IApplicationBuilder applicationBuilder)
            : this(applicationBuilder, null)
        {
        }

        public PluginRouteBuilder(IApplicationBuilder applicationBuilder, IRouter defaultHandler)
        {

            if (applicationBuilder == null)
            {
                throw new ArgumentNullException(nameof(applicationBuilder));
            }

            if (applicationBuilder.ApplicationServices.GetService(typeof(RoutingMarkerService)) == null)
            {
                throw new InvalidOperationException();
            }


            ApplicationBuilder = applicationBuilder;
            DefaultHandler = defaultHandler;
            ServiceProvider = applicationBuilder.ApplicationServices;

            Routes = new List<IRouter>();
        }

        public IPluginManager PluginManager { get; set; }

        public IApplicationBuilder ApplicationBuilder { get; }

        public IRouter DefaultHandler { get; set; }

        public IServiceProvider ServiceProvider { get; }

        public IList<IRouter> Routes { get; }

        public IRouter Build()

        {
            var inlineConstraintResolver = ServiceProvider.GetService<IInlineConstraintResolver>();
            var routeCollection = new PluginRouteCollection(this.PluginManager, DefaultHandler, inlineConstraintResolver);

            foreach (var route in Routes)
            {
                routeCollection.Add(route);
            }

            return routeCollection;
        }
    }
}
