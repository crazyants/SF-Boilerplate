using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SF.Data;
using SF.Entitys;
using SF.Web.Base.Controllers;
using SF.Web.Base.DataContractMapper;
using SF.Module.Backend.ViewModels;
using SF.Web.Control.JqGrid.Core.Json;
using SF.Web.Control.JqGrid.Core.Request;
using SF.Web.Control.JqGrid.Core.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace SF.Module.Backend.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Authorize(Roles = "Administrators")]
    [Route("User/")]
    public class UserController : SF.Web.Base.Controllers.ControllerBase
    {
        private IBaseUnitOfWork _baseUnitOfWork;
        public UserController(IServiceCollection collection, ILogger<UserController> logger,
            IBaseUnitOfWork baseUnitOfWork) : base(collection, logger)
        {
            _baseUnitOfWork = baseUnitOfWork;
        }

        #region 视图功能
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 表单
        /// </summary>
        /// <returns></returns>
        [Route("Form")]
        public ActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <returns></returns>
        [Route("RevisePassword")]
        public ActionResult RevisePassword()
        {
            return View();
        }
        #endregion


    }
}
