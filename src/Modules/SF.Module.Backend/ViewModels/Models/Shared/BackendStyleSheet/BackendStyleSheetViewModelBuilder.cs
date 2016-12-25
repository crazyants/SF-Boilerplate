


using SF.Core.Abstraction.UI.Backends;

namespace SF.Module.Backend.ViewModels.Shared
{
  public class BackendStyleSheetViewModelBuilder : ViewModelBuilderBase
  {
    public BackendStyleSheetViewModelBuilder()
      : base()
    {
    }

    public BackendStyleSheetViewModel Build(BackendStyleSheet backendStyleSheet)
    {
      return new BackendStyleSheetViewModel()
      {
        Url = backendStyleSheet.Url,
        Position = backendStyleSheet.Position
      };
    }
  }
}