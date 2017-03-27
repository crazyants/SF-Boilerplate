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
using SF.Module.Backend.Domain.Role.ViewModel;
using System.Threading.Tasks;
using SF.Module.Backend.Domain.DataItemDetail.Service;
using SF.Module.Backend.Domain.Organize.Service;
using SF.Module.Backend.Domain.Department.Service;
using Microsoft.AspNetCore.Identity;
using SF.Module.Backend.Domain.Role.Rule;
using SF.Core.Errors.Exceptions;
using SF.Module.Backend.Domain.Role.Service;

namespace SF.Module.Backend.Controllers
{
    /// <summary>
    /// 角色API
    /// </summary>
    [Authorize]
    [Route("Api/Role/")]
    public class RoleApiController : SF.Web.Base.Controllers.ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly RoleManager<RoleEntity> _roleManager;
        private readonly IRoleRules _roleRules;
        private readonly IRoleService _roleService;

        private ICrudDtoMapper<RoleEntity, RoleViewModel, long> _crudDtoMapper;
        public RoleApiController(IServiceCollection collection, ILogger<RoleApiController> logger,
             IRoleService roleService,
             IMediator mediator,
             RoleManager<RoleEntity> roleManager,
             IRoleRules roleRules,
             IServiceCollection service,
             ICrudDtoMapper<RoleEntity, RoleViewModel, long> crudDtoMapper)
            : base(service, logger)
        {
            this._roleService = roleService;
            this._mediator = mediator;
            this._roleManager = roleManager;
            this._crudDtoMapper = crudDtoMapper;
            this._roleRules = roleRules;
        }

        #region 获取数据
        /// <summary>
        /// 异步获取模型数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                var codetableEntity = await _roleManager.FindByIdAsync(id.ToString());
                if (codetableEntity == null)
                    return NotFoundResult($"Code with id {id} not found in {nameof(RoleViewModel)}.");
                RoleViewModel model = _crudDtoMapper.MapEntityToDto(codetableEntity);

                return OkResult(model.ToJson());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex, "Error while loading {0} with id '{1}'", nameof(RoleViewModel), id);
            }
        }
        /// <summary>
        /// 列表 
        /// </summary>
        /// <param name="value">当前主键</param>
        /// <returns>返回树形Json</returns>
        [Route("GetListJson")]
        public IActionResult GetListJson()
        {
            var data = _roleManager.Roles;
            var dtos = this._crudDtoMapper.MapEntityToDtos(data);
            return Content(dtos.ToJson());
        }

        /// <summary>
        /// 列表 
        /// </summary>
        /// <param name="category">类型ID</param>
        /// <param name="condition">查询条件</param>
        /// <param name="keyword">关键字</param>
        /// <returns>返回树形列表Json</returns>
        [Route("GetPageListJson")]
        public IActionResult GetPageListJson(JqGridRequest request, string condition, string keyword)
        {
            var data = _roleManager.Roles;
            var dtos = this._crudDtoMapper.MapEntityToDtos(data);
            JqGridResponse response = new JqGridResponse();
            foreach (RoleViewModel userInput in dtos)
            {
                response.Records.Add(new JqGridRecord(Convert.ToString(userInput.Id), userInput));
            }

            response.Reader.RepeatItems = false;
            return new JqGridJsonResult(response);
        }
        #endregion

        #region 验证数据

        /// <summary>
        /// 名称不能重复
        /// </summary>
        /// <param name="fullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        [Route("ExistFullName")]
        public ActionResult ExistFullName(string fullName, long keyValue)
        {
            bool IsOk = this._roleRules.IsRoleNameUnique(fullName, keyValue);
            return Content(IsOk.ToString());
        }
        #endregion

        #region 增删改
        /// <summary>
        /// 异步插入表单到数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostAsync(RoleViewModel model)
        {
            //if (!ModelState.IsValid)
            //    return BadRequestResult(ModelState);
            try
            {
                var entity = _crudDtoMapper.MapDtoToEntity(model);
                var insertedEntity = await _roleManager.CreateAsync(entity);

                return Success("更新成功!");
            }
            catch (ValidationException validationEx)
            {
                return BadRequestResult(validationEx);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex, "Error while inserting {0}", typeof(RoleViewModel).Name);
            }
        }
        /// <summary>
        /// 异步更新表单到数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, RoleViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequestResult(ModelState);

            try
            {
                if (model == null) throw new ValidationException("model not provided");
                if (id != model.Id) throw new ValidationException("id does not match model id");
                var codetableEntity = await _roleManager.FindByIdAsync(id.ToString());
                if (codetableEntity == null)
                    return NotFoundResult($"Code with id {id} not found in {typeof(RoleViewModel).Name}.");
                var entity = _crudDtoMapper.MapDtoToEntity(model, codetableEntity);
                await _roleManager.UpdateAsync(entity);

                return Success("updated success");
            }
            catch (EntityNotFoundException)
            {
                return NotFoundResult("No event found with id {0}", id);
            }
            catch (ValidationException validationEx)
            {
                return BadRequestResult(validationEx);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex, "Error while updating {0}", typeof(RoleViewModel).Name);
            }
        }
        /// <summary>
        /// 异步删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {

            try
            {
                var codetableEntity = await _roleManager.FindByIdAsync(id.ToString());
                if (codetableEntity == null)
                    return NotFoundResult($"Code with id {id} not found in {typeof(RoleViewModel).Name}.");
                await _roleManager.DeleteAsync(codetableEntity);
                return Success("delete success");
            }
            catch (EntityNotFoundException)
            {
                return NotFoundResult("No event found with id {0}", id);
            }
            catch (ValidationException validationEx)
            {
                return BadRequestResult(validationEx);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex, "Error while deleting {0}", typeof(RoleViewModel).Name);
            }
        }
        #endregion

        #region 保存授权
        /// <summary>
        /// 保存角色授权
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <param name="moduleIds">功能Id</param>
        /// <param name="moduleButtonIds">按钮Id</param>
        /// <returns></returns>
        [Route("SaveAuthorize")]
        public async Task<IActionResult> SaveAuthorize(long roleId, string moduleIds, string moduleButtonIds)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            var roleModules = role.RoleModules;
            var rolePermissions = role.RolePermissions;
            string[] arrayModuleId = moduleIds.Split(',');
            string[] arrayModuleButtonId = moduleButtonIds.Split(',');
            _roleService.SaveRoleAuthorize(roleId, arrayModuleId, arrayModuleButtonId);
            return Success("保存成功。");
        }
        #endregion
    }


}
