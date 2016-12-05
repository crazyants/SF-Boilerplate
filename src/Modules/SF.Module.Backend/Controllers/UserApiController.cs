using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SF.Core.Abstraction.Data;
using SF.Core.Entitys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using SF.Core.Security;
using SF.Core.Common;
using SF.Core.Data;
using SF.Core.Abstraction.UoW.Helper;

namespace SF.Module.Backend.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/users")]
    public class UserApiController : Core.Web.Base.Controllers.ControllerBase
    {
        private readonly IBaseUnitOfWork _baseUnitOfWork;
        private readonly ISecurityService _securityService;
        public UserApiController(IBaseUnitOfWork baseUnitOfWork,
            ISecurityService securityService,
            IServiceCollection service,
            ILogger<UserApiController> logger) : base(service, logger)
        {
            this._baseUnitOfWork = baseUnitOfWork;
            this._securityService = securityService;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> GetUserById(string id)
        {
            var retVal = await _securityService.FindByIdAsync(id, UserDetails.Full);
            return Content(retVal.ToJson());
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {
            var user = _baseUnitOfWork.BaseWorkArea.User.GetById(id);
            if (user == null)
            {
                return new NotFoundResult();
            }

            user.IsDeleted = true;
            _baseUnitOfWork.ExecuteAndCommit(uow => { _baseUnitOfWork.BaseWorkArea.User.Update(user); });
            return Json(true);
        }

        /// <summary>
        /// Get current user details
        /// </summary>
        [HttpGet]
        [Route("currentuser")]
        public async Task<ActionResult> GetCurrentUser()
        {
            var retVal = await _securityService.FindByNameAsync(User.Identity.Name, UserDetails.Full);
            return Content(retVal.ToJson());
        }

        /// <summary>
        /// Get user details by user name
        /// </summary>
        /// <param name="userName"></param>
        [HttpGet]
        [Route("{userName}")]
        public async Task<ActionResult> GetUserByName(string userName)
        {
            var retVal = await _securityService.FindByNameAsync(userName, UserDetails.Full);
            return Content(retVal.ToJson());
        }


        /// <summary>
        /// Check specified user has passed permissions in specified scope
        /// </summary>
        /// <param name="userName">security account name</param>
        /// <param name="permissions">checked permissions Example: ?permissions=read&amp;permissions=write </param>
        /// <param name="scopes">security bounded scopes. Read mode: http://docs.virtocommerce.com/display/vc2devguide/Working+with+platform+security </param>
        /// <returns></returns>
        [HttpGet]
        [Route("{userName}/hasPermissions")]
        public ActionResult UserHasAnyPermission(string userName, string[] permissions, string[] scopes)
        {
            var retVal = new { Result = _securityService.UserHasAnyPermission(userName, scopes, permissions) };
            return Content(retVal.ToJson());
        }
    }
}
