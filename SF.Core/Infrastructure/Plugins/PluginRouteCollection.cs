using Microsoft.AspNetCore.Routing;
using SF.Core.Infrastructure.Plugins.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Core.Infrastructure.Plugins
{

    public class PluginRouteCollection : RouteCollection, IRouteCollection
    {
        private List<IRouter> _routes = new List<IRouter>();
        private List<IRouter> _unnamedRoutes = new List<IRouter>();
        private Dictionary<string, INamedRouter> _namedRoutes =
                                    new Dictionary<string, INamedRouter>(StringComparer.OrdinalIgnoreCase);

        private int _pluginHash;
        private readonly IPluginManager _pluginManager;
        private readonly IRouter _defaultHander;
        private readonly IInlineConstraintResolver _constraintResolver;

        private List<IRouter> _initialRoutes = new List<IRouter>();

        public PluginRouteCollection(IPluginManager pluginManager, IRouter defaultHander,
            IInlineConstraintResolver inlineConstraintResolver) :
            base()
        {
            _pluginManager = pluginManager;
            _pluginHash = -1;
            _defaultHander = defaultHander;
            _constraintResolver = inlineConstraintResolver;
        }


        private RouteOptions _options;

        public new IRouter this[int index]
        {
            get { return _routes[index]; }
        }

        public new int Count
        {
            get { return _routes.Count; }
        }

        public new void Add(IRouter router)
        {
            if (router == null)
            {
                throw new ArgumentNullException(nameof(router));
            }

            var namedRouter = router as INamedRouter;
            if (namedRouter != null)
            {
                if (!string.IsNullOrEmpty(namedRouter.Name))
                {
                    _namedRoutes.Add(namedRouter.Name, namedRouter);
                }
            }
            else
            {
                _unnamedRoutes.Add(router);
            }

            
            _routes.Add(router);
        }

        public async override Task RouteAsync(RouteContext context)
        {
            if (_pluginHash != _pluginManager.ActivePluginHash)
            {
                GetRoutesFromPluginManager();
                _pluginHash = _pluginManager.ActivePluginHash;
            }


            // Perf: We want to avoid allocating a new RouteData for each route we need to process.
            // We can do this by snapshotting the state at the beginning and then restoring it
            // for each router we execute.
            var snapshot = context.RouteData.PushState(null, values: null, dataTokens: null);

            for (var i = 0; i < Count; i++)
            {
                var route = this[i];
                context.RouteData.Routers.Add(route);

                try
                {
                    await route.RouteAsync(context);

                    if (context.Handler != null)
                    {
                        break;
                    }
                }
                finally
                {
                    if (context.Handler == null)
                    {
                        snapshot.Restore();
                    }
                }
            }
        }

        private void GetRoutesFromPluginManager()
        {
            if (_pluginHash == -1)
            {
                _initialRoutes.AddRange(_routes);
            }

            _routes = new List<IRouter>();
            _unnamedRoutes = new List<IRouter>();
            _namedRoutes = new Dictionary<string, INamedRouter>();

            foreach (var route in _initialRoutes)
            {
                Add(route);
            }

            var routesFromPlugin = _pluginManager.GetActiveRoutesForPlugins(_defaultHander, _constraintResolver);
            foreach (var route in routesFromPlugin)
            {
                Add(route);
            }

        }

    }
}
