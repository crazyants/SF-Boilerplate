using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SF.Core.Common;
using SF.Core.Data;
using SF.Core.Entitys;
using SF.Core.Extensions;
using SF.Core.QueryExtensions.Extensions;
using SF.Web.Common.Base.Controllers;
using SF.Web.Common.Base.DataContractMapper;
using SF.Web.Control.JqGrid.Core.Json;
using SF.Web.Control.JqGrid.Core.Request;
using SF.Web.Control.JqGrid.Core.Response;
using SF.Web.Common.Models.GridTree;
using SF.Web.Common.Models.Tree;
using SF.Module.Backend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SF.Module.Backend.Domain.DataItemDetail.ViewModel;
using LinqKit;

namespace SF.Module.Backend.Controllers
{
    /// <summary>
    /// 字典管理API
    /// </summary>
    [Authorize]
    [Route("Api/DataItemDetail/")]
    public class DataItemDetailApiController : CrudControllerBase<DataItemDetailEntity, DataItemDetailViewModel>
    {
        private readonly IBaseUnitOfWork _baseUnitOfWork;
        public DataItemDetailApiController(IServiceCollection collection, ILogger<DataItemDetailApiController> logger,
             IBaseUnitOfWork baseUnitOfWork)
            : base(baseUnitOfWork, collection, logger)
        {
            this._baseUnitOfWork = baseUnitOfWork;

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
        public ActionResult GetPageListJson(long itemId, string condition, string keyword, JqGridRequest request)
        {
            Expression<Func<DataItemDetailEntity, bool>> pk = d => d.ItemId == itemId;

            if (!string.IsNullOrEmpty(keyword))
            {
                Expression<Func<DataItemDetailEntity, bool>> pc = null;
                #region 多条件查询
                switch (condition)
                {
                    case "ItemName":        //项目名
                        pc = d => d.ItemName.Contains(keyword);
                        break;
                    case "ItemValue":      //项目值
                        pc = d => d.ItemValue.Contains(keyword);
                        break;
                    case "SimpleSpelling": //拼音
                        pc = d => d.SimpleSpelling.Contains(keyword);
                        break;
                    default:
                        break;

                }
                #endregion
                pk.And(pc);
            }
            var query = _repository.QueryPage(pk, page: request.PageIndex, pageSize: request.RecordsCount);
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
        #endregion

        #region 验证数据
        /// <summary>
        /// 分类值不能重复
        /// </summary>
        /// <param name="ItemCode">编号</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        [Route("ExistItemValue")]
        public ActionResult ExistItemValue(string itemValue, long? keyValue)
        {
            var query = _repository.Query();
            query = query.Where(d => d.ItemValue == itemValue);
            if (keyValue.HasValue)
            {
                query = query.Where(d => d.Id != keyValue);
            }
            bool IsOk = query.Count() == 0 ? true : false;
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
        public ActionResult ExistItemName(string itemName, long? keyValue)
        {
            var query = _repository.Query();
            query = query.Where(d => d.ItemName == itemName);
            if (keyValue.HasValue)
            {
                query = query.Where(d => d.Id != keyValue);
            }
            bool IsOk = query.Count() == 0 ? true : false;
            return Content(IsOk.ToString());
        }
        #endregion

    }

}
