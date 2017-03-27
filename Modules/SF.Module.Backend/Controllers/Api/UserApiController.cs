using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SF.Core.Abstraction.Data;
using SF.Entitys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using SF.Core.Common;
using SF.Data;
using SF.Core.Abstraction.UoW.Helper;
using SF.Web.Security;
using Microsoft.AspNetCore.Identity;
using SF.Module.Backend.Domain.User.ViewModel;
using SF.Web.Base.DataContractMapper;
using SF.Web.Control.JqGrid.Core.Request;
using SF.Web.Control.JqGrid.Core.Response;
using SF.Web.Control.JqGrid.Core.Json;
using SF.Module.Backend.Domain.User.Rule;
using SF.Core.Errors.Exceptions;
using System.Collections.Generic;
using SF.Core.Extensions;
using SF.Core.Abstraction.Resolvers;
using SF.Entitys.Abstraction;
using LinqKit;

namespace SF.Module.Backend.Controllers
{
    [Authorize(Roles = "Administrators")]
    [Route("Api/User")]
    public class UserApiController : SF.Web.Base.Controllers.ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IBaseUnitOfWork _baseUnitOfWork;
        private readonly ISecurityService _securityService;
        private readonly IUserRules _userRules;
        private readonly IUserNameResolver _userNameResolver;
        private ICrudDtoMapper<UserEntity, UserViewModel, long> _crudDtoMapper;
        public UserApiController(IBaseUnitOfWork baseUnitOfWork,
            UserManager<UserEntity> userManager,
            IUserRules userRules,
            ISecurityService securityService,
            IUserNameResolver userNameResolver,
            IServiceCollection service,
            ILogger<UserApiController> logger,
            ICrudDtoMapper<UserEntity, UserViewModel, long> crudDtoMapper) : base(service, logger)
        {
            this._baseUnitOfWork = baseUnitOfWork;
            this._userManager = userManager;
            this._userRules = userRules;
            this._userNameResolver = userNameResolver;
            this._securityService = securityService;
            this._crudDtoMapper = crudDtoMapper;
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
                var entity = await _userManager.FindByIdAsync(id.ToString());
                if (entity == null)
                    return NotFoundResult($"Code with id {id} not found in {nameof(UserViewModel)}.");
                UserViewModel model = _crudDtoMapper.MapEntityToDto(entity);

                return OkResult(model.ToJson());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex, "Error while loading {0} with id '{1}'", nameof(UserViewModel), id);
            }
        }
        /// <summary>
        /// 列表 
        /// </summary>
        /// <param name="value">当前主键</param>
        /// <returns>返回树形Json</returns>
        [Route("GetListJson")]
        public IActionResult GetListJson(long? departmentId)
        {
            var data = _userManager.Users.Where(x => x.DepartmentId == departmentId);
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

            var predicate = PredicateBuilder.New<UserEntity>(true);

            #region 多条件查询
            if (!keyword.IsEmpty())
            {
                switch (condition)
                {
                    case "Account":            //账户
                        predicate.And(t => t.UserName.Contains(keyword));
                        break;
                    case "RealName":          //姓名
                        predicate.And(t => t.DisplayName.Contains(keyword));
                        break;
                    case "Mobile":          //手机
                        predicate.And(t => t.Mobile.Contains(keyword));
                        break;
                    default:
                        break;

                }
            }
            #endregion
             var data = _userManager.Users.Where(predicate);
            if (request.RecordsCount != 0)
            {
                data = data.Skip(request.PageIndex * request.RecordsCount).Take(request.RecordsCount);
            }
            var dtos = this._crudDtoMapper.MapEntityToDtos(data);
            JqGridResponse response = new JqGridResponse();
            foreach (UserViewModel userInput in dtos)
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
        [Route("ExistUserName")]
        public ActionResult ExistUserName(string userName, long keyValue)
        {
            bool IsOk = this._userRules.IsUserNameUnique(userName, keyValue);
            return Content(IsOk.ToString());
        }

        /// <summary>
        /// 邮箱不能重复
        /// </summary>
        /// <param name="fullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        [Route("ExistEmail")]
        public ActionResult ExistEmail(string email, long keyValue)
        {
            bool IsOk = this._userRules.IsUserEmailUnique(email, keyValue);
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
        public async Task<IActionResult> PostAsync(UserViewModel model)
        {
            //if (!ModelState.IsValid)
            //    return BadRequestResult(ModelState);
            try
            {
                var entity = _crudDtoMapper.MapDtoToEntity(model);
                entity.DefaultCreate(this._userNameResolver.GetCurrentUserName());
                entity.DefaultUpdate(this._userNameResolver.GetCurrentUserName());
                var insertedEntity = await _userManager.CreateAsync(entity, model.Password);

                return Success("更新成功!");
            }
            catch (ValidationException validationEx)
            {
                return BadRequestResult(validationEx);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex, "Error while inserting {0}", typeof(UserViewModel).Name);
            }
        }
        /// <summary>
        /// 异步更新表单到数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, UserViewModel model)
        {
            //if (!ModelState.IsValid)
            //    return BadRequestResult(ModelState);

            try
            {
                if (model == null) throw new ValidationException("model not provided");
                if (id != model.Id) throw new ValidationException("id does not match model id");
                var entity = await _userManager.FindByIdAsync(id.ToString());
                if (entity == null)
                    return NotFoundResult($"Code with id {id} not found in {typeof(UserViewModel).Name}.");
                //  var entity = _crudDtoMapper.MapDtoToEntity(model, entity);
                entity.DisplayName = model.DisplayName;
                entity.UserNo = model.UserNo;
                entity.Gender = model.Gender;
                entity.Birthday = model.Birthday;
                entity.Mobile = model.Mobile;
                entity.OICQ = model.OICQ;
                entity.WeChat = model.WeChat;
                entity.MSN = model.MSN;
                entity.ManagerId = model.ManagerId;
                entity.OrganizeId = model.OrganizeId;
                entity.DepartmentId = model.DepartmentId;
                entity.DutyId = model.DutyId;
                entity.PostId = model.PostId;
                entity.WorkGroupId = model.WorkGroupId;
                entity.Description = model.Description;

                entity.DefaultUpdate(this._userNameResolver.GetCurrentUserName());
                await _userManager.UpdateAsync(entity);

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
                return InternalServerError(ex, "Error while updating {0}", typeof(UserViewModel).Name);
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
                var entity = await _userManager.FindByIdAsync(id.ToString());
                if (entity == null)
                    return NotFoundResult($"Code with id {id} not found in {typeof(UserViewModel).Name}.");
                await _userManager.DeleteAsync(entity);
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
                return InternalServerError(ex, "Error while deleting {0}", typeof(UserViewModel).Name);
            }
        }
        #endregion


        #region 其他业务

        /// <summary>
        /// 保存重置修改密码
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="Password">新密码</param>
        /// <returns></returns>
        [Route("SaveRevisePassword")]
        public async Task<ActionResult> SaveRevisePassword(string userId, string password)
        {
            if (userId == "Administrator")
            {
                throw new Exception("当前账户不能重置密码");
            }
            var user = await _userManager.FindByNameAsync(userId);
            if (user == null)
            {
                return Error("用户不存在。");
            }
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, code, password);
            if (result.Succeeded)
            {
                return Error(result.Errors.First()?.Description);
            }
            return Success("密码修改成功，请牢记新密码。");
        }
        /// <summary>
        /// 禁用账户
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [Route("DisabledAccount")]
        public async Task<ActionResult> DisabledAccount(string userId)
        {
            if (userId == "Administrator")
            {
                return Error("当前账户不禁用。");
                // throw new Exception("当前账户不禁用");
            }
            var user = await _userManager.FindByNameAsync(userId);
            if (user == null)
            {
                return Error("用户不存在。");
            }
            user.IsLockedOut = false;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return Error(result.Errors.First()?.Description);
            }
            return Success("账户禁用成功。");
        }
        /// <summary>
        /// 启用账户
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [Route("EnabledAccount")]
        public async Task<ActionResult> EnabledAccount(string userId)
        {
            var user = await _userManager.FindByNameAsync(userId);
            if (user == null)
            {
                return Error("用户不存在。");
            }
            user.IsLockedOut = true;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return Error(result.Errors.First()?.Description);
            }
            return Success("账户启用成功。");
        }
        /// <summary>
        /// 保存角色授权
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <param name="moduleIds">功能Id</param>
        /// <param name="moduleButtonIds">按钮Id</param>
        /// <returns></returns>
        [Route("SaveAuthorize")]
        public async Task<IActionResult> SaveAuthorize(long userId, string roleIds)
        {
            string[] arrayRoleId = roleIds.Split(',');
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var userRoles = await _userManager.GetRolesAsync(user);

            var result = await _userManager.AddToRolesAsync(user,
                arrayRoleId.Except(userRoles).ToArray<string>());
            if (!result.Succeeded)
            {
                return Error(result.Errors.First()?.Description);
            }
            result = await _userManager.RemoveFromRolesAsync(user,
               userRoles.Except(arrayRoleId).ToArray<string>());
            if (!result.Succeeded)
            {
                return Error(result.Errors.First()?.Description);
            }
            return Success("保存成功。");
        }
        #endregion
    }
}
