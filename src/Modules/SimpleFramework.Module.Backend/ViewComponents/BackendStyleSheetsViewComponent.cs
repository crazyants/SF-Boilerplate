using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleFramework.Module.Backend.ViewModels.Shared;

namespace SimpleFramework.Module.Backend.ViewComponents
{
    public class BackendStyleSheetsViewComponent : ViewComponentBase
    {
        public BackendStyleSheetsViewComponent()
          : base()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return this.View(new BackendStyleSheetsViewModelBuilder().Build());
        }
    }
}