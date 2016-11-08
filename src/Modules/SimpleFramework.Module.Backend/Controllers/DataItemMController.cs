using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;
using SimpleFramework.Core.Common;
using SimpleFramework.Core.Entitys;
using SimpleFramework.Core.Models.GridTree;
using SimpleFramework.Core.Web.Base.Controllers;
using SimpleFramework.Core.Web.Base.DataContractMapper;
using SimpleFramework.Module.Backend.ViewModels;
using SimpleFramework.Web.Control.JqGrid.Core.Json;
using SimpleFramework.Web.Control.JqGrid.Core.Request;
using SimpleFramework.Web.Control.JqGrid.Core.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace SimpleFramework.Module.Backend.Controllers
{
    /// <summary>
    /// 字典管理
    /// </summary>
    [Authorize]
    [Route("DataItemM/")]
    public class DataItemMController : CrudControllerBase<DataItemEntity, DataItemViewModel>
    {
        public DataItemMController(IServiceCollection collection, ILogger<UserCrudController> logger) : base(collection, logger)
        {
            CrudDtoMapper = new DataItemDtoMapper();
        }
        [Route("List")]
        public ActionResult List()
        {
            return View();
        }
        /// <summary>
        /// 分类列表
        /// </summary>
        /// <param name="keyword">关键字查询</param>
        /// <returns>返回树形列表Json</returns>
        [HttpGet]
        public ActionResult GetTreeListJson(string keyword)
        {
            var data = _repository.GetAll().ToList();

            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.ItemName.Contains(keyword), "");
            }
            JqGridResponse response = new JqGridResponse();



            var TreeList = new List<TreeGridEntity>();
            foreach (DataItemEntity item in data)
            {
                TreeGridEntity tree = new TreeGridEntity();
                bool hasChildren = data.Count(t => t.ParentId == item.Id) == 0 ? false : true;
                tree.id = item.Id.ToString();
                tree.parentId = item.ParentId.HasValue ? item.ParentId.Value.ToString() : "";
                tree.expanded = true;
                tree.hasChildren = hasChildren;
                tree.entityJson = item.ToJson();
                TreeList.Add(tree);
            }
            return Content(TreeList.TreeJson());
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
            var query = _repository.Query().SelectPage(request.PageIndex, request.RecordsCount, out totalRecords);
            JqGridResponse response = new JqGridResponse()
            {
                TotalPagesCount = (int)Math.Ceiling((float)totalRecords / (float)request.RecordsCount),
                PageIndex = request.PageIndex,
                TotalRecordsCount = totalRecords,
            };
            foreach (DataItemEntity userEntity in query)
            {
                response.Records.Add(new JqGridRecord(Convert.ToString(userEntity.Id), userEntity));
            }

            response.Reader.RepeatItems = false;
            return new JqGridJsonResult(response);
        }
    }
    /// <summary>
    /// 字典据映射
    /// </summary>
    public class DataItemDtoMapper : CrudDtoMapper<DataItemEntity, DataItemViewModel>
    {
        /// <summary>
        /// DTO转换领域的实体映射
        /// </summary>
        /// <param name="dto">DTO实体映射</param>
        /// <param name="entity">实体映射DTO</param>
        /// <returns>The entity</returns>
        protected override DataItemEntity OnMapDtoToEntity(DataItemViewModel dto, DataItemEntity entity)
        {
            var retVal = new DataItemEntity();
            retVal.InjectFrom(dto);
            return retVal;
        }
        /// <summary>
        /// 领域的实体转换DTO映射
        /// </summary>
        /// <param name="entity">实体映射</param>
        /// <param name="dto">DTO映射实体</param>
        /// <returns>The dto</returns>
        protected override DataItemViewModel OnMapEntityToDto(DataItemEntity entity, DataItemViewModel dto)
        {
            var retVal = new DataItemViewModel();
            retVal.InjectFrom(entity);
            return retVal;
        }
    }
}
