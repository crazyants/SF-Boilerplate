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
using SF.Module.Backend.Domain.Department.Service;
using SF.Module.Backend.Domain.Department.ViewModel;
using System.Threading.Tasks;
using SF.Module.Backend.Domain.Department.Rule;
using SF.Module.Backend.Domain.Organize.Service;
using SF.Module.Backend.Domain.DataItemDetail.Service;

namespace SF.Module.Backend.Controllers
{
    /// <summary>
    /// 字典管理API
    /// </summary>
    [Authorize]
    [Route("Api/Department/")]
    public class DepartmentApiController : CrudControllerBase<DepartmentEntity, DepartmentViewModel, long>
    {
        private readonly IMediator _mediator;
        private readonly IOrganizeService _organizeService;
        private readonly IDepartmentService _departmentService;
        private readonly IDataItemDetailService _dataItemDetailService;
        private readonly IBackendUnitOfWork _backendUnitOfWork;
        private readonly IDepartmentRules _departmentRules;
        public DepartmentApiController(IServiceCollection collection, ILogger<DepartmentApiController> logger,
             IBackendUnitOfWork backendUnitOfWork,
             IMediator mediator,
             IDepartmentService departmentService,
             IDepartmentRules departmentRules,
             IOrganizeService organizeService,
             IDataItemDetailService dataItemDetailService)
            : base(backendUnitOfWork, collection, logger)
        {
            this._backendUnitOfWork = backendUnitOfWork;
            this._mediator = mediator;
            this._departmentService = departmentService;
            this._departmentRules = departmentRules;
            this._organizeService = organizeService;
            this._dataItemDetailService = dataItemDetailService;
        }
        #region 事件

        /// <summary>
        /// 新增后
        /// </summary>
        /// <param name="arg"></param>
        protected override void OnAfterAdd(CrudEventArgs<DepartmentEntity, DepartmentViewModel, long> arg)
        {
            this._mediator.Publish(new EntityCreatedEventData<DepartmentEntity>(arg.Entity));
        }
        /// <summary>
        /// 编辑后
        /// </summary>
        /// <param name="arg"></param>
        protected override void OnAfterEdit(CrudEventArgs<DepartmentEntity, DepartmentViewModel, long> arg)
        {
            this._mediator.Publish(new EntityUpdatedEventData<DepartmentEntity>(arg.Entity));
        }
        /// <summary>
        /// 删除后
        /// </summary>
        /// <param name="arg"></param>
        protected override void OnAfterDeletet(CrudEventArgs<DepartmentEntity, DepartmentViewModel, long> arg)
        {
            this._mediator.Publish(new EntityDeletedEventData<DepartmentEntity>(arg.Entity));
        }

        #endregion

        #region 获取数据

