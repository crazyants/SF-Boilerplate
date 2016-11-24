
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SF.Module.Backend.ViewModels.Shared;

namespace SF.Module.Backend.ViewComponents
{
  public class BackendScriptsViewComponent : ViewComponentBase
  {
    public BackendScriptsViewComponent( )
      : base()
    {
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
      return this.View(new BackendScriptsViewModelBuilder().Build());
    }
  }
}