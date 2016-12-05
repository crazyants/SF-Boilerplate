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
using SF.Core.Web.Base.Controllers;
using SF.Core.Web.Base.DataContractMapper;
using SF.Core.Web.Models.GridTree;
using SF.Core.Web.Models.Tree;
using SF.Module.Backend.Services;
using SF.Module.Backend.ViewModels;
using SF.Web.Control.JqGrid.Core.Json;
using SF.Web.Control.JqGrid.Core.Request;
using SF.Web.Control.JqGrid.Core.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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
            CrudDtoMapper = new DataItemDetailDtoMapper();

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
        public ActionResult GetPageListJson(JqGridRequest request, string encode)
        {
            Expression<Func<DataItemDetailEntity, bool>> di = d => d.ItemId == encode.Convert<long>();
            var query = _repository.QueryPage(di, page: request.PageIndex, pageSize: request.RecordsCount);
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
        /// 分类编号不能重复
        /// </summary>
        /// <param name="ItemCode">编号</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        [Route("ExistItemCode")]
        public ActionResult ExistItemCode(string itemCode, string keyValue)
        {
            var query = _repository.Query();
            Expression<Func<DataItemDetailEntity, bool>> pi = d => d.ItemCode == itemCode;
            if (!string.IsNullOrEmpty(keyValue))
            {
                Expression<Func<DataItemDetailEntity, bool>> pk = d => d.Id != keyValue.AsInteger(0);
                pi.And(pk);
            }
            bool IsOk = query.Where(pi).Count() == 0 ? true : false;
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
        public ActionResult ExistItemName(string itemName, string keyValue)
        {

            var query = _repository.Query();
            Expression<Func<DataItemDetailEntity, bool>> pi = d => d.ItemName == itemName;
            if (!string.IsNullOrEmpty(keyValue))
            {
                Expression<Func<DataItemDetailEntity, bool>> pk = d => d.Id != keyValue.AsInteger(0);
                pi.And(pk);
            }
            bool IsOk = query.Where(pi).Count() == 0 ? true : false;
            return Content(IsOk.ToString());
        }
        #endregion

      
    }

    /// <summary>
    /// 字典映射
    /// </summary>
    public class DataItemDetailDtoMapper : CrudDtoMapper<DataItemDetailEntity, DataItemDetailViewModel>
    {


        /// <summary>
        /// DTO转换领域的实体映射
        /// </summary>
        /// <param name="dto">DTO实体映射</param>
        /// <param name="entity">实体映射DTO</param>
        /// <returns>The entity</returns>
        protected override DataItemDetailEntity OnMapDtoToEntity(DataItemDetailViewModel dto, DataItemDetailEntity entity)
        {
            Mapper.Map<DataItemDetailViewModel, DataItemDetailEntity>(dto, entity);
            return entity;
        }
        /// <summary>
        /// 领域的实体转换DTO映射
        /// </summary>
        /// <param name="entity">实体映射</param>
        /// <param name="dto">DTO映射实体</param>
        /// <returns>The dto</returns>
        protected override DataItemDetailViewModel OnMapEntityToDto(DataItemDetailEntity entity, DataItemDetailViewModel dto)
        {
            Mapper.Map<DataItemDetailEntity, DataItemDetailViewModel>(entity, dto);
            return dto;
        }
        /// <summary>
        /// 领域的实体转换List<dto>映射
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected override IEnumerable<DataItemDetailViewModel> OnMapEntityToDtos(IEnumerable<DataItemDetailEntity> entitys)
        {
            var dtos = Mapper.Map<IEnumerable<DataItemDetailEntity>, IEnumerable<DataItemDetailViewModel>>(entitys);
            return dtos;
        }
    }
}
