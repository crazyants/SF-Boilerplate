
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using SF.Core.Abstraction.Entitys;
using SF.Core.Web.Base.Datatypes;
using SF.Core.Web.Base.Business;
using SF.Core.Errors.Exceptions;
using SF.Core.Web.Base.DataContractMapper;
using SF.Core.Abstraction.Data;
using SF.Core.EFCore.UoW;
using SF.Core.Abstraction.UoW;
using SF.Core.Data.Repository;
using Microsoft.EntityFrameworkCore;
using SF.Core.EFCore.Repository;
using SF.Core.Common;
using SF.Core.Web.Base.Args;
using SF.Core.Web.Models;
using SF.Core.Extensions;

namespace SF.Core.Web.Base.Controllers
{
    /// <summary>
    /// 控制器基类，实现增删改查基础功能
    /// </summary>
    /// <typeparam name="TCodeTabelEntity"></typeparam>
    /// <typeparam name="TCodeTabelModel"></typeparam>
    public abstract class CrudControllerBase<TCodeTabelEntity, TCodeTabelModel> : ControllerBase
        where TCodeTabelEntity : BaseEntity
        where TCodeTabelModel : EntityModelBase
    {
        #region Fields

        private readonly ICodetableReader<TCodeTabelEntity, long> _reader;
        private readonly ICodetableWriter<TCodeTabelEntity, long> _writer;
        protected readonly IEFCoreQueryableRepository<TCodeTabelEntity, long> _repository;

        #endregion

        #region Constructors
        /// <summary>
        /// 数据转换器
        /// </summary>
        /// <returns></returns>
        public ICrudDtoMapper<TCodeTabelEntity, TCodeTabelModel> CrudDtoMapper { get; set; }
        /// <summary>
        /// 初始化构造
        /// 使用注入的同一个上下文
        /// </summary>
        /// <param name="service">服务集合</param>
        /// <param name="logger">日志</param>
        public CrudControllerBase(IServiceCollection service, ILogger<Controller> logger) : base(service, logger)
        {
            _reader = service.BuildServiceProvider().GetService<ICodetableReader<TCodeTabelEntity, long>>();
            _writer = service.BuildServiceProvider().GetService<ICodetableWriter<TCodeTabelEntity, long>>();
            _repository = service.BuildServiceProvider().GetService<IEFCoreQueryableRepository<TCodeTabelEntity, long>>();
        }
        /// <summary>
        /// 初始化构造
        /// 用于不同的个上下文，使用注入的工作单元
        /// </summary>
        /// <param name="dbContext">上下文实例</param>
        /// <param name="service">服务集合</param>
        /// <param name="logger">日志</param>
        public CrudControllerBase(IEFCoreUnitOfWork unitOfWork, IServiceCollection service, ILogger<Controller> logger) : base(service, logger)
        {
            _repository = new EFCoreBaseRepository<TCodeTabelEntity>(unitOfWork.Context);
            _reader = new CodetableReader<TCodeTabelEntity, long>(logger, _repository);
            _writer = new CodeTableWriter<TCodeTabelEntity, long>(logger, _repository, unitOfWork);

        }
        #endregion

        #region Utilities
        /// <summary>
        /// 新增前
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        protected virtual AjaxResult OnBeforAdd(CrudEventArgs<TCodeTabelEntity, TCodeTabelModel> arg)
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
        protected virtual void OnAfterAdd(CrudEventArgs<TCodeTabelEntity, TCodeTabelModel> arg)
        {

        }
        /// <summary>
        /// 编辑前
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        protected virtual AjaxResult OnBeforEdit(CrudEventArgs<TCodeTabelEntity, TCodeTabelModel> arg)
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
        protected virtual void OnAfterEdit(CrudEventArgs<TCodeTabelEntity, TCodeTabelModel> arg)
        {

        }
        /// <summary>
        /// 删除前
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        protected virtual AjaxResult OnBeforDelete(CrudEventArgs<TCodeTabelEntity, TCodeTabelModel> arg)
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
        protected virtual void OnAfterDeletet(CrudEventArgs<TCodeTabelEntity, TCodeTabelModel> arg)
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
        public async Task<IActionResult> GetAsync(int id)
        {
            Guard.CheckArgumentNull(CrudDtoMapper, "数据转换器不能为空");
            try
            {
                var codetable = await _reader.GetAsync(id);
                if (codetable == null)
                    return NotFoundResult($"Code with id {id} not found in {typeof(TCodeTabelModel).Name}.");
                var model = CrudDtoMapper.MapEntityToDto(codetable);
                return OkResult(model);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex, "Error while loading {0} with id '{0}'", typeof(TCodeTabelModel).Name, id);
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
                var values = await _reader.GetAllAsync();
                var mappedValues = values.Select(x =>
                  {
                      return CrudDtoMapper.MapEntityToDto(x);
                  });
                return OkResult(mappedValues);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex, "Error while loading {0}", typeof(TCodeTabelModel).Name);
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
            Guard.CheckArgumentNull(CrudDtoMapper, "数据转换器不能为空");
            try
            {
                #region 新增处理Befor
                var addArgs = new CrudEventArgs<TCodeTabelEntity, TCodeTabelModel>(model);
                var rtnBefore = this.OnBeforAdd(addArgs);
                if (!rtnBefore.state.ToString().IsCaseSensitiveEqual(ResultType.success.ToString())) return Error(rtnBefore.message);
                #endregion
                #region 新增处理

                var entity = CrudDtoMapper.MapDtoToEntity(model);
                var insertedEntity = await _writer.InsertAsync(entity);

                #endregion
                #region 新增处理After
                addArgs.Entity = entity;
                this.OnAfterAdd(addArgs);
                #endregion
                return Success("", CrudDtoMapper.MapEntityToDto(insertedEntity, model));
            }
            catch (ValidationException validationEx)
            {
                return BadRequestResult(validationEx);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex, "Error while inserting {0}", typeof(TCodeTabelModel).Name);
            }
        }
        /// <summary>
        /// 异步更新表单到数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, TCodeTabelModel model)
        {
            if (!ModelState.IsValid)
                return BadRequestResult(ModelState);
            Guard.CheckArgumentNull(CrudDtoMapper, "数据转换器不能为空");
            try
            {

                #region 编辑处理Befor
                var addArgs = new CrudEventArgs<TCodeTabelEntity, TCodeTabelModel>(model);
                var rtnBefore = this.OnBeforEdit(addArgs);
                if (!rtnBefore.state.ToString().IsCaseSensitiveEqual(ResultType.success.ToString())) return Error(rtnBefore.message);
                #endregion
                #region 编辑处理

                if (model == null) throw new ValidationException("model not provided");
                if (id != model.Id) throw new ValidationException("id does not match model id");
                var entity = CrudDtoMapper.MapDtoToEntity(model);
                await _writer.UpdateAsync(entity);

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
                return InternalServerError(ex, "Error while updating {0}", typeof(TCodeTabelModel).Name);
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
            Guard.CheckArgumentNull(CrudDtoMapper, "数据转换器不能为空");

            try
            {
                #region 删除处理Befor
                var addArgs = new CrudEventArgs<TCodeTabelEntity, TCodeTabelModel>(null, null, id);
                var rtnBefore = this.OnBeforDelete(addArgs);
                if (!rtnBefore.state.ToString().IsCaseSensitiveEqual(ResultType.success.ToString())) return Error(rtnBefore.message);
                #endregion
                #region 删除处理

                await _writer.DeleteAsync(id);

                #endregion
                #region 删除处理After
                this.OnAfterEdit(addArgs);
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
                return InternalServerError(ex, "Error while deleting {0}", typeof(TCodeTabelModel).Name);
            }
        }
        #endregion

    }
}
