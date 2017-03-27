 
using System.Collections.Generic;

namespace SF.Web.Barebone.Backend.ViewModels
{
  public class BackendScriptsViewModel : ViewModelBase
  {
    public IEnumerable<BackendScriptViewModel> BackendScripts { get; set; }
  }
}