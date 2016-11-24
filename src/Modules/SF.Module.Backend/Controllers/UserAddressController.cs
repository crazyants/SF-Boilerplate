using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SF.Core.Abstraction.Data;
using SF.Core.Entitys;
using SF.Module.Backend.ViewModels;
using SF.Core.Extensions;
using SF.Core.Data;
using SF.Core.WorkContexts;

namespace SF.Module.Backend.Controllers
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
