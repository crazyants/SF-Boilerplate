 
using System.Collections.Generic;

namespace SF.Module.Backend.ViewModels.Shared
{
  public class BackendScriptsViewModel : ViewModelBase
  {
    public IEnumerable<BackendScriptViewModel> BackendScripts { get; set; }
  }
}