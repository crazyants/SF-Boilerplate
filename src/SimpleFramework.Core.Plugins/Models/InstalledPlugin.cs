using SimpleFramework.Core.Abstraction.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleFramework.Core.Plugins.Models
{
    public class InstalledPlugin : EntityBase
    {
        public string PluginName { get; set; }

        public string PluginVersion { get; set; }

        public bool Installed { get; set; }

        public bool Active { get; set; }

        public string PluginAssemblyName { get; set; }

        public DateTime DateInstalled { get; set; }

        public DateTime DateActivated { get; set; }

        public DateTime DateDeactivated { get; set; }

    }
}
