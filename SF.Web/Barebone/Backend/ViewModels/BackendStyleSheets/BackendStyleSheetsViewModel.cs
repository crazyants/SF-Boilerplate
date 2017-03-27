 
using System.Collections.Generic;

namespace SF.Web.Barebone.Backend.ViewModels
{
  public class BackendStyleSheetsViewModel : ViewModelBase
  {
    public IEnumerable<BackendStyleSheetViewModel> BackendStyleSheets { get; set; }
  }
}