
using SF.Core.Plugins.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Core.Plugins
{
    public class PluginSettingsManager : IPluginSettingsManager
    {
        public bool AddSettings(string key, object value, IPlugin plugin)
        {
            throw new NotImplementedException();
        }

        public T GetSetting<T>(string key, IPlugin plugin)
        {
            throw new NotImplementedException();
        }

    }
}
