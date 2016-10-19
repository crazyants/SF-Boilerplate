 
using System.Collections.Generic;

namespace SimpleFramework.Module.Backend.ViewModels.Shared
{
  public class BackendMenuGroupViewModel : ViewModelBase
  {
    public string Name { get; set; }
    public int Position { get; set; }
    public IEnumerable<BackendMenuItemViewModel> BackendMenuItems { get; set; }
  }
}