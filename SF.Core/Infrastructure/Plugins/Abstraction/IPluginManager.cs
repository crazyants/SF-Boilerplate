using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SF.Core.Infrastructure.Plugins.Abstraction
{
    public interface IPluginManager
    {
        int ActivePluginHash { get; }

        IList<IPlugin> AvailablePlugins { get; }

        IList<IPlugin> InstalledPlugins { get; }

        IList<IPlugin> ActivePlugins { get; }

        IList<Tuple<IPlugin, IList<Assembly>>> AvailablePluginAssemblies { get; }

        IList<Tuple<IPlugin, IList<Assembly>>> InstalledPluginAssemblies { get; }

        IList<Tuple<IPlugin, IList<Assembly>>> ActivePluginAssemblies { get; }

        IList<Assembly> ActiveAssemblies { get; }

        void ActivatePlugin(IPlugin plugin);

        void DeactivatePlugin(IPlugin plugin);

        ICollection<IRouter> GetActiveRoutesForPlugins(IRouter defaultHandler, IInlineConstraintResolver inlineConstraintResolver);
    }
}
