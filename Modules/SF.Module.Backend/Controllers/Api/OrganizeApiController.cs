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
using SF.Module.Backend.Domain.Organize.Service;
using SF.Module.Backend.Domain.Organize.ViewModel;
using System.Threading.Tasks;
using SF.Module.Backend.Domain.Organize.Rule;

namespace SF.Module.Backend.Controllers
{
    /// <summary>
    /// 字典管理API
    /// </summary>
    [Authorize]
    [Route("Api/Organize/")]
    public class OrganizeApiController : CrudControllerBase<OrganizeEntity, OrganizeViewModel, long>
    {
        private readonly IMediator _mediator;
        private readonly IOrganizeService _organizeService;
        private readonly IBackendUnitOfWork _backendUnitOfWork;
        private readonly IOrganizeRules _organizeRules;
        public OrganizeApiController(IServiceCollection collection, ILogger<OrganizeApiController> logger,
             IBackendUnitOfWork backendUnitOfWork,
             IMediator mediator,
             IOrganizeService organizeService,
             IOrganizeRules organizeRules)
            : base(backendUnitOfWork, collection, logger)
        {
            this._backendUnitOfWork = backendUnitOfWork;
            this._mediator = mediator;
            this._organizeService = organizeService;
            this._organizeRules = organizeRules;
        }
        #region 事件

        /// <summary>
        /// 新增后
        /// </summary>
        /// <param name="arg"></param>
        protected override void OnAfterAdd(CrudEventArgs<OrganizeEntity, OrganizeViewModel, long> arg)
        {
            this._mediator.Publish(new EntityCreatedEventData<OrganizeEntity>(arg.Entity));
        }
        /// <summary>
        /// 编辑后
        /// </summary>
        /// <param name="arg"></param>
        protected override void OnAfterEdit(CrudEventArgs<OrganizeEntity, OrganizeViewModel, long> arg)
        {
            this._mediator.Publish(new EntityUpdatedEventData<OrganizeEntity>(arg.Entity));
        }
        /// <summary>
        /// 删除后
        /// </summary>
        /// <param name="arg"></param>
        protected override void OnAfterDeletet(CrudEventArgs<OrganizeEntity, OrganizeViewModel, long> arg)
        {
            this._mediator.Publish(new EntityDeletedEventData<OrganizeEntity>(arg.Entity));
        }

        #endregion

        #region 获取数据

        /// <summary>
        /// 机构列表 
        /// </summary>
        /// <param name="value">当前主键</param>
        /// <returns>返回树形Json</returns>
        [Route("GetTreeJson")]
        public async Task<IActionResult> GetTreeJson(string keyword)
        {

            var data = await _organizeService.GetAlls();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.FullName.Contains(keyword), "");
            }
            var treeList = new List<TreeEntity>();
            foreach (OrganizeEntity item in data)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = data.Count(t => t.ParentId == item.Id) == 0 ? false : true;
                tree.id = item.Id.ToString();
                tree.text = item.FullName;
                tree.value = item.Id.ToString();
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                tree.parentId = (item.ParentId ?? 0).ToString();
                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson());
        }

        /// <summary>
        /// 机构列表 
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="keyword">关键字</param>
        /// <returns>返回树形列表Json</returns>
        [Route("GetTreeListJson")]
        public IActionResult GetTreeListJson(string condition, string keyword)
        {
            var data = _organizeService.GetByWhere(keyword, condition);
            var treeList = new List<TreeGridEntity>();
            foreach (OrganizeEntity item in data)
            {
                TreeGridEntity tree = new TreeGridEntity();
                bool hasChildren = data.Count(t => t.ParentId == item.Id) == 0 ? false : true;
                tree.id = item.Id.ToString();
                tree.hasChildren = hasChildren;
                tree.parentId = (item.ParentId ?? 0).ToString();
                tree.expanded = true;
                tree.entityJson = item.ToJson();
                treeList.Add(tree);
            }
            return Content(treeList.TreeJson());
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
            bool IsOk = _organizeRules.IsOrganizeCodeUnique(enCode, keyValue);
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
            bool IsOk = _organizeRules.IsOrganizeNameUnique(fullName, keyValue);
            return Content(IsOk.ToString());
        }

        /// <summary>
        /// 中文名称不能重复
        /// </summary>
        /// <param name="shortName">中文名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [Route("ExistShortName")]
        public ActionResult ExistShortName(string shortName, long keyValue)
        {
            bool IsOk = _organizeRules.IsOrganizeShortNameUnique(shortName, keyValue);
            return Content(IsOk.ToString());
        }
        #endregion

    }


}
