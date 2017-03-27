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
using SF.Module.Backend.Domain.DataItem.Rule;
using SF.Module.Backend.Data.Entitys;
using System.Threading.Tasks;
using System.Text;

namespace SF.Module.Backend.Controllers
{
    /// <summary>
    /// 字典管理API
    /// </summary>
    [Authorize]
    [Route("Api/DataItem/")]
    public class DataItemApiController : CrudControllerBase<DataItemEntity, DataItemViewModel, long>
    {
        private readonly IMediator _mediator;
        private readonly IDataItemService _dataItemService;
        private readonly IDataItemRules _dataItemRules;
        private readonly IBaseUnitOfWork _baseUnitOfWork;
        public DataItemApiController(IServiceCollection collection, ILogger<DataItemApiController> logger,
             IBaseUnitOfWork baseUnitOfWork,
             IMediator mediator,
             IDataItemService dataItemService,
             IDataItemRules dataItemRules)
            : base(baseUnitOfWork, collection, logger)
        {
            this._baseUnitOfWork = baseUnitOfWork;
            this._mediator = mediator;
            this._dataItemService = dataItemService;
            this._dataItemRules = dataItemRules;
        }

        #region 事件
        /// <summary>
        /// 获取一条记录后
        /// </summary>
        /// <param name="arg"></param>
        protected override void OnAfterGet(DataItemViewModel arg)
        {
            var allDataItemDto = CrudDtoMapper.MapEntityToDtos(_readerService.GetAll());
            var ids = arg.FindParentWhere(allDataItemDto.ToList(), null).Select(x => x.Id).ToArray();
            arg.ParentPath = string.Join(",", ids);
        }

        /// <summary>
        /// 新增后
        /// </summary>
        /// <param name="arg"></param>
        protected override void OnAfterAdd(CrudEventArgs<DataItemEntity, DataItemViewModel, long> arg)
        {
            this._mediator.Publish(new EntityCreatedEventData<DataItemEntity>(arg.Entity));
        }
        /// <summary>
        /// 编辑后
        /// </summary>
        /// <param name="arg"></param>
        protected override void OnAfterEdit(CrudEventArgs<DataItemEntity, DataItemViewModel, long> arg)
        {
            this._mediator.Publish(new EntityUpdatedEventData<DataItemEntity>(arg.Entity));
        }
        /// <summary>
        /// 删除后
        /// </summary>
        /// <param name="arg"></param>
        protected override void OnAfterDeletet(CrudEventArgs<DataItemEntity, DataItemViewModel, long> arg)
        {
            this._mediator.Publish(new EntityDeletedEventData<DataItemEntity>(arg.Entity));
        }
        
        #endregion

        #region 获取数据
        /// <summary>
        /// 字典树数据源
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
            var qry = _dataItemService.GetChildren(id, rootDataItemId);

            List<DataItemViewModel> dataItemEntityList = new List<DataItemViewModel>();
            List<TreeViewItem> groupNameList = new List<TreeViewItem>();
            var groups = qry.OrderBy(g => g.ItemName);
            foreach (var group in groups)
            {

                dataItemEntityList.Add(group);
                var treeViewItem = new TreeViewItem();
                treeViewItem.Id = group.Id.ToString();
                treeViewItem.Name = $"{group.ItemName}({group.ItemCode})";
                treeViewItem.IsActive = (group.EnabledMark ?? 0) > 0;

                treeViewItem.IconCssClass = "fa fa-sitemap";

                if (countsType == TreeViewItem.GetCountsType.ChildGroups)
                {
                    treeViewItem.CountInfo = this._dataItemService.GetAlls().Where(a => a.ParentId.HasValue && a.ParentId == group.Id).Count();
                }

                groupNameList.Add(treeViewItem);

            }

            //快速找出哪些项目有子级
            List<long> resultIds = dataItemEntityList.Select(a => a.Id).ToList();
            var qryHasChildrenList = _dataItemService.GetAlls()
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
        /// 分类树列表
        /// </summary>
        /// <param name="keyword">关键字查询</param>
        /// <returns>返回树形列表Json</returns>
        [HttpGet]
        [Route("GetTreeList")]
        public ActionResult GetTreeListJson(string keyword)
        {
            var data = _dataItemService.GetAlls();

            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.ItemName.Contains(keyword));
            }
            JqGridResponse response = new JqGridResponse();
            var dtos = CrudDtoMapper.MapEntityToDtos(data);
            var TreeList = new List<TreeGridEntity>();
            foreach (DataItemViewModel item in dtos)
            {
                TreeGridEntity tree = new TreeGridEntity();
                bool hasChildren = data.Count(t => t.ParentId == item.Id && t.ParentId != t.Id) == 0 ? false : true;
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
        /// 分类普通列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        [Route("GetPageList")]
        public ActionResult GetPageListJson(JqGridRequest request, string keyword)
        {

            var query = _dataItemService.GetPageListBykeyword(keyword, request.PageIndex, request.RecordsCount);
            var dtos = CrudDtoMapper.MapEntityToDtos(query);
            JqGridResponse response = new JqGridResponse()
            {
                TotalPagesCount = query.TotalPages,
                PageIndex = request.PageIndex,
                TotalRecordsCount = query.TotalCount,
            };
            foreach (DataItemViewModel userInput in dtos)
            {
                response.Records.Add(new JqGridRecord(Convert.ToString(userInput.Id), userInput));
            }

            response.Reader.RepeatItems = false;
            return new JqGridJsonResult(response);
        }

        /// <summary>
        /// 区域列表 
        /// </summary>
        /// <param name="value">当前主键</param>
        /// <returns>返回树形Json</returns>
        [Route("GetTreeJson")]
        public    IActionResult GetTreeJson(long? value)
        {
           
            var filterdata =   _dataItemService.GetChildren(value ?? 0,0);
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            if (filterdata.Count > 0)
            {
                foreach (DataItemViewModel item in filterdata)
                {
                    bool hasChildren = _dataItemService.GetChildren(item.Id,0).Any() ? true : false;
                    sb.Append("{");
                    sb.Append("\"id\":\"" + item.Id + "\",");
                    sb.Append("\"text\":\"" + item.ItemName + "\",");
                    sb.Append("\"value\":\"" + item.Id + "\",");
                    sb.Append("\"isexpand\":false,");
                    sb.Append("\"complete\":false,");
                    sb.Append("\"hasChildren\":" + hasChildren.ToString().ToLower() + "");
                    sb.Append("},");
                }
                sb = sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("]");
            return Content(sb.ToString());
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
        public ActionResult ExistItemCode(string itemCode, long keyValue)
        {
            bool IsOk = _dataItemRules.IsDataItemCodeUnique(itemCode, keyValue);
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
        public ActionResult ExistItemName(string itemName, long keyValue)
        {
            bool IsOk = _dataItemRules.IsDataItemNameUnique(itemName, keyValue);
            return Content(IsOk.ToString());
        }
        #endregion

    }


}
