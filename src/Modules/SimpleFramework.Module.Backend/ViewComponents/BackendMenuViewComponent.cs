
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleFramework.Module.Backend.ViewModels.Shared;

namespace SimpleFramework.Module.Backend.ViewComponents
{
    public class BackendMenuViewComponent : ViewComponentBase
    {
        public BackendMenuViewComponent()
          : base()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return this.View(new BackendMenuViewModelBuilder().Build());
        }
    }
}