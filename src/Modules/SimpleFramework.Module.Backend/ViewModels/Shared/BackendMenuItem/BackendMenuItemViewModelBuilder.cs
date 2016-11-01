

using SimpleFramework.Core.Web.UI.Backends;

namespace SimpleFramework.Module.Backend.ViewModels.Shared
{
    public class BackendMenuItemViewModelBuilder : ViewModelBuilderBase
    {
        public BackendMenuItemViewModelBuilder( )
          : base()
        {
        }

        public BackendMenuItemViewModel Build(BackendMenuItem backendMenuItem)
        {
            return new BackendMenuItemViewModel()
            {
                Url = backendMenuItem.Url,
                Name = backendMenuItem.Name,
                Position = backendMenuItem.Position
            };
        }
    }
}