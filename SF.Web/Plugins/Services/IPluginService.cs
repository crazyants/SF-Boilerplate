using System.Collections.Generic;
using SF.Core.Infrastructure.Plugins.Abstraction;
using SF.Core.Infrastructure.Plugins.Models;

namespace SF.Web.Plugins.Services
{
    public interface IPluginService
    {
        IEnumerable<InstalledPlugin> AllActivePlugins();
        IEnumerable<InstalledPlugin> AllInstalledPlugins();
        InstalledPlugin InstalledPluginForPlugin(IPlugin _plugin);
    }
}