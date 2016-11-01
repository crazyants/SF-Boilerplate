
using SimpleFramework.Core;
using SimpleFramework.Core.Web.UI.Backends;

namespace SimpleFramework.Module.Backend
{
    public class ModuleInitializer : ModuleInitializerBase, IModuleInitializer
    {
        public override IBackendMetadata BackendMetadata
        {
            get
            {
                return new BackendMetadata();
            }
        }

    }
}
