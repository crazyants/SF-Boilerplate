using SF.Core.Infrastructure.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace SF.Web.Modules
{
    public interface IModuleManager
    {
        List<ModuleInfo> GetModule();
        void InstallModule(ModuleInfo module);
        void UnInstallModule(ModuleInfo module);
    }
}
