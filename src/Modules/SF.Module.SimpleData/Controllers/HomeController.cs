using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using SF.Module.SimpleData.Data;

namespace SF.Module.SimpleData.Controllers
{

    [Route("api/simple")]
    public class HomeController : Controller
    {
        private readonly ISimpleDataUnitOfWork _uitOfWork;

        public HomeController(ISimpleDataUnitOfWork uitOfWork)
        {
            _uitOfWork = uitOfWork;
        }


        public IActionResult Index()
        {
            var list = _uitOfWork.Activity.Query().ToList();
            return View();
        }


    }
}
