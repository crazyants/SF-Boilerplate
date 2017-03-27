using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SF.Module.Backend.ViewModels;
using Microsoft.Extensions.Localization;
using Audit.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;


namespace SF.Module.Backend.Controllers
{
    [Authorize(Roles = "Administrators")]
    //  [BreadCrumb(Title = "Home", UseDefaultRouteUrl = true, Order = 0)]
    public class HomeController : Controller
    {
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly IAuthorizationService _authorizationService;
        public HomeController( IStringLocalizer<HomeController> localizer, IAuthorizationService authorizationService)
        {
            _localizer = localizer;
            _authorizationService = authorizationService;
        }

         [Audit]
        // [BreadCrumb(Title = "Main index", Order = 1)]
        //[ValidateSFCaptcha(ErrorMessage = "Please enter the security code as a number.",
        //                    IsNumericErrorMessage = "The input value should be a number.",
        //                    CaptchaGeneratorLanguage = Language.English)]
        public IActionResult Index()
        {
            var model = new HomeViewModel();
            //测试多语言
            ViewData["Message"] = _localizer["Your application description page."];

            ViewData["Message"] = _localizer["EFString"];

            return View(model);
        }

        public IActionResult Default()
        {
            return View();
        }
        public IActionResult UEditor()
        {
            return View();
        }
        public IActionResult EditorMD()
        {
            return View();
        }
        /// <summary>
        /// 测试授权
        /// </summary>
        /// <returns></returns>
      //  [BreadCrumb(Title = "Authoer", Order = 3)]
        public async Task<ActionResult> Authoer()
        {
            if (!await _authorizationService.AuthorizeAsync(User, BackendPermissionProvider.DataItemAdd))
                return Unauthorized();


            return View();
        }

        /// <summary>
        /// 测试授权
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> AuthoerFilter()
        {
            if (!await _authorizationService.AuthorizeAsync(User, BackendPermissionProvider.DataItemEdit))
                return Unauthorized();


            return View();
        }
       // [BreadCrumb(Title = "Posts list", Order = 3)]
        public ActionResult Posts()
        {
            //this.SetCurrentBreadCrumbTitle("dynamic title 1");

            //this.AddBreadCrumb(new BreadCrumb
            //{
            //    Title = "Wiki",
            //    Url = string.Format("{0}?id=1", Url.Action("Index", "Home")),
            //    Order = 1
            //});
            //this.AddBreadCrumb(new BreadCrumb
            //{
            //    Title = "Lab",
            //    Url = string.Format("{0}?id=2", Url.Action("Index", "Home")),
            //    Order = 2
            //});

            return View();
        }

        public ActionResult NotFoundError()
        {
             
            return View();
        }
        public ActionResult InternalError()
        {

            return View();
        }
        public ActionResult AccessDenied()
        {

            return View();
        }
    }
}
