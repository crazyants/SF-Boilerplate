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
    /// 区域管理
    /// </summary>
    [Authorize(Roles = "Administrators")]
    [Route("Area/")]
    public class AreaController : SF.Web.Base.Controllers.ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IBackendUnitOfWork _backendUnitOfWork;
        public AreaController(IServiceCollection collection, ILogger<AreaController> logger,
           IBackendUnitOfWork backendUnitOfWork)
            : base(collection, logger)
        {
            this._backendUnitOfWork = backendUnitOfWork;
        }
        #region 视图功能
        /// <summary>
        /// 区域管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 区域表单
        /// </summary>
        /// <returns></returns>
        [Route("Form")]
        public ActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 区域详细
        /// </summary>
        /// <returns></returns>
        [Route("Detail")]
        public ActionResult Detail()
        {
            return View();
        }
        #endregion

    }

}
