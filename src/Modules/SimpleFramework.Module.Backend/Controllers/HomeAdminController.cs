using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SimpleFramework.Module.Backend.Controllers
{

    [Authorize(Roles = "admin")]
    public class HomeAdminController : Controller
    {
        public HomeAdminController()
        {

        }
        [Route("admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
