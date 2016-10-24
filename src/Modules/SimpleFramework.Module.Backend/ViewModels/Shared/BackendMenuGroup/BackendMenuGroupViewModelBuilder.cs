


using SimpleFramework.Core.UI;

namespace SimpleFramework.Module.Backend.ViewModels.Shared
{
  public class BackendMenuGroupViewModelBuilder : ViewModelBuilderBase
  {
    public BackendMenuGroupViewModelBuilder( )
      : base()
    {
    }

    public BackendMenuGroupViewModel Build(BackendMenuGroup backendMenuGroup)
    {
      return new BackendMenuGroupViewModel()
      {
        Name = backendMenuGroup.Name,
        Position = backendMenuGroup.Position
      };
    }
  }
}