
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using SF.Entitys.Abstraction;
using SF.Web.Base.Business;
using SF.Core.Errors.Exceptions;
using SF.Web.Base.DataContractMapper;
using SF.Core.Abstraction.Data;
using SF.Core.EFCore.UoW;
using SF.Core.Abstraction.UoW;
using SF.Data.Repository;
using Microsoft.EntityFrameworkCore;
using SF.Core.EFCore.Repository;
using SF.Core.Common;
using SF.Web.Base.Args;
using SF.Web.Models;
using SF.Core.Extensions;
using System.Collections.Generic;
using SF.Core.Abstraction.Domain;

namespace SF.Web.Base.Controllers
{
    /// <summary>
    /// 控制器基类，实现增删改查基础功能
    /// </summary>
    /// <typeparam name="TCodeTabelEntity"></typeparam>
    /// <typeparam name="TCodeTabelModel"></typeparam>
    public abstract class CrudControllerBase<TCodeTabelEntity, TCodeTabelModel, Tkey> : ControllerBase
        where TCodeTabelEntity : BaseEntity<Tkey>, new()
        where TCodeTabelModel : EntityModelBase<Tkey>, new()
    {
        #region Fields
        protected readonly FluentValidation.IValidator<TCodeTabelModel> _validator;
        protected readonly IGenericReaderService<TCodeTabelEntity, Tkey> _readerService;
        protected readonly IGenericWriterService<TCodeTabelEntity, Tkey> _writerService;
        protected readonly IEFCoreQueryableRepository<TCodeTabelEntity, Tkey> _repository;

        #endregion

        #region Constructors
        /// <summary>
        /// 数据转换器
        /// </summary>
        /// <returns></returns>
        protected ICrudDtoMapper<TCodeTabelEntity, TCodeTabelModel, Tkey> CrudDtoMapper { get; set; }
        /// <summary>
        /// 初始化构造
        /// 使用注入的同一个上下文
        /// </summary>
        /// <param name="service">服务集合</param>
        /// <param name="logger">日志</param>
        protected CrudControllerBase(IServiceCollection service, ILogger<Controller> logger) : base(service, logger)
        {

            _validator = service.BuildServiceProvider().GetService<FluentValidation.IValidator<TCodeTabelModel>>();
            _readerService = service.BuildServiceProvider().GetService<IGenericReaderService<TCodeTabelEntity, Tkey>>();
            _writerService = service.BuildServiceProvider().GetService<IGenericWriterService<TCodeTabelEntity, Tkey>>();
            _repository = service.BuildServiceProvider().GetService<IEFCoreQueryableRepository<TCodeTabelEntity, Tkey>>();
            CrudDtoMapper = service.BuildServiceProvider().GetService<ICrudDtoMapper<TCodeTabelEntity, TCodeTabelModel, Tkey>>();

        }
        /// <summary>
        /// 初始化构造
        /// 用于不同的个上下文，使用注入的工作单元
        /// </summary>
        /// <param name="dbContext">上下文实例</param>
        /// <param name="service">服务集合</param>
        /// <param name="logger">日志</param>
        protected CrudControllerBase(IEFCoreUnitOfWork unitOfWork, IServiceCollection service, ILogger<Controller> logger) : base(service, logger)
        {

            _validator = service.BuildServiceProvider().GetService<FluentValidation.IValidator<TCodeTabelModel>>();
            _repository = new EFCoreBaseRepository<TCodeTabelEntity, Tkey>(unitOfWork.Context);
            _readerService = new GenericReaderService<TCodeTabelEntity, Tkey>(logger, _repository);
            _writerService = new GenericWriterService<TCodeTabelEntity, Tkey>(logger, _repository, unitOfWork);
            CrudDtoMapper = service.BuildServiceProvider().GetService<ICrudDtoMapper<TCodeTabelEntity, TCodeTabelModel, Tkey>>();

        }
        #endregion

        #region Utilities
        /// <summary>
        /// 查询带返回结果的外部委托方法
        /// </summary>
        protected Func<IGenericReaderService<TCodeTabelEntity, Tkey>, Task<TCodeTabelModel>> GetAsyncFun;
        /// <summary>
        /// 查询所有带返回结果的外部委托方法
        /// </summary>
        protected Func<IGenericReaderService<TCodeTabelEntity, Tkey>, Task<IEnumerable<TCodeTabelModel>>> GetAllAsyncFun;
        /// <summary>
        /// 获取一条记录后
        /// </summary>
        /// <param name="arg"></param>
        protected virtual void OnAfterGet(TCodeTabelModel arg)
        {

        }
        /// <summary>
        /// 新增前
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        protected virtual AjaxResult OnBeforAdd(CrudEventArgs<TCodeTabelEntity, TCodeTabelModel, Tkey> arg)
        {
            return new AjaxResult
            {
                state = ResultType.success.ToString(),
            };
        }
        /// <summary>
        /// 新增后
        /// </summary>
        /// <param name="arg"></param>
        protected virtual void OnAfterAdd(CrudEventArgs<TCodeTabelEntity, TCodeTabelModel, Tkey> arg)
        {

        }
        /// <summary>
        /// 编辑前
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        protected virtual AjaxResult OnBeforEdit(CrudEventArgs<TCodeTabelEntity, TCodeTabelModel, Tkey> arg)
        {
            return new AjaxResult
            {
                state = ResultType.success.ToString(),
            };
        }
        /// <summary>
        /// 编辑后
        /// </summary>
        /// <param name="arg"></param>
        protected virtual void OnAfterEdit(CrudEventArgs<TCodeTabelEntity, TCodeTabelModel, Tkey> arg)
        {

        }
        /// <summary>
        /// 删除前
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        protected virtual AjaxResult OnBeforDelete(CrudEventArgs<TCodeTabelEntity, TCodeTabelModel, Tkey> arg)
        {
            return new AjaxResult
            {
                state = ResultType.success.ToString(),
            };
        }
        /// <summary>
        /// 删除后
        /// </summary>
        /// <param name="arg"></param>
        protected virtual void OnAfterDeletet(CrudEventArgs<TCodeTabelEntity, TCodeTabelModel, Tkey> arg)
        {

        }

        #endregion

        #region Method  
        /// <summary>
        /// 异步获取模型数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(Tkey id)
        {
            Guard.CheckArgumentNull(CrudDtoMapper, "数据转换器不能为空");
            try
            {
                TCodeTabelModel model;
                if (GetAsyncFun != null)
                {
                    model = await GetAsyncFun(_readerService);
                }
                else
                {
                    var codetableEntity = await _readerService.GetAsync(id);
                    if (codetableEntity == null)
                        return NotFoundResult($"Code with id {id} not found in {nameof(TCodeTabelModel)}.");
                    model = CrudDtoMapper.MapEntityToDto(codetableEntity);
                }
                #region 获取After
                this.OnAfterGet(model);
                #endregion
                return OkResult(model.ToJson());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex, "Error while loading {0} with id '{1}'", nameof(TCodeTabelModel), id);
            }
        }
        /// <summary>
        /// 异步获取所有模型数据
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public async Task<IActionResult> GetAllAsync()
        {
            Guard.CheckArgumentNull(CrudDtoMapper, "数据转换器不能为空");
            try
            {
                IEnumerable<TCodeTabelModel> models;
                if (GetAllAsyncFun != null)
                {
                    models = await GetAllAsyncFun(_readerService);
                }
                else
                {
                    var values = await _readerService.GetAllAsync();
                    models = values.Select(x =>
                    {
                        return CrudDtoMapper.MapEntityToDto(x);
                    });
                }
                return OkResult(models.ToJson());

            }
            catch (Exception ex)
            {
                return InternalServerError(ex, "Error while loading {0}", nameof(TCodeTabelModel));
            }
        }
        /// <summary>
        /// 异步插入表单到数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostAsync(TCodeTabelModel model)
        {
            if (!ModelState.IsValid)
                return BadRequestResult(ModelState);
            //var validationResults = (model as IViewModelValidate<TCodeTabelModel>).Validate(_validator);
            //if (validationResults.Any())
            //    return BadRequestResult(validationResults);

            Guard.CheckArgumentNull(CrudDtoMapper, "数据转换器不能为空");
            try
            {
                #region 新增处理Befor
                var addArgs = new CrudEventArgs<TCodeTabelEntity, TCodeTabelModel, Tkey>(model.Id, model);
                var rtnBefore = this.OnBeforAdd(addArgs);
                if (!rtnBefore.state.ToString().IsCaseSensitiveEqual(ResultType.success.ToString())) return Error(rtnBefore.message);
                #endregion
                #region 新增处理

                var entity = CrudDtoMapper.MapDtoToEntity(model);
                var insertedEntity = await _writerService.InsertAsync(entity);

                #endregion
                #region 新增处理After
                addArgs.Entity = entity;
                this.OnAfterAdd(addArgs);
                #endregion
                return Success("更新成功!", CrudDtoMapper.MapEntityToDto(insertedEntity, model));
            }
            catch (ValidationException validationEx)
            {
                return BadRequestResult(validationEx);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex, "Error while inserting {0}", nameof(TCodeTabelModel));
            }
        }
        /// <summary>
        /// 异步更新表单到数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync(Tkey id, TCodeTabelModel model)
        {
            if (!ModelState.IsValid)
                return BadRequestResult(ModelState);
            //var validationResults = (model as IViewModelValidate<TCodeTabelModel>).Validate(_validator);
            //if (validationResults.Any())
            //    return BadRequestResult(validationResults);
            Guard.CheckArgumentNull(CrudDtoMapper, "数据转换器不能为空");
            try
            {

                #region 编辑处理Befor
                var addArgs = new CrudEventArgs<TCodeTabelEntity, TCodeTabelModel, Tkey>(id, model);
                var rtnBefore = this.OnBeforEdit(addArgs);
                if (!rtnBefore.state.ToString().IsCaseSensitiveEqual(ResultType.success.ToString())) return Error(rtnBefore.message);
                #endregion
                #region 编辑处理

                if (model == null) throw new ValidationException("model not provided");
                if (!id.Equals(model.Id)) throw new ValidationException("id does not match model id");
                var codetableEntity = await _readerService.GetAsync(id);
                if (codetableEntity == null)
                    return NotFoundResult($"Code with id {id} not found in {nameof(TCodeTabelModel)}.");
                var entity = CrudDtoMapper.MapDtoToEntity(model, codetableEntity);
                await _writerService.UpdateAsync(entity);

                #endregion
                #region 编辑处理After
                addArgs.Entity = entity;
                this.OnAfterEdit(addArgs);
                #endregion
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
                return InternalServerError(ex, "Error while updating {0}", nameof(TCodeTabelModel));
            }
        }
        /// <summary>
        /// 异步删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(Tkey id)
        {
            Guard.CheckArgumentNull(CrudDtoMapper, "数据转换器不能为空");

            try
            {
                #region 删除处理Befor
                var addArgs = new CrudEventArgs<TCodeTabelEntity, TCodeTabelModel, Tkey>(id, null, null);
                var rtnBefore = this.OnBeforDelete(addArgs);
                if (!rtnBefore.state.ToString().IsCaseSensitiveEqual(ResultType.success.ToString())) return Error(rtnBefore.message);
                #endregion
                #region 删除处理

                await _writerService.DeleteAsync(id);

                #endregion
                #region 删除处理After
                var entity = new TCodeTabelEntity();
                entity.Id = id;
                addArgs.Entity = entity;
                this.OnAfterDeletet(addArgs);
                #endregion
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
                return InternalServerError(ex, "Error while deleting {0}", nameof(TCodeTabelModel));
            }
        }
        #endregion

    }
}
