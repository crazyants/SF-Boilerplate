
using SF.Core;
using SF.Core.Web.UI.Backends;

namespace SF.Module.Backend
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
