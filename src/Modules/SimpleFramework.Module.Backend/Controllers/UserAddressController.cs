using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleFramework.Core.Abstraction.Data;
using SimpleFramework.Core.Entitys;
using SimpleFramework.Module.Backend.ViewModels;
using SimpleFramework.Core.Extensions;
using SimpleFramework.Core.Data;

namespace SimpleFramework.Module.Backend.Controllers
{
    [Authorize]
    public class UserAddressController : Controller
    {
        private IBaseUnitOfWork _baseUnitOfWork;
        private IWorkContext _workContext;

        public UserAddressController(IBaseUnitOfWork baseUnitOfWork, IWorkContext workContext)
        {
            _baseUnitOfWork = baseUnitOfWork;
            _workContext = workContext;
        }

        [Route("user/address")]
        public async Task<IActionResult> List()
        {
            var currentUser = await _workContext.GetCurrentUser();
            var model = _baseUnitOfWork.BaseWorkArea.UserAddress
                .Query()
                .Where(x => x.AddressType == AddressType.Shipping && x.UserId == currentUser.Id)
                .Select(x => new UserAddressListItem
                {
                    UserAddressId = x.Id,
                    ContactName = x.Address.ContactName,
                    Phone = x.Address.Phone,
                    AddressLine1 = x.Address.AddressLine1,
                    AddressLine2 = x.Address.AddressLine1,
                    DistrictName = x.Address.District.Name,
                    StateOrProvinceName = x.Address.StateOrProvince.Name,
                    CountryName = x.Address.Country.Name
                }).ToList();

            return View(model);
        }
    }
}
