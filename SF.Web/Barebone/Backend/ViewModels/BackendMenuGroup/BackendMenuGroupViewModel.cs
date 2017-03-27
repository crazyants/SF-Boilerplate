 
using System.Collections.Generic;

namespace SF.Web.Barebone.Backend.ViewModels
{
  public class BackendMenuGroupViewModel : ViewModelBase
  {
    public string Name { get; set; }
    public int Position { get; set; }
    public IEnumerable<BackendMenuItemViewModel> BackendMenuItems { get; set; }
  }
}