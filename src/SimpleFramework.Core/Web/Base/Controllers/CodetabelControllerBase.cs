
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleFramework.Core.Abstraction.Entitys;
using SimpleFramework.Core.Web.Base.Datatypes;
using SimpleFramework.Core.Web.Base.Business;
using SimpleFramework.Core.Errors.Exceptions;
using SimpleFramework.Core.Web.Base.DataContractMapper;

namespace SimpleFramework.Core.Web.Base.Controllers
{
    public class CodetableControllerBase<TCodeTabelEntity, TCodeTabelModel> : ControllerBase
        where TCodeTabelEntity : EntityBase
        where TCodeTabelModel : EntityModelBase
    {
        private readonly ICodetableReader<TCodeTabelEntity> _reader;
        private ICodetableWriter<TCodeTabelEntity> _writer;

        /// <summary>
        /// 数据转换
        /// </summary>
        /// <returns></returns>
        public ICrudDtoMapper<TCodeTabelEntity, TCodeTabelModel> CrudDtoMapper { get; set; }

        public CodetableControllerBase(IServiceCollection service, ILogger<Controller> logger) : base(logger)
        {
            _reader = service.BuildServiceProvider().GetService<ICodetableReader<TCodeTabelEntity>>();
            _writer = service.BuildServiceProvider().GetService<ICodetableWriter<TCodeTabelEntity>>();
        }

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

        [HttpPost()]
        public async Task<IActionResult> InsertAsync([FromBody]TCodeTabelModel model)
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
