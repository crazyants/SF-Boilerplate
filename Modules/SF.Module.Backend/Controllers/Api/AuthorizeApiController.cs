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
using SF.Module.Backend.Domain.Module.Service;
using Microsoft.AspNetCore.Identity;

namespace SF.Module.Backend.Controllers
{
    /// <summary>
    /// 字典管理API
    /// </summary>
    [Authorize]
    [Route("Api/Authorize/")]
    public class AuthorizeApiController : CrudControllerBase<OrganizeEntity, OrganizeViewModel, long>
    {
        private readonly IMediator _mediator;
        private readonly IModuleService _moduleService;
        private readonly IOrganizeService _organizeService;
        private readonly IBackendUnitOfWork _backendUnitOfWork;
        private readonly IBaseUnitOfWork _baseUnitOfWork;
        private readonly IOrganizeRules _organizeRules;
        private readonly RoleManager<RoleEntity> _roleManager;
        private readonly UserManager<UserEntity> _userManager;
        public AuthorizeApiController(IServiceCollection collection, ILogger<OrganizeApiController> logger,
             IBackendUnitOfWork backendUnitOfWork,
             IBaseUnitOfWork baseUnitOfWork,
             IMediator mediator,
             IModuleService moduleService,
             IOrganizeService organizeService,
             IOrganizeRules organizeRules,
             RoleManager<RoleEntity> roleManager,
             UserManager<UserEntity> userManager)
            : base(backendUnitOfWork, collection, logger)
        {
            this._backendUnitOfWork = backendUnitOfWork;
            this._baseUnitOfWork = baseUnitOfWork;
            this._mediator = mediator;
            this._moduleService = moduleService;
            this._organizeService = organizeService;
            this._organizeRules = organizeRules;
            this._roleManager = roleManager;
            this._userManager = userManager;
        }

        #region 获取数据

        /// <summary>
        /// 系统功能列表 
        /// </summary>
        /// <param name="value">角色Id</param>
        /// <returns>返回树形Json</returns>
        [Route("GetModuleTreeJson")]
        public async Task<ActionResult> GetModuleTreeJson(long roleId)
        {
            var data = await _moduleService.GetAlls();
            //   var role = await _roleManager.FindByIdAsync(roleId);
            //   var roleModules = role.RoleModules;
            var roleModules = _baseUnitOfWork.BaseWorkArea.RoleModule.QueryFilter(x => x.RoleId == roleId);
            var treeList = new List<TreeEntity>();
            foreach (ModuleEntity item in data)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = data.Count(t => t.ParentId == item.Id) == 0 ? false : true;
                tree.id = item.Id.ToString();
                tree.text = item.FullName;
                tree.value = item.Id.ToString();
                tree.title = "";
                tree.checkstate = roleModules.Count(t => t.ModuleId == item.Id);
                tree.showcheck = true;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                tree.parentId = (item.ParentId ?? 0).ToString();
                tree.img = item.Icon;
                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson());
        }

        /// <summary>
        /// 系统功能列表 
        /// </summary>
        /// <param name="value">角色Id</param>
        /// <returns>返回树形Json</returns>
        [Route("GetModuleButtonTreeJson")]
        public async Task<ActionResult> GetModuleButtonTreeJson(long roleId)
        {
            var moduleData = await _moduleService.GetAlls();
            var permissionData = _backendUnitOfWork.Permission.Query();

            var roleModules = _baseUnitOfWork.BaseWorkArea.RoleModule.QueryFilter(x => x.RoleId == roleId);
            var rolePermissions = _baseUnitOfWork.BaseWorkArea.RolePermission.QueryFilter(x => x.RoleId == roleId);
            var treeList = new List<TreeEntity>();
            foreach (ModuleEntity item in moduleData)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = moduleData.Count(t => t.ParentId == item.Id) == 0 ? false : true;
                if (!hasChildren)
                    hasChildren = permissionData.Count(t => t.ModuleId == item.Id) == 0 ? false : true;
                tree.id = "M_" + item.Id.ToString();
                tree.text = item.FullName;
                tree.value = item.Id.ToString();
                tree.title = "";
                tree.checkstate = roleModules.Count(t => t.ModuleId == item.Id);
                tree.showcheck = true;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                tree.parentId = "M_" + (item.ParentId ?? 0).ToString();
                tree.img = item.Icon;
                treeList.Add(tree);
            }
            foreach (PermissionEntity item in permissionData)
            {
                TreeEntity tree = new TreeEntity();
                tree.id = item.Id.ToString();
                tree.text = item.Description;
                tree.value = item.Id.ToString();
                tree.parentId = "M_" + item.ModuleId.ToString();
                tree.checkstate = rolePermissions.Count(t => t.PermissionId == item.Id);
                tree.showcheck = true;
                tree.isexpand = true;
                tree.complete = true;
                tree.img = "fa fa-wrench " + item.ModuleId;
                tree.hasChildren = false;
                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson("M_0"));
        }


        /// <summary>
        /// 角色列表 
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="keyword">关键字</param>
        /// <returns>返回树形列表Json</returns>
        [Route("GetRoleTreeJson")]
        public async Task<ActionResult> GetRoleTreeJson(long userId)
        {
            var data = _roleManager.Roles.Where(x => x.Enabled > 0);
            var user =await this._userManager.FindByIdAsync(userId.ToString());
            var userRoles = await _userManager.GetRolesAsync(user);
            var treeList = new List<TreeEntity>();
            foreach (RoleEntity item in data)
            {
                TreeEntity tree = new TreeEntity();
                tree.id = item.Name.ToString();
                tree.text = item.Description;
                tree.value = item.Name.ToString();
                tree.checkstate = userRoles.Count(t => t == item.Name);
                tree.parentId = "0";
                tree.hasChildren = false;
                tree.showcheck = true;
                tree.isexpand = true;
                tree.complete = true;
                tree.img = "fa fa-wrench ";
                tree.hasChildren = false;
                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson());
        }
        #endregion

        #region 提交数据

        #endregion

    }


}
