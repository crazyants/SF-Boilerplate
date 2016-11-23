
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using SimpleFramework.Core.Abstraction.Entitys;
using SimpleFramework.Core.Web.Base.Datatypes;
using SimpleFramework.Core.Web.Base.Business;
using SimpleFramework.Core.Errors.Exceptions;
using SimpleFramework.Core.Web.Base.DataContractMapper;
using SimpleFramework.Core.Abstraction.Data;
using SimpleFramework.Core.EFCore.UoW;
using SimpleFramework.Core.Abstraction.UoW;
using SimpleFramework.Core.Data.Repository;
using Microsoft.EntityFrameworkCore;
using SimpleFramework.Core.EFCore.Repository;

namespace SimpleFramework.Core.Web.Base.Controllers
{
    /// <summary>
    /// 控制器基类，实现增删改查基础功能
    /// </summary>
    /// <typeparam name="TCodeTabelEntity"></typeparam>
    /// <typeparam name="TCodeTabelModel"></typeparam>
    public class CrudControllerBase<TCodeTabelEntity, TCodeTabelModel> : ControllerBase
        where TCodeTabelEntity : BaseEntity
        where TCodeTabelModel : EntityModelBase
    {
        private readonly ICodetableReader<TCodeTabelEntity,long> _reader;
        private readonly ICodetableWriter<TCodeTabelEntity, long> _writer;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IEFCoreQueryableRepository<TCodeTabelEntity, long> _repository;

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
        public CrudControllerBase(DbContext dbContext, IServiceCollection service, ILogger<Controller> logger) : base(service, logger)
        {
            _reader = service.BuildServiceProvider().GetService<ICodetableReader<TCodeTabelEntity, long>>();
            _writer = service.BuildServiceProvider().GetService<ICodetableWriter<TCodeTabelEntity, long>>();
            _repository = new EFCoreBaseRepository<TCodeTabelEntity>(dbContext);
        }
        /// <summary>
        /// 初始化构造
        /// 用于不同的上下文示例、不同的工作单元
        /// </summary>
        /// <param name="dbContext">上下文实例</param>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="service">服务集合</param>
        /// <param name="logger">日志</param>
        public CrudControllerBase(DbContext dbContext, IUnitOfWork unitOfWork, IServiceCollection service, ILogger<Controller> logger) : base(service, logger)
        {
            _repository = new EFCoreBaseRepository<TCodeTabelEntity,long>(dbContext);
            _reader = new CodetableReader<TCodeTabelEntity, long>(logger, _repository);
            _writer = new CodeTableWriter<TCodeTabelEntity,long>(logger, _repository, unitOfWork);
        }
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
            try
            {

                var values = await _reader.GetAllAsync();
                var mappedValues = values.Select(x =>
                  {
                      return CrudDtoMapper.MapEntityToDto(x);
                  });
                // var mappedValues = Mapper.Map<IEnumerable<TCodeTabelModel>>(values);
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

            try
            {
                var entity = CrudDtoMapper.MapDtoToEntity(model);
                //    var entity = Mapper.Map<TCodeTabelEntity>(model);
                var insertedEntity = await _writer.InsertAsync(entity);

                return Created($"{Request.Path.Value}/{insertedEntity.Id}", CrudDtoMapper.MapEntityToDto(insertedEntity));
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
        public async Task<IActionResult> UpdateAsync(int id, [FromBody]TCodeTabelModel model)
        {
            if (!ModelState.IsValid)
                return BadRequestResult(ModelState);

            try
            {
                if (model == null) throw new ValidationException("model not provided");
                if (id != model.Id) throw new ValidationException("id does not match model id");
                var entity = CrudDtoMapper.MapDtoToEntity(model);
                // var entity = Mapper.Map<TCodeTabelEntity>(model);
                await _writer.UpdateAsync(entity);
                return OkResult();
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
            try
            {
                await _writer.DeleteAsync(id);
                return OkResult();
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


    }
}
