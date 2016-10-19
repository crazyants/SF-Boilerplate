using SimpleFramework.Infrastructure.UI;

namespace SimpleFramework.Module.Backend.ViewModels.Shared
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