using System.Collections.Generic;
using SF.Core.Plugins.Abstraction;
using SF.Core.Plugins.Models;

namespace SF.Core.Plugins.Services
{
    public interface IPluginService
    {
        IEnumerable<InstalledPlugin> AllActivePlugins();
        IEnumerable<InstalledPlugin> AllInstalledPlugins();
        InstalledPlugin InstalledPluginForPlugin(IPlugin _plugin);
    }
}