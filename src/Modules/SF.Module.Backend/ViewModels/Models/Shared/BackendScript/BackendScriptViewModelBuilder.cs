

using SF.Core.Abstraction.UI.Backends;

namespace SF.Module.Backend.ViewModels.Shared
{
    public class BackendScriptViewModelBuilder : ViewModelBuilderBase
    {
        public BackendScriptViewModelBuilder( )
          : base()
        {
        }

        public BackendScriptViewModel Build(BackendScript backendScript)
        {
            return new BackendScriptViewModel()
            {
                Url = backendScript.Url,
                Position = backendScript.Position
            };
        }
    }
}