 
using System.Collections.Generic;

namespace SF.Module.Backend.ViewModels.Shared
{
  public class BackendStyleSheetsViewModel : ViewModelBase
  {
    public IEnumerable<BackendStyleSheetViewModel> BackendStyleSheets { get; set; }
  }
}