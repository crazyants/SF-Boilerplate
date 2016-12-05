 
using System.Collections.Generic;

namespace SF.Module.Backend.ViewModels.Shared
{
  public class BackendMenuGroupViewModel : ViewModelBase
  {
    public string Name { get; set; }
    public int Position { get; set; }
    public IEnumerable<BackendMenuItemViewModel> BackendMenuItems { get; set; }
  }
}