
using SF.Core;
using SF.Core.Abstraction.UI.Backends;
using System.Collections.Generic;
using System.Linq;

namespace SF.Module.Backend.ViewModels.Shared
{
    public class BackendScriptsViewModelBuilder : ViewModelBuilderBase
    {
        public BackendScriptsViewModelBuilder( )
          : base( )
        {
        }

        public BackendScriptsViewModel Build()
        {
            List<BackendScriptViewModel> backendScriptViewModels = new List<BackendScriptViewModel>();

            foreach (IModuleInitializer extension in ExtensionManager.Extensions)
                if (extension is IModuleInitializer)
                    if ((extension as IModuleInitializer).BackendMetadata != null && (extension as IModuleInitializer).BackendMetadata.BackendScripts != null)
                        foreach (BackendScript backendScript in (extension as IModuleInitializer).BackendMetadata.BackendScripts)
                            backendScriptViewModels.Add(new BackendScriptViewModelBuilder().Build(backendScript));

            return new BackendScriptsViewModel()
            {
                BackendScripts = backendScriptViewModels.OrderBy(bs => bs.Position)
            };
        }
    }
}