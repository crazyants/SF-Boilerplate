using SF.Entitys.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace SF.Core.Infrastructure.Modules
{

    public class InstalledModule : BaseEntity
    {
        public string ModuleName { get; set; }

        public string ModuleVersion { get; set; }

        public bool Installed { get; set; }

        public bool Active { get; set; }

        public string ModuleAssemblyName { get; set; }

        public DateTime DateInstalled { get; set; }

        public DateTime DateActivated { get; set; }

        public DateTime DateDeactivated { get; set; }

    }
}
