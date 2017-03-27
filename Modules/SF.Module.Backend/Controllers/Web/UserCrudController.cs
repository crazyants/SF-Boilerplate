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
    [Authorize]
    public class UserCrudController : SF.Web.Base.Controllers.ControllerBase
    {
        private IBaseUnitOfWork _baseUnitOfWork;
        public UserCrudController(IServiceCollection collection, ILogger<UserCrudController> logger,
            IBaseUnitOfWork baseUnitOfWork) : base(collection, logger)
        {
            _baseUnitOfWork = baseUnitOfWork;
        }
        [Route("Users")]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(JqGridRequest request, string queryJson)
        {
            int totalRecords = 0;
            var query = _baseUnitOfWork.BaseWorkArea.User.QueryPage(page: request.PageIndex, pageSize: request.RecordsCount);
            totalRecords = query.TotalCount;
            JqGridResponse response = new JqGridResponse()
            {
                TotalPagesCount = (int)Math.Ceiling((float)totalRecords / (float)request.RecordsCount),
                PageIndex = request.PageIndex,
                TotalRecordsCount = totalRecords,
            };
            foreach (UserEntity userEntity in query)
            {
                response.Records.Add(new JqGridRecord(Convert.ToString(userEntity.Id), userEntity));
            }

            response.Reader.RepeatItems = false;
            return new JqGridJsonResult(response);
        }

    }
}
