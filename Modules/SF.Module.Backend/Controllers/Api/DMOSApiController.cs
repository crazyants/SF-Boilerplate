using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SF.Core.Abstraction.Events;
using SF.Core.Common;
using SF.Data;
using SF.Entitys;
using SF.Core.Extensions;
using SF.Core.QueryExtensions.Extensions;
using SF.Module.Backend.Services;
using SF.Module.Backend.ViewModels;
using SF.Web.Base.Args;
using SF.Web.Base.Controllers;
using SF.Web.Base.DataContractMapper;
using SF.Web.Control.JqGrid.Core.Json;
using SF.Web.Control.JqGrid.Core.Request;
using SF.Web.Control.JqGrid.Core.Response;
using SF.Web.Models.GridTree;
using SF.Web.Models.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SF.Module.Backend.Domain.DataItem.ViewModel;
using SF.Module.Backend.Domain.DataItem.Service;
using LinqKit;
using SF.Module.Backend.Domain.DataItem.Rule;
using SF.Module.Backend.Data.Entitys;
using SF.Module.Backend.Data.Uow;
using System.Text;
using SF.Module.Backend.Domain.DMOS.Service;
using SF.Module.Backend.Domain.DMOS.ViewModel;
using System.Threading.Tasks;
using SF.Module.Backend.Domain.DMOS.Rule;
using SF.Module.Backend.Domain.DataItemDetail.Service;
using SF.Module.Backend.Domain.Organize.Service;
using SF.Module.Backend.Domain.Department.Service;

namespace SF.Module.Backend.Controllers
{
    /// <summary>
    /// 字典管理API
    /// </summary>
    [Authorize]
    [Route("Api/DMOS/")]
    public class DMOSApiController : CrudControllerBase<DMOSEntity, DMOSViewModel, long>
    {
        private readonly IMediator _mediator;
        private readonly IDMOSService _dmosService;
        private readonly IDataItemDetailService _dataItemDetailService;
        private readonly IBackendUnitOfWork _backendUnitOfWork;
        private readonly IOrganizeService _organizeService;
        private readonly IDepartmentService _departmentService;
        private readonly IDMOSRules _DMOSRules;
        public DMOSApiController(IServiceCollection collection, ILogger<DMOSApiController> logger,
             IBackendUnitOfWork backendUnitOfWork,
             IMediator mediator,
             IDMOSRules DMOSRules,
             IDMOSService dmosService,
             IOrganizeService organizeService,
             IDepartmentService departmentService,
             IDataItemDetailService dataItemDetailService)
            : base(backendUnitOfWork, collection, logger)
        {
            this._backendUnitOfWork = backendUnitOfWork;
            this._mediator = mediator;
            this._DMOSRules = DMOSRules;
            this._dmosService = dmosService;
            this._organizeService = organizeService;
            this._departmentService = departmentService;
            this._dataItemDetailService = dataItemDetailService;
        }
        #region 事件

        /// <summary>
        /// 新增后
        /// </summary>
        /// <param name="arg"></param>
        protected override void OnAfterAdd(CrudEventArgs<DMOSEntity, DMOSViewModel, long> arg)
        {
            this._mediator.Publish(new EntityCreatedEventData<DMOSEntity>(arg.Entity));
        }
        /// <summary>
        /// 编辑后
        /// </summary>
        /// <param name="arg"></param>
        protected override void OnAfterEdit(CrudEventArgs<DMOSEntity, DMOSViewModel, long> arg)
        {
            this._mediator.Publish(new EntityUpdatedEventData<DMOSEntity>(arg.Entity));
        }
        /// <summary>
        /// 删除后
        /// </summary>
        /// <param name="arg"></param>
        protected override void OnAfterDeletet(CrudEventArgs<DMOSEntity, DMOSViewModel, long> arg)
        {
            this._mediator.Publish(new EntityDeletedEventData<DMOSEntity>(arg.Entity));
        }

        #endregion

        #region 获取数据

        /// <summary>
        /// 岗位职位工作组列表 
        /// </summary>
        /// <param name="value">当前主键</param>
        /// <returns>返回树形Json</returns>
        [Route("GetListJson")]
        public async Task<IActionResult> GetListJson(int category, string organizeId)
        {

            var data = await _dmosService.QueryFilterByCategoryAsync(category, organizeId);
            var dtos = CrudDtoMapper.MapEntityToDtos(data);
            return Content(dtos.ToJson());
        }

        /// <summary>
        /// 岗位职位工作组列表 
        /// </summary>
        /// <param name="category">类型ID</param>
        /// <param name="condition">查询条件</param>
        /// <param name="keyword">关键字</param>
        /// <returns>返回树形列表Json</returns>
        [Route("GetPageListJson")]
        public async Task<IActionResult> GetPageListJson(JqGridRequest request, int category, string condition, string keyword)
        {
            var data = await _dmosService.GetByWhere(category, keyword, condition, request.PageIndex, request.RecordsCount);
            var dtos = CrudDtoMapper.MapEntityToDtos(data);
            var organizes = await this._organizeService.GetAlls();
            var departments = await _departmentService.GetAlls();
            JqGridResponse response = new JqGridResponse();
            foreach (DMOSViewModel userInput in dtos)
            {
                var organizeId = userInput.OrganizeId;
                var departmentId = userInput.DepartmentId;
                if (departmentId == 0)
                {
                    userInput.OrganizeName = organizes.Where(o => o.Id == organizeId)?.FirstOrDefault()?.FullName;
                }
                else
                {
                    var department = departments.Where(o => o.Id == departmentId)?.FirstOrDefault();
                    userInput.DepartmentName = department?.FullName;
                    userInput.OrganizeName = organizes.Where(o => o.Id == department?.OrganizeId)?.FirstOrDefault()?.FullName;
                }
                response.Records.Add(new JqGridRecord(Convert.ToString(userInput.Id), userInput));
            }

            response.Reader.RepeatItems = false;
            return new JqGridJsonResult(response);
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 外文名称不能重复
        /// </summary>
        /// <param name="enCode">外文名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        [Route("ExistEnCode")]
        public ActionResult ExistEnCode(string enCode, long keyValue)
        {
            bool IsOk = _DMOSRules.IsDMOSCodeUnique(enCode, keyValue);
            return Content(IsOk.ToString());
        }
        /// <summary>
        /// 公司名称不能重复
        /// </summary>
        /// <param name="fullName">公司名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        [Route("ExistFullName")]
        public ActionResult ExistFullName(string fullName, long keyValue)
        {
            bool IsOk = _DMOSRules.IsDMOSNameUnique(fullName, keyValue);
            return Content(IsOk.ToString());
        }
        #endregion

    }


}
