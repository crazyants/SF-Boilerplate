using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;
using SF.Core.Common;
using SF.Core.Data;
using SF.Core.Entitys;
using SF.Core.Extensions;
using SF.Core.QueryExtensions.Extensions;
using SF.Core.Web.Attributes;
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
    [Route("Api/DataItem/")]
    public class DataIteApiController : CrudControllerBase<DataItemEntity, DataItemViewModel>
    {
        private readonly IBaseUnitOfWork _baseUnitOfWork;
        public DataIteApiController(IServiceCollection collection, ILogger<UserCrudController> logger,
             IBaseUnitOfWork baseUnitOfWork)
            : base( baseUnitOfWork, collection, logger)
        {
            this._baseUnitOfWork = baseUnitOfWork;
            CrudDtoMapper = new DataItemDtoMapper();

        }


        #region 获取数据
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rootDataItemId"></param>
        /// <param name="countsType"></param>
        /// <returns></returns>
        [Route("GetChildren/{id}")]
        public IQueryable<TreeViewItem> GetChildren(
        int id,
        int rootDataItemId = 0,
        TreeViewItem.GetCountsType countsType = TreeViewItem.GetCountsType.None)
        {

            var qry = this._baseUnitOfWork.BaseWorkArea.DataItem.GetChildren(id, rootDataItemId);

            List<DataItemEntity> dataItemEntityList = new List<DataItemEntity>();
            List<TreeViewItem> groupNameList = new List<TreeViewItem>();

            foreach (var group in qry.OrderBy(g => g.ItemName))
            {

                dataItemEntityList.Add(group);
                var treeViewItem = new TreeViewItem();
                treeViewItem.Id = group.Id.ToString();
                treeViewItem.Name = group.ItemName;
                treeViewItem.IsActive = (group.EnabledMark ?? 0) > 0;

                treeViewItem.IconCssClass = "fa fa-sitemap";

                if (countsType == TreeViewItem.GetCountsType.ChildGroups)
                {
                    treeViewItem.CountInfo = this._baseUnitOfWork.BaseWorkArea.DataItem.Query().Where(a => a.ParentId.HasValue && a.ParentId == group.Id).Count();
                }

                groupNameList.Add(treeViewItem);

            }

            //快速找出哪些项目有子级
            List<long> resultIds = dataItemEntityList.Select(a => a.Id).ToList();
            var qryHasChildrenList = this._baseUnitOfWork.BaseWorkArea.DataItem.Query()
                .Where(g =>
                   g.ParentId.HasValue &&
                   resultIds.Contains(g.ParentId.Value)).Select(g => g.ParentId.Value)
                .Distinct()
                .ToList();

            foreach (var g in groupNameList)
            {
                int groupId = g.Id.AsInteger();
                g.HasChildren = qryHasChildrenList.Any(a => a == groupId);
            }

            return groupNameList.AsQueryable();
        }
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
                data = data.TreeWhere(t => t.ItemName.Contains(keyword));
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
        [Route("ExistItemCode")]
        public ActionResult ExistItemCode(string itemCode, string keyValue)
        {
            var query = _repository.Query();
            Expression<Func<DataItemEntity, bool>> pi = d => d.ItemCode == itemCode;
            if (!string.IsNullOrEmpty(keyValue))
            {
                Expression<Func<DataItemEntity, bool>> pk = d => d.Id != keyValue.AsInteger(0);
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
            Expression<Func<DataItemEntity, bool>> pi = d => d.ItemName == itemName;
            if (!string.IsNullOrEmpty(keyValue))
            {
                Expression<Func<DataItemEntity, bool>> pk = d => d.Id != keyValue.AsInteger(0);
                pi.And(pk);
            }
            bool IsOk = query.Where(pi).Count() == 0 ? true : false;
            return Content(IsOk.ToString());
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[HandlerAjaxOnly]
        //public ActionResult RemoveForm(string keyValue)
        //{
        //    dataItemBLL.RemoveForm(keyValue);
        //    base.UpdateAsync
        //    return Success("删除成功。");
        //}
        /// <summary>
        /// 保存分类表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="dataItemEntity">分类实体</param>
        /// <returns></returns>
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[HandlerAjaxOnly]
        //public ActionResult SaveForm(string keyValue, DataItemEntity dataItemEntity)
        //{
        //    dataItemBLL.SaveForm(keyValue, dataItemEntity);
        //    return Success("操作成功。");
        //}
        #endregion
    }

    /// <summary>
    /// 字典映射
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
            var retVal = Mapper.Map<DataItemViewModel, DataItemEntity>(dto);
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
            var retVal = Mapper.Map<DataItemEntity, DataItemViewModel>(entity);
            return retVal;
        }
    }
}
