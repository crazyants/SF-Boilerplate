using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleFramework.Core.Abstraction.Data;
using SimpleFramework.Core.Entitys;

namespace SimpleFramework.Module.Backend.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/widget-instances")]
    public class WidgetInstanceApiController : Controller
    {
        private readonly IRepository<WidgetInstanceEntity> _widgetInstanceRepository;
        private readonly IRepository<WidgetEntity> _widgetRespository;

        public WidgetInstanceApiController(IRepository<WidgetInstanceEntity> widgetInstanceRepository, IRepository<WidgetEntity> widgetRespository)
        {
            _widgetInstanceRepository = widgetInstanceRepository;
            _widgetRespository = widgetRespository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var widgetInstances = _widgetInstanceRepository.Query()
                .Include(x => x.Widget)
                .Include(x => x.WidgetZone)
                .Select(x => new
                {
                    Id = x.Id,
                    Name = x.Name,
                    WidgetType = x.Widget.Name,
                    WidgetZone = x.WidgetZone.Name,
                    CreatedOn = x.CreatedDate,
                    EditUrl = x.Widget.EditUrl,
                    PublishStart = x.PublishStart,
                    PublishEnd = x.PublishEnd
                }).ToList();

            return Json(widgetInstances);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var widgetInstance = _widgetInstanceRepository.Queryable().FirstOrDefault(x => x.Id == id);
            if (widgetInstance == null)
            {
                return NotFound();
            }

            _widgetInstanceRepository.Delete(widgetInstance);
            _widgetInstanceRepository.SaveChange();

            return Ok();
        }
    }
}
