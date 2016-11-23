using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleFramework.Core.Common;
using SimpleFramework.Core.Data;
using SimpleFramework.Core.Entitys;
using SimpleFramework.Core.Extensions;
using SimpleFramework.Core.Web.Models.Tree;
using SimpleFramework.Module.Backend.Services;
using System.Collections.Generic;
using System.Linq;


namespace SimpleFramework.Module.Backend.Controllers
{
    /// <summary>
    /// 字典管理
    /// </summary>
    [Authorize]
    [Route("DataItem/")]
    public class DataItemController : Core.Web.Base.Controllers.ControllerBase
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
        [Route("DList")]
        public ActionResult DList()
        {
            return View();
        }
        /// <summary>
        /// 分类表单
        /// </summary>
        /// <returns></returns>
        [Route("DForm")]
        public ActionResult DForm()
        {
            return View();
        }
        /// <summary>
        /// 字典项列表
        /// </summary>
        /// <returns></returns>
        [Route("DTList")]
        public ActionResult DTList()
        {
            return View();
        }
        /// <summary>
        /// 字典项表单
        /// </summary>
        /// <returns></returns>
        [Route("DTForm")]
        public ActionResult DTForm()
        {
            return View();
        }
        #endregion

    }

}
