using System.Collections.Generic;
using SimpleFramework.Core.Plugins.Abstraction;
using SimpleFramework.Core.Plugins.Models;

namespace SimpleFramework.Core.Plugins.Services
{
    public interface IPluginService
    {
        IEnumerable<InstalledPlugin> AllActivePlugins();
        IEnumerable<InstalledPlugin> AllInstalledPlugins();
        InstalledPlugin InstalledPluginForPlugin(IPlugin _plugin);
    }
}