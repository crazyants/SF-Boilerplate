using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using SF.Core.Infrastructure.Plugins.Abstraction;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Core.Infrastructure.Plugins
{
    public class PluginActionDescriptorCollectionProvider : IActionDescriptorCollectionProvider
    {
        private readonly IServiceProvider _serviceProvider;
        private ActionDescriptorCollection _collection;
        private IPluginManager _pluginManager;
        private int _activePluginHash;

        public PluginActionDescriptorCollectionProvider(IServiceProvider serviceProvider, IPluginManager pluginManager)
        {
            _serviceProvider = serviceProvider;
            _pluginManager = pluginManager;
            _activePluginHash = -1;

        }

        public ActionDescriptorCollection ActionDescriptors
        {
            get
            {
                if (_collection == null || _activePluginHash != _pluginManager.ActivePluginHash)
                {
                    _collection = GetCollection();
                }

                return _collection;
            }
        }

        private ActionDescriptorCollection GetCollection()
        {
            var providers =
                _serviceProvider.GetServices<IActionDescriptorProvider>()
                                .OrderBy(p => p.Order)
                                .ToArray();

            var context = new ActionDescriptorProviderContext();

            foreach (var provider in providers)
            {
                provider.OnProvidersExecuting(context);
            }

            for (var i = providers.Length - 1; i >= 0; i--)
            {
                providers[i].OnProvidersExecuted(context);
            }

            _activePluginHash = _pluginManager.ActivePluginHash;

            return new ActionDescriptorCollection(
                new ReadOnlyCollection<ActionDescriptor>(context.Results), _activePluginHash);
        }
    }
}
