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


namespace SF.Module.Backend.Controllers
{
    /// <summary>
    /// 字典管理
    /// </summary>
    [Authorize(Roles = "Administrators")]
    [Route("DataItem/")]
    public class DataItemController : SF.Web.Base.Controllers.ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IBaseUnitOfWork _baseUnitOfWork;
        public DataItemController(IServiceCollection collection, ILogger<DataItemController> logger,
           IBaseUnitOfWork baseUnitOfWork)
            : base(collection, logger)
        {
            this._baseUnitOfWork = baseUnitOfWork;
        }
        #region 视图功能
        /// <summary>
        /// 字典首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 字典分类列表
        /// </summary>
        /// <returns></returns>
        [Route("List")]
        public ActionResult DList()
        {
            return View("List");
        }
        /// <summary>
        /// 分类表单
        /// </summary>
        /// <returns></returns>
        [Route("Form")]
        public ActionResult DForm()
        {
            return View("Form");
        }
        /// <summary>
        /// 字典项表单
        /// </summary>
        /// <returns></returns>
        [Route("ValueForm")]
        public ActionResult ValueForm()
        {
            return View("ValueForm");
        }
        /// <summary>
        /// 字典项明细
        /// </summary>
        /// <returns></returns>
        [Route("ValueDetailForm")]
        public ActionResult ValueDetailForm()
        {
            return View("ValueDetailForm");
        }
        #endregion

    }

}
