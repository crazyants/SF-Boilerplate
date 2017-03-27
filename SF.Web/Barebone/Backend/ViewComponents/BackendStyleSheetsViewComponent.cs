using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SF.Web.Barebone.Backend.ViewModels;

namespace SF.Web.Barebone.Backend.ViewComponents
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