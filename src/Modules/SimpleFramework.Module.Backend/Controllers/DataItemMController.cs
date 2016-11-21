using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;
using SimpleFramework.Core.Common;
using SimpleFramework.Core.Data;
using SimpleFramework.Core.Entitys;
using SimpleFramework.Core.Extensions;
using SimpleFramework.Core.Web.Base.Controllers;
using SimpleFramework.Core.Web.Base.DataContractMapper;
using SimpleFramework.Core.Web.Models.GridTree;
using SimpleFramework.Module.Backend.ViewModels;
using SimpleFramework.Web.Control.JqGrid.Core.Json;
using SimpleFramework.Web.Control.JqGrid.Core.Request;
using SimpleFramework.Web.Control.JqGrid.Core.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SimpleFramework.Module.Backend.Controllers
{
    /// <summary>
    /// 字典管理
    /// </summary>
    [Authorize]
    [Route("DataItemM/")]
    public class DataItemMController : CrudControllerBase<DataItemEntity, DataItemViewModel>
    {
        public DataItemMController(IServiceCollection collection, ILogger<UserCrudController> logger,
            CoreDbContext dbContext, IBaseUnitOfWork baseUnitOfWork)
            : base(dbContext, baseUnitOfWork, collection, logger)
        {
            CrudDtoMapper = new DataItemDtoMapper();
        }
        #region 视图功能
        [Route("List")]
        public ActionResult List()
        {
            return View();
        }
        /// <summary>
        /// 分类表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        #endregion
        #region 获取数据
        /// <summary>
        /// 分类列表
        /// </summary>
        /// <param name="keyword">关键字查询</param>
        /// <returns>返回树形列表Json</returns>
        [HttpGet]
        [Route("GetTreeList")]
        public ActionResult GetTreeListJson(string keyword)
        {
            var data = _repository.Query().ToList();

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
        [Route("GetPageList")]
        public ActionResult GetPageListJson(JqGridRequest request, string queryJson)
        {

            var query = _repository.QueryPage(page: request.PageIndex, pageSize: request.RecordsCount);
            JqGridResponse response = new JqGridResponse()
            {
                TotalPagesCount = query.TotalPages,
                PageIndex = request.PageIndex,
                TotalRecordsCount = query.TotalCount,
            };
            foreach (DataItemEntity userEntity in query)
            {
                response.Records.Add(new JqGridRecord(Convert.ToString(userEntity.Id), userEntity));
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
        public ActionResult ExistItemCode(string itemCode, string keyValue)
        {
            var query = _repository.Query();
            Expression<Func<DataItemEntity, bool>> pi = d => d.ItemCode == itemCode;
            if (!string.IsNullOrEmpty(keyValue))
            {
                Expression<Func<DataItemEntity, bool>> pk = d => d.Id != keyValue.TryParse();
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
        public ActionResult ExistItemName(string itemName, string keyValue)
        {

            var query = _repository.Query();
            Expression<Func<DataItemEntity, bool>> pi = d => d.ItemName == itemName;
            if (!string.IsNullOrEmpty(keyValue))
            {
                Expression<Func<DataItemEntity, bool>> pk = d => d.Id != keyValue.TryParse();
                pi.And(pk);
            }
            bool IsOk = query.Where(pi).Count() == 0 ? true : false;
            return Content(IsOk.ToString());
        }
        #endregion
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
