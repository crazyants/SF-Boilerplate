using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Core.Infrastructure.Plugins.Abstraction
{
    public interface IPluginSettingsManager
    {
        bool AddSettings(string key, object value, IPlugin plugin);

        T GetSetting<T>(string key, IPlugin plugin);

    }
}
