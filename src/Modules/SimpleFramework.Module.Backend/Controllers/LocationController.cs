using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SimpleFramework.Core.Abstraction.Data;
using SimpleFramework.Core.Entitys;

namespace SimpleFramework.Module.Backend.Controllers
{
    public class LocationController : Controller
    {
        private readonly IRepository<DistrictEntity> districtRepository;

        public LocationController(IRepository<DistrictEntity> districtRepository)
        {
            this.districtRepository = districtRepository;
        }

        [Route("location/getdistricts/{stateOrProvinceId}")]
        public IActionResult GetDistricts(long stateOrProvinceId)
        {
            var districts = districtRepository
                .Queryable()
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
