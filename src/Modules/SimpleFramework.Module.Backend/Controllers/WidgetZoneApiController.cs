using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleFramework.Core.Abstraction.Data;
using SimpleFramework.Core.Entitys;

namespace SimpleFramework.Module.Backend.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/widget-zones")]
    public class WidgetZoneApiController : Controller
    {
        private readonly IRepository<WidgetZoneEntity> _widgetZoneRespository;

        public WidgetZoneApiController(IRepository<WidgetZoneEntity> widgetZoneRespository)
        {
            _widgetZoneRespository = widgetZoneRespository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var widgetZones = _widgetZoneRespository.Query().Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToList();

            return Json(widgetZones);
        }
    }
}
