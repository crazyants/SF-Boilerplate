using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SF.Module.Blog.Data.Uow;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SF.Module.Blog.Controllers
{
    [Authorize(Roles = "Administrators")]
    [Route("ManagePost/")]
    public class ManagePostController : Controller //SF.Web.Base.Controllers.ControllerBase
    {
        private readonly IBlogUnitOfWork _blogUnitOfWork;
        public ManagePostController(IServiceCollection collection, ILogger<ManagePostController> logger,
           IBlogUnitOfWork blogUnitOfWork)//:/*base(collection, logger)*/
        {
            _blogUnitOfWork = blogUnitOfWork;
        }

        public IActionResult Index() {
            return View("Index");
        }

        [Route("Form")] 
        public IActionResult Form()
        {
            return View();
        }
    }
}