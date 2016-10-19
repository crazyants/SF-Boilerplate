using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SimpleFramework.Core.Services;
using SimpleFramework.Module.Backend.ViewModels;
using Microsoft.Extensions.Localization;
using Audit.Mvc;
using Microsoft.AspNetCore.Authorization;
using SimpleFramework.Core.Security;
using System.Threading.Tasks;
using SimpleFramework.Core.Models;

namespace SimpleFramework.Module.Backend.Controllers
{

    public class HomeController : Controller
    {
        private IWidgetInstanceService _widgetInstanceService;
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly IAuthorizationService _authorizationService;
        public HomeController(IWidgetInstanceService widgetInstanceService,
               IStringLocalizer<HomeController> localizer, IAuthorizationService authorizationService)
        {
            _widgetInstanceService = widgetInstanceService;
            _localizer = localizer;
            _authorizationService = authorizationService;
        }
        [Audit]
        public IActionResult Index()
        {
            var model = new HomeViewModel();

            model.WidgetInstances = _widgetInstanceService.GetPublished().Select(x => new WidgetInstanceViewModel
            {
                Id = x.Id,
                Name = x.Name,
                ViewComponentName = x.Widget.ViewComponentName,
                WidgetId = x.WidgetId,
                WidgetZoneId = x.WidgetZoneId,
                Data = x.Data,
                HtmlData = x.HtmlData
            }).ToList();

            //测试多语言
            ViewData["Message"] = _localizer["Your application description page."];

            ViewData["Message"] = _localizer["EFString"];

            return View(model);
        }
        /// <summary>
        /// 测试授权
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Authoer()
        {
            if (!await _authorizationService.AuthorizeAsync(User, GobalPermissions.AccessAdminPanel))
                return Unauthorized();


            return View();
        }

        /// <summary>
        /// 测试授权
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> AuthoerFilter()
        {
            if (!await _authorizationService.AuthorizeAsync(User, GobalPermissions.AccessAdminPanel))
                return Unauthorized();


            return View();
        }

    }
}
