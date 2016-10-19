using SimpleFramework.Infrastructure.Data;
using SimpleFramework.Core.Plugins.Abstraction;
using SimpleFramework.Core.Plugins.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleFramework.Core.Plugins.Services
{
    public class PluginService: IPluginService
    {
        private readonly IRepository<InstalledPlugin> _installedPluginRepository;

        public PluginService(IRepository<InstalledPlugin> installedPluginRepository)
        {
            this._installedPluginRepository = installedPluginRepository;
        }

        public IEnumerable<InstalledPlugin> AllInstalledPlugins()
        {
            return _installedPluginRepository.Query(x => x.Installed == true).Select();

        }
        public IEnumerable<InstalledPlugin> AllActivePlugins()
        {
            return _installedPluginRepository.Query(x => x.Installed && x.Active).Select();

        }
        public InstalledPlugin InstalledPluginForPlugin(IPlugin _plugin)
        {
            return _installedPluginRepository.Find(x => x.PluginAssemblyName == _plugin.AssemblyName);

        }
    }
}
