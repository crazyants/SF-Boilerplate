using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SF.Core.Common;
using SF.Data;
using SF.Entitys;
using SF.Core.Extensions;
using SF.Core.QueryExtensions.Extensions;
using SF.Web.Base.Controllers;
using SF.Web.Base.DataContractMapper;
using SF.Web.Control.JqGrid.Core.Json;
using SF.Web.Control.JqGrid.Core.Request;
using SF.Web.Control.JqGrid.Core.Response;
using SF.Web.Models.GridTree;
using SF.Web.Models.Tree;
using SF.Module.Backend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SF.Module.Backend.Domain.DataItemDetail.ViewModel;
using LinqKit;
using MediatR;
using SF.Module.Backend.Domain.DataItemDetail.Rule;
using SF.Module.Backend.Domain.DataItemDetail.Service;
using SF.Module.Backend.Data.Entitys;

namespace SF.Module.Backend.Controllers
{
    /// <summary>
    /// 字典管理API
    /// </summary>
    [Authorize]
    [Route("Api/DataItemDetail/")]
    public class DataItemDetailApiController : CrudControllerBase<DataItemDetailEntity, DataItemDetailViewModel, long>
    {
        private readonly IMediator _mediator;
        private readonly IDataItemDetailService _dataItemService;
        private readonly IDataItemDetailRules _dataItemRules;
        private readonly IBaseUnitOfWork _baseUnitOfWork;
        public DataItemDetailApiController(IServiceCollection collection, ILogger<DataItemDetailApiController> logger,
             IBaseUnitOfWork baseUnitOfWork,
             IMediator mediator,
             IDataItemDetailService dataItemService,
             IDataItemDetailRules dataItemRules)
            : base(baseUnitOfWork, collection, logger)
        {
            this._baseUnitOfWork = baseUnitOfWork;
            this._mediator = mediator;
            this._dataItemService = dataItemService;
            this._dataItemRules = dataItemRules;

        }

        #region 获取数据
        /// <summary>
        /// 项列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        [Route("GetPageList")]
        public ActionResult GetPageListJson(DataItemDetailSearchRequest searchRequest, JqGridRequest request)
        {

            var query = _dataItemService.GetByWhere(searchRequest.ItemId, searchRequest.KeyWord, searchRequest.Condition, request.PageIndex, request.RecordsCount);
           
            var dtos = CrudDtoMapper.MapEntityToDtos(query);
            JqGridResponse response = new JqGridResponse()
            {
                TotalPagesCount = query.TotalPages,
                PageIndex = request.PageIndex,
                TotalRecordsCount = query.TotalCount,
            };
            foreach (DataItemDetailViewModel userInput in dtos)
            {
                response.Records.Add(new JqGridRecord(Convert.ToString(userInput.Id), userInput));
            }

            response.Reader.RepeatItems = false;
            return new JqGridJsonResult(response);
        }
        /// <summary>
        /// 获取数据字典列表（绑定控件）
        /// </summary>
        /// <param name="EnCode">代码</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        [Route("GetDataItemListJson")]
        public ActionResult GetDataItemListJson(string EnCode)
        {
            var data = _dataItemService.GetByItemCode(EnCode);
            return Content(data.ToJson());
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 分类编号不能重复
        /// </summary>
        /// <param name="ItemCode">编号</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        [Route("ExistItemValue")]
        public ActionResult ExistItemValue(string itemValue, long keyValue, long itemId)
        {
            bool IsOk = _dataItemRules.IsDataItemDetailValueUnique(itemValue, itemId, keyValue);
            return Content(IsOk.ToString());
        }
        /// <summary>
        /// 分类名称不能重复
        /// </summary>
        /// <param name="ItemName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        [Route("ExistItemName")]
        public ActionResult ExistItemName(string itemName, long keyValue, long itemId)
        {
            bool IsOk = _dataItemRules.IsDataItemDetailNameUnique(itemName, itemId, keyValue);
            return Content(IsOk.ToString());
        }
        #endregion
    }

}
