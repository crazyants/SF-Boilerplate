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
using SF.Module.Backend.Domain.Area.Service;
using SF.Module.Backend.Domain.Area.ViewModel;
using System.Threading.Tasks;

namespace SF.Module.Backend.Controllers
{
    /// <summary>
    /// 字典管理API
    /// </summary>
    [Authorize]
    [Route("Api/Area/")]
    public class AreaApiController : CrudControllerBase<AreaEntity, AreaViewModel,long>
    {
        private readonly IMediator _mediator;
        private readonly IAreaService _areaService;
        private readonly IBackendUnitOfWork _backendUnitOfWork;
        public AreaApiController(IServiceCollection collection, ILogger<AreaApiController> logger,
             IBackendUnitOfWork backendUnitOfWork,
             IMediator mediator,
             IAreaService areaService)
            : base(backendUnitOfWork, collection, logger)
        {
            this._backendUnitOfWork = backendUnitOfWork;
            this._mediator = mediator;
            this._areaService = areaService;

        }
        #region 事件
        
        /// <summary>
        /// 新增后
        /// </summary>
        /// <param name="arg"></param>
        protected override void OnAfterAdd(CrudEventArgs<AreaEntity, AreaViewModel, long> arg)
        {
            this._mediator.Publish(new EntityCreatedEventData<AreaEntity>(arg.Entity));
        }
        /// <summary>
        /// 编辑后
        /// </summary>
        /// <param name="arg"></param>
        protected override void OnAfterEdit(CrudEventArgs<AreaEntity, AreaViewModel, long> arg)
        {
            this._mediator.Publish(new EntityUpdatedEventData<AreaEntity>(arg.Entity));
        }
        /// <summary>
        /// 删除后
        /// </summary>
        /// <param name="arg"></param>
        protected override void OnAfterDeletet(CrudEventArgs<AreaEntity, AreaViewModel, long> arg)
        {
            this._mediator.Publish(new EntityDeletedEventData<AreaEntity>(arg.Entity));
        }

        #endregion

        #region 获取数据
        [Route("GetChildren/{id}")]
        public async Task<IQueryable<TreeViewItem>> GetChildren(
        int id,
        int rootDataItemId = 0,
        TreeViewItem.GetCountsType countsType = TreeViewItem.GetCountsType.None)
        {
            var qry = await _areaService.QueryFilterByParentId(id);

            List<AreaEntity> areaEntityList = new List<AreaEntity>();
            List<TreeViewItem> groupNameList = new List<TreeViewItem>();
            var groups = qry.OrderBy(g => g.SortIndex);
            foreach (var group in groups)
            {

                areaEntityList.Add(group);
                var treeViewItem = new TreeViewItem();
                treeViewItem.Id = group.Id.ToString();
                treeViewItem.Name = group.AreaName;
                treeViewItem.IsActive = (group.EnabledMark ?? 0) > 0;

                treeViewItem.IconCssClass = "fa fa-sitemap";

                if (countsType == TreeViewItem.GetCountsType.ChildGroups)
                {
                    treeViewItem.CountInfo = (await this._areaService.QueryFilterByParentId(group.Id)).Count();
                }

                groupNameList.Add(treeViewItem);

            }

            //快速找出哪些项目有子级
            List<long> resultIds = areaEntityList.Select(a => a.Id).ToList();
            var qryHasChildrenList = (await this._areaService.QueryFilterByParentIds(resultIds.ToArray()));

            foreach (var g in groupNameList)
            {
                int groupId = g.Id.AsInteger();
                g.HasChildren = qryHasChildrenList.Any(a => a == groupId);
            }

            return groupNameList.AsQueryable();
        }
        /// <summary>
        /// 区域列表 
        /// </summary>
        /// <param name="value">当前主键</param>
        /// <returns>返回树形Json</returns>
        [Route("GetTreeJson")]
        public async Task<IActionResult> GetTreeJson(long? value)
        {

            var filterdata = await _areaService.QueryFilterByParentId(value ?? 0);
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            if (filterdata.Count > 0)
            {
                foreach (AreaEntity item in filterdata)
                {
                    bool hasChildren = _areaService.QueryFilterByParentId(item.Id).Result.Any() ? true : false;
                    sb.Append("{");
                    sb.Append("\"id\":\"" + item.Id + "\",");
                    sb.Append("\"text\":\"" + item.AreaName + "\",");
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

        /// <summary>
        /// 区域列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        [Route("GetListJson")]
        public async Task<IActionResult> GetListJson(JqGridRequest request, long value, string keyword)
        {

            var data = await _areaService.GetPageListBykeyword(value, keyword);
            var dtos = CrudDtoMapper.MapEntityToDtos(data);
            JqGridResponse response = new JqGridResponse();
            foreach (AreaViewModel userInput in dtos)
            {
                response.Records.Add(new JqGridRecord(Convert.ToString(userInput.Id), userInput));
            }

            response.Reader.RepeatItems = false;
            return new JqGridJsonResult(response);
        }

        /// <summary>
        /// 区域列表（主要是绑定下拉框）
        /// </summary>
        /// <param name="parentId">节点Id</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        [Route("GetAreaListJson/{id}")]
        public async Task<IActionResult> GetAreaListJson(long id)
        {
            var data =await _areaService.QueryFilterByParentId(id);
            return Content(data.ToJson());
        }
        #endregion


    }


}
