using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SimpleFramework.Core.Abstraction.Data;
using SimpleFramework.Core.Entitys;
using SimpleFramework.Core.Data;

namespace SimpleFramework.Module.Backend.Controllers
{
    public class LocationController : Controller
    {
        private readonly IBaseUnitOfWork _baseUnitOfWork;

        public LocationController(IBaseUnitOfWork baseUnitOfWork)
        {
            this._baseUnitOfWork = baseUnitOfWork;
        }

        [Route("location/getdistricts/{stateOrProvinceId}")]
        public IActionResult GetDistricts(long stateOrProvinceId)
        {
            var districts = _baseUnitOfWork.BaseWorkArea.District.Query()
                .Where(x => x.StateOrProvinceId == stateOrProvinceId)
                .OrderBy(x => x.Name)
                .Select(x => new
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();

            return Json(districts);
        }
    }
}
