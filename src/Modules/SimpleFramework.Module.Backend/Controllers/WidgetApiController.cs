using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleFramework.Infrastructure.Data;
using SimpleFramework.Core.Entitys;

namespace SimpleFramework.Module.Backend.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/widgets")]
    public class WidgetApiController : Controller
    {
        private readonly IRepository<WidgetEntity> _widgetRespository;

        public WidgetApiController(IRepository<WidgetEntity> widgetRespository)
        {
            _widgetRespository = widgetRespository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var widgets = _widgetRespository.Query().Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
                CreateUrl = x.CreateUrl
            }).ToList();

            return Json(widgets);
        }
    }
}