        /// <summary>
        /// 机构列表 
        /// </summary>
        /// <param name="value">当前主键</param>
        /// <returns>返回树形Json</returns>
        [Route("GetTreeJson")]
        public async Task<IActionResult> GetTreeJson(string organizeId, string keyword)
        {

            var departmentdata = await _departmentService.GetAlls();
            if (!string.IsNullOrEmpty(organizeId))
            {
                departmentdata = departmentdata.Where(t => t.OrganizeId==organizeId.AsLong()).ToList();
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                departmentdata = departmentdata.TreeWhere(t => t.FullName.Contains(keyword), "");
            }
            var treeList = new List<TreeEntity>();
            foreach (DepartmentEntity item in departmentdata)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = departmentdata.Count(t => t.ParentId == item.Id) == 0 ? false : true;
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
        /// 部门列表
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns>返回机构+部门树形Json</returns>
        [Route("GetOrganizeTreeJson")]
        public async Task<IActionResult> GetOrganizeTreeJson(string keyword)
        {
            var organizedata = await _organizeService.GetAlls();
            var departmentdata = await _departmentService.GetAlls();
            var treeList = new List<TreeEntity>();
            foreach (OrganizeEntity item in organizedata)
            {
                #region 机构
                TreeEntity tree = new TreeEntity();
                bool hasChildren = organizedata.Count(t => t.ParentId == item.Id) == 0 ? false : true;
                if (hasChildren == false)
                {
                    hasChildren = departmentdata.Count(t => t.OrganizeId == item.Id) == 0 ? false : true;
                }
                tree.id = "O_" + item.Id.ToString();
                tree.text = item.FullName;
                tree.value = item.Id.ToString();
                tree.parentId = "O_" + (item.ParentId ?? 0).ToString();
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                tree.Attribute = "Sort";
                tree.AttributeValue = "Organize";
                treeList.Add(tree);
                #endregion
            }
            foreach (DepartmentEntity item in departmentdata)
            {
                #region 部门
                TreeEntity tree = new TreeEntity();
                bool hasChildren = departmentdata.Count(t => t.ParentId == item.Id) == 0 ? false : true;
                tree.id = item.Id.ToString();
                tree.text = item.FullName;
                tree.value = item.Id.ToString();
                var parentId = (item.ParentId ?? 0).ToString();
                if (parentId == "0")
                {
                    tree.parentId = "O_" + (item.OrganizeId ?? 0).ToString();
                }
                else
                {
                    tree.parentId = parentId;
                }
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                tree.Attribute = "Sort";
                tree.AttributeValue = "Department";
                treeList.Add(tree);
                #endregion
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                treeList = treeList.TreeWhere(t => t.text.Contains(keyword), "id", "parentId");
            }
            return Content(treeList.TreeToJson("O_0"));
        }
        /// <summary>
        /// 机构列表 
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="keyword">关键字</param>
        /// <returns>返回树形列表Json</returns>
        [Route("GetTreeListJson")]
        public async Task<IActionResult> GetTreeListJson(string condition, string keyword)
        {
            var organizedata = await _organizeService.GetAlls();
            var departmentdata = _departmentService.GetByWhere(keyword, condition);
            var treeList = new List<TreeGridEntity>();
            foreach (OrganizeEntity item in organizedata)
            {
                TreeGridEntity tree = new TreeGridEntity();
                bool hasChildren = organizedata.Count(t => t.ParentId == item.Id) == 0 ? false : true;
                if (hasChildren == false)
                {
                    hasChildren = departmentdata.Count(t => t.OrganizeId == item.Id) == 0 ? false : true;

                }
                tree.id = "O_" + item.Id.ToString();
                tree.hasChildren = hasChildren;
                tree.parentId = "O_" + (item.ParentId ?? 0).ToString();
                tree.expanded = true;
                item.EnCode = ""; item.ShortName = ""; item.Nature = ""; item.Manager = ""; item.OuterPhone = ""; item.InnerPhone = ""; item.Description = "";
                string itemJson = item.ToJson();
                itemJson = itemJson.Insert(1, "\"Sort\":\"Organize\",");
                tree.entityJson = itemJson;
                treeList.Add(tree);
            }
            foreach (DepartmentEntity item in departmentdata)
            {
                TreeGridEntity tree = new TreeGridEntity();
                bool hasChildren = departmentdata.Count(t => t.ParentId == item.Id) == 0 ? false : true;
                item.Nature = this._dataItemDetailService.GetAlls().Where(x => x.ItemValue == item.Nature)?.FirstOrDefault()?.ItemName;
                tree.id = item.Id.ToString();
                if ((item.ParentId ?? 0).ToString() == "0")
                {
                    tree.parentId = "O_" + (item.OrganizeId ?? 0).ToString();
                }
                else
                {
                    tree.parentId = (item.ParentId ?? 0).ToString();
                }
                tree.expanded = true;
                tree.hasChildren = hasChildren;
                string itemJson = item.ToJson();
                itemJson = itemJson.Insert(1, "\"Sort\":\"Department\",");
                tree.entityJson = itemJson;
                treeList.Add(tree);
            }
            return Content(treeList.TreeJson("O_0"));
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
            bool IsOk = _departmentRules.IsDepartmentCodeUnique(enCode, keyValue);
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
            bool IsOk = _departmentRules.IsDepartmentNameUnique(fullName, keyValue);
            return Content(IsOk.ToString());
        }
        #endregion

    }


}
