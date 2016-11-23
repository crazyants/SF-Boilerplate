 
using System.Collections.Generic;

namespace SimpleFramework.Module.Backend.ViewModels.Shared
{
  public class BackendScriptsViewModel : ViewModelBase
  {
    public IEnumerable<BackendScriptViewModel> BackendScripts { get; set; }
  }
}