
using SF.Core;
using SF.Core.Abstraction;
using SF.Core.Abstraction.UI.Backends;
using SF.Core.Infrastructure.Modules;
using SF.Web.Modules;
using System.Collections.Generic;
using System.Linq;

namespace SF.Web.Barebone.Backend.ViewModels
{
  public class BackendStyleSheetsViewModelBuilder : ViewModelBuilderBase
  {
    public BackendStyleSheetsViewModelBuilder()
      : base()
    {
    }

    public BackendStyleSheetsViewModel Build()
    {
      List<BackendStyleSheetViewModel> backendStyleSheetViewModels = new List<BackendStyleSheetViewModel>();

      foreach (IModuleInitializer extension in ExtensionManager.Extensions)
        if (extension is IModuleInitializer)
          if ((extension as IModuleInitializer).BackendMetadata != null && (extension as IModuleInitializer).BackendMetadata.BackendStyleSheets != null)
            foreach (BackendStyleSheet backendStyleSheet in (extension as IModuleInitializer).BackendMetadata.BackendStyleSheets)
              backendStyleSheetViewModels.Add(new BackendStyleSheetViewModelBuilder().Build(backendStyleSheet));

      return new BackendStyleSheetsViewModel()
      {
        BackendStyleSheets = backendStyleSheetViewModels.OrderBy(bss => bss.Position)
      };
    }
  }
}