
using SF.Core.Abstraction.UI.Backends;

namespace SF.Web.Barebone.Backend.ViewModels
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