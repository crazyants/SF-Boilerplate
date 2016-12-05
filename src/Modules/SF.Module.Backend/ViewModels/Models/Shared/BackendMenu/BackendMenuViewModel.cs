 
using System.Collections.Generic;

namespace SF.Module.Backend.ViewModels.Shared
{
  public class BackendMenuViewModel : ViewModelBase
  {
    public IEnumerable<BackendMenuGroupViewModel> BackendMenuGroups { get; set; }
  }
}