using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using SF.Web.Security.AuthorizationHandlers.Custom;

namespace SF.Module.Demo.Controllers
{
    [Route("Demo")]
    public class DemoController : Controller
    {
        private readonly IAuthorizationService _authorizationService;

        public DemoController(IAuthorizationService authorizationService)
        {

            _authorizationService = authorizationService;
        }

        public IActionResult Index()
        {

            return View();
        }
        [Authorize(Roles = "Administrators")]
        [SFMvcAuthorize("DataItem.Add")]
        [SFMvcAuthorize(true, "DataItem.Add", "DataItem.Delete")]
        [Route("Authorize")]
        public async Task<ActionResult> Authorize()
        {
            if (!await _authorizationService.AuthorizeAsync(User, BackendPermissionProvider.Super))
                return Unauthorized();
            return View();
        }


    }
}
