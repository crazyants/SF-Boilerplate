 
using System.Collections.Generic;

namespace SF.Web.Barebone.Backend.ViewModels
{
  public class BackendMenuViewModel : ViewModelBase
  {
    public IEnumerable<BackendMenuGroupViewModel> BackendMenuGroups { get; set; }
  }
}