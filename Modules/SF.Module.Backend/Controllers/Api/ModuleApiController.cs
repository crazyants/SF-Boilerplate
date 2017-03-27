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
using SF.Module.Backend.Domain.Module.Service;
using SF.Module.Backend.Domain.Module.ViewModel;
using System.Threading.Tasks;
using SF.Web.Security;

namespace SF.Module.Backend.Controllers
{
    /// <summary>
    /// 菜单管理API
    /// </summary>
    [Authorize]
    [Route("Api/Module/")]
    public class ModuleApiController : CrudControllerBase<ModuleEntity, ModuleViewModel, long>
    {
        private readonly IMediator _mediator;
        private readonly IModuleService _moduleService;
        private readonly IBackendUnitOfWork _backendUnitOfWork;
        public ModuleApiController(IServiceCollection collection, ILogger<ModuleApiController> logger,
             IBackendUnitOfWork backendUnitOfWork,
             IMediator mediator,
             IModuleService moduleService)
            : base(backendUnitOfWork, collection, logger)
        {
            this._backendUnitOfWork = backendUnitOfWork;
            this._mediator = mediator;
            this._moduleService = moduleService;

        }
        #region 事件

        /// <summary>
        /// 新增后
        /// </summary>
        /// <param name="arg"></param>
        protected override void OnAfterAdd(CrudEventArgs<ModuleEntity, ModuleViewModel, long> arg)
        {
            this._mediator.Publish(new EntityCreatedEventData<ModuleEntity>(arg.Entity));
        }
        /// <summary>
        /// 编辑后
        /// </summary>
        /// <param name="arg"></param>
        protected override void OnAfterEdit(CrudEventArgs<ModuleEntity, ModuleViewModel, long> arg)
        {
            this._mediator.Publish(new EntityUpdatedEventData<ModuleEntity>(arg.Entity));
        }
        /// <summary>
        /// 删除后
        /// </summary>
        /// <param name="arg"></param>
        protected override void OnAfterDeletet(CrudEventArgs<ModuleEntity, ModuleViewModel, long> arg)
        {
            this._mediator.Publish(new EntityDeletedEventData<ModuleEntity>(arg.Entity));
        }

        #endregion

        #region 获取数据

        #region 菜单功能

        [Route("GetChildren/{id}")]
        public async Task<IQueryable<TreeViewItem>> GetChildren(
        int id,
        int rootDataItemId = 0,
        TreeViewItem.GetCountsType countsType = TreeViewItem.GetCountsType.None)
        {
            var qry = await _moduleService.QueryFilterByParentId(id);

            List<ModuleEntity> moduleEntityList = new List<ModuleEntity>();
            List<TreeViewItem> groupNameList = new List<TreeViewItem>();
            var groups = qry.OrderBy(g => g.SortIndex);
            foreach (var group in groups)
            {

                moduleEntityList.Add(group);
                var treeViewItem = new TreeViewItem();
                treeViewItem.Id = group.Id.ToString();
                treeViewItem.Name = group.FullName;
                treeViewItem.IsActive = (group.EnabledMark ?? 0) > 0;

                treeViewItem.IconCssClass = "fa fa-sitemap";

                if (countsType == TreeViewItem.GetCountsType.ChildGroups)
                {
                    treeViewItem.CountInfo = (await this._moduleService.QueryFilterByParentId(group.Id)).Count();
                }

                groupNameList.Add(treeViewItem);

            }

            //快速找出哪些项目有子级
            List<long> resultIds = moduleEntityList.Select(a => a.Id).ToList();
            var qryHasChildrenList = (await this._moduleService.QueryFilterByParentIds(resultIds.ToArray()));

            foreach (var g in groupNameList)
            {
                int groupId = g.Id.AsInteger();
                g.HasChildren = qryHasChildrenList.Any(a => a == groupId);
            }

            return groupNameList.AsQueryable();
        }
        /// <summary>
        /// 菜单列表 
        /// </summary>
        /// <param name="value">当前主键</param>
        /// <returns>返回树形Json</returns>
        [Route("GetTreeJson")]
        public async Task<IActionResult> GetTreeJson(string keyword)
        {

            var data = await _moduleService.GetAlls();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.FullName.Contains(keyword), "");
            }
            var treeList = new List<TreeEntity>();
            foreach (ModuleEntity item in data)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = data.Any(t => t.ParentId == item.Id) ? true : false;
                tree.id = item.Id.ToString();
                tree.text = item.FullName;
                tree.value = item.Id.ToString();
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                tree.parentId = item.ParentId?.ToString();
                tree.img = item.Icon;
                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson());
        }

        [Route("GetCatalogTreeJson")]
        public async Task<IActionResult> GetCatalogTreeJson(string keyword)
        {
            var data = await _moduleService.GetAlls();
            data = data.FindAll(t => t.IsMenu != 1);
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.FullName.Contains(keyword), "");
            }
            var treeList = new List<TreeEntity>();
            foreach (ModuleEntity item in data)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = data.Any(t => t.ParentId == item.Id) ? false : true;
                tree.id = item.Id.ToString();
                tree.text = item.FullName;
                tree.value = item.Id.ToString();
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                tree.parentId = item.ParentId?.ToString();
                tree.img = item.Icon;
                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson());
        }
        /// <summary>
        /// 菜单列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        [Route("GetListJson")]
        public async Task<IActionResult> GetListJson(JqGridRequest request, ModuleSearchRequest searchRequest)
        {
            var data = await _moduleService.GetPageListBykeyword(searchRequest.ParentId, searchRequest.KeyWord, searchRequest.Condition);
            var dtos = CrudDtoMapper.MapEntityToDtos(data);
            JqGridResponse response = new JqGridResponse();
            foreach (ModuleViewModel userInput in dtos)
            {
                response.Records.Add(new JqGridRecord(Convert.ToString(userInput.Id), userInput));
            }

            response.Reader.RepeatItems = false;
            return new JqGridJsonResult(response);
        }


        #endregion

        #region 操作
        /// <summary>
        /// 按钮列表 
        /// </summary>
        /// <returns>返回列表Json</returns>
        [Route("Button/GetListJson")]
        public async Task<ActionResult> GetListJson(long moduleId, string moduleName)
        {
            var data = new List<PermissionEntity>();
            if (!moduleName.IsEmpty())
                data = await _moduleService.GetButtonByModuleName(moduleName);
            else
                data = await _moduleService.GetButtonByModuleId(moduleId);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 按钮列表 
        /// </summary>
        /// <returns>返回树形列表Json</returns>
        [Route("Button/GetTreeListJson")]
        public async Task<ActionResult> GetTreeListJson(JqGridRequest request, long moduleId, string moduleName)
        {
            var data = await _moduleService.GetButtonByModuleId(moduleId);
            var dtos = Mapper.Map<IEnumerable<PermissionEntity>, IEnumerable<PermissionViemModel>>(data);

            JqGridResponse response = new JqGridResponse();
            foreach (PermissionViemModel userInput in dtos)
            {

                response.Records.Add(new JqGridRecord(Convert.ToString(userInput.Id), userInput));
            }

            response.Reader.RepeatItems = false;
            return new JqGridJsonResult(response);
        }
        #endregion

        #endregion

        #region 提交数据

        /// <summary>
        /// 按钮列表Json转换按钮树形Json 
        /// </summary>
        /// <param name="moduleButtonJson">按钮列表</param>
        /// <returns>返回树形Json</returns>
        [HttpPost]
        [Route("Button/ListToTreeJson")]
        public ActionResult ListToTreeJson(string moduleButtonJson)
        {
            var data = from items in moduleButtonJson.ToList<PermissionViemModel>() select items;
            var treeList = new List<TreeEntity>();
            foreach (PermissionViemModel item in data)
            {
                TreeEntity tree = new TreeEntity();
                tree.id = item.Id.ToString();
                tree.text = item.Description;
                tree.value = item.Name;
                tree.isexpand = true;
                tree.complete = true;
                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson());
        }
        /// <summary>
        /// 按钮列表Json
        /// </summary>
        /// <param name="moduleButtonJson">按钮列表</param>
        /// <returns>返回树形列表Json</returns>
        [HttpPost]
        [Route("Button/ListToListTreeJson")]
        public ActionResult ListToListTreeJson(long moduleId, string moduleButtonJson)
        {
            var data = from items in moduleButtonJson.ToList<PermissionViemModel>() select items;

            JqGridResponse response = new JqGridResponse();
            foreach (PermissionViemModel userInput in data)
            {
                userInput.ModuleId = moduleId;
                response.Records.Add(new JqGridRecord(Convert.ToString(userInput.Id), userInput));
            }

            response.Reader.RepeatItems = false;
            return new JqGridJsonResult(response);

        }
        [HttpPost]
        [Route("SaveModule")]
        public IActionResult SaveModulePost(ModuleViewModel moduleModel, string moduleButtonListJson)
        {
            var moduleButtonList = moduleButtonListJson.ToList<PermissionEntity>();
            var moduleEntity = new ModuleEntity();
            if (moduleModel.Id != 0)
            {
                moduleEntity = _backendUnitOfWork.Module.GetById(moduleModel.Id);
            }
            CrudDtoMapper.MapDtoToEntity(moduleModel, moduleEntity);
            _moduleService.SaveForm(moduleEntity, moduleButtonList);
            this._mediator.Publish(new EntityUpdatedEventData<ModuleEntity>(moduleEntity));
            return Success("Save Success");
        }
        #endregion
    }


}
