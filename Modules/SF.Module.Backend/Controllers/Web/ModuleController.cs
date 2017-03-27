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
    /// 菜单管理
    /// </summary>
    [Authorize(Roles = "Administrators")]
    [Route("Module/")]
    public class ModuleController : SF.Web.Base.Controllers.ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IBackendUnitOfWork _backendUnitOfWork;
        public ModuleController(IServiceCollection collection, ILogger<ModuleController> logger,
           IBackendUnitOfWork backendUnitOfWork)
            : base(collection, logger)
        {
            this._backendUnitOfWork = backendUnitOfWork;
        }
        #region 视图功能
        /// <summary>
        /// 菜单管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 菜单表单
        /// </summary>
        /// <returns></returns>
        [Route("Form")]
        public ActionResult Form(long? keyValue)
        {
            ViewBag.ModuleId = keyValue ?? 0;
            return View();
        }
        /// <summary>
        /// 菜单详细
        /// </summary>
        /// <returns></returns>
        [Route("Detail")]
        public ActionResult Detail()
        {
            return View();
        }
        /// <summary>
        /// 功能图标
        /// </summary>
        /// <returns></returns>
        [Route("Icon")]
        public ActionResult Icon()
        {
            return View();
        }

        /// <summary>
        /// 按钮表单
        /// </summary>
        /// <returns></returns>
        [Route("ButtonForm")]
        public ActionResult ButtonForm()
        {
            ViewBag.ModuleId = HttpContext.Request.Query["keyValue"];
            return View();
        }
        /// <summary>
        /// 选择系统功能
        /// </summary>
        /// <returns></returns>
        [Route("OptionModule")]
        public ActionResult OptionModule()
        {
            return View();
        }
        #endregion

    }

}
