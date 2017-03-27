
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SF.Web.Barebone.Backend.ViewModels;

namespace SF.Web.Barebone.Backend.ViewComponents
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