using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SF.Core.Common;
using SF.Data;
using SF.Entitys;
using SF.Core.Extensions;
using SF.Web.Models.Tree;
using SF.Module.Backend.Services;
using System.Collections.Generic;
using System.Linq;
using SF.Module.Backend.Data.Uow;

namespace SF.Module.Backend.Controllers
{
    /// <summary>
    /// 授权管理
    /// </summary>
    [Authorize(Roles = "Administrators")]
    [Route("Authorize/")]
    public class AuthorizeController : SF.Web.Base.Controllers.ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        public AuthorizeController(IServiceCollection collection, ILogger<ModuleController> logger,
           IBackendUnitOfWork backendUnitOfWork)
            : base(collection, logger)
        {

        }
        #region 视图功能
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 角色权限
        /// </summary>
        /// <returns></returns>
        [Route("AllotAuthorize")]
        public ActionResult AllotAuthorize()
        {
            return View();
        }
        /// <summary>
        /// 角色成员
        /// </summary>
        /// <returns></returns>
        [Route("AllotMember")]
        public ActionResult AllotMember()
        {
            return View();
        }

        /// <summary>
        /// 用户角色授权
        /// </summary>
        /// <returns></returns>
        [Route("AllotRole")]
        public ActionResult AllotRole()
        {
            return View();
        }
        #endregion

    }

}
