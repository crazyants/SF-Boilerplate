using SimpleFramework.Core.Abstraction.Data;
using SimpleFramework.Core.Plugins.Abstraction;
using SimpleFramework.Core.Plugins.Data;
using SimpleFramework.Core.Plugins.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleFramework.Core.Plugins.Services
{
    public class PluginService : IPluginService
    {
        private readonly IPluginsUnitOfWork _unitOfWork;

        public PluginService(IPluginsUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<InstalledPlugin> AllInstalledPlugins()
        {
            return _unitOfWork.Plugin.QueryFetching(x => x.Installed == true);

        }
        public IEnumerable<InstalledPlugin> AllActivePlugins()
        {
            return _unitOfWork.Plugin.QueryFetching(x => x.Installed && x.Active);

        }
        public InstalledPlugin InstalledPluginForPlugin(IPlugin _plugin)
        {
            return _unitOfWork.Plugin.Query().First(x => x.PluginAssemblyName == _plugin.AssemblyName);

        }
    }
}
