using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SF.Core.Abstraction.Data;
using SF.Core.Entitys;
using SF.Module.Backend.Converters.Settings;
using webModel = SF.Module.Backend.ViewModels.Setting;
using System.Threading.Tasks;
using SF.Core.Security;
using SF.Core.Settings;

namespace SF.Module.Backend.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/settings")]
    public class SettingController : Controller
    {
        private static object _lock = new object();
        private readonly ISettingsManager _settingsManager;
        private readonly IAuthorizationService _authorizationService;
        public SettingController(ISettingsManager settingsManager,
            IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
            _settingsManager = settingsManager;
        }



        /// <summary>
        /// Update settings values
        /// </summary>
        /// <param name="settings"></param>
        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Update(webModel.SettingViewModel[] settings)
        {
            if (!await _authorizationService.AuthorizeAsync(User, GobalPermissions.EditSetting))
                return Unauthorized();
            lock (_lock)
            {
                _settingsManager.SaveSettings(settings.Select(x => x.ToModuleModel()).ToArray());
            }
            return Json(new { Result = true });
        }

        /// <summary>
        /// Get array setting values by name
        /// </summary>
        /// <param name="name">Setting system name.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("values/{name}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult GetArray(string name)
        {
            var value = _settingsManager.GetArray<object>(name, null);
            return Ok(value);
        }
    }
}
