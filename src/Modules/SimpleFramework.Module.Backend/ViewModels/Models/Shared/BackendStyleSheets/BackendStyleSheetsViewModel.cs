 
using System.Collections.Generic;

namespace SimpleFramework.Module.Backend.ViewModels.Shared
{
  public class BackendStyleSheetsViewModel : ViewModelBase
  {
    public IEnumerable<BackendStyleSheetViewModel> BackendStyleSheets { get; set; }
  }
}