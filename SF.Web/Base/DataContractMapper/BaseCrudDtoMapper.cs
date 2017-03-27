
using SF.Web.Models;
using System.Collections.Generic;
using AutoMapper;
using SF.Entitys.Abstraction;

namespace SF.Web.Base.DataContractMapper
{
    /// <summary>
    /// 数据映射自动映射基类的ID、创建、修改日期
    /// </summary>
    public class BaseCrudDtoMapper<TEntity, TDto, Tkey> : CrudDtoMapper<TEntity, TDto, Tkey>
       where TEntity : IEntityWithTypedId<Tkey>, new()
       where TDto : EntityModelBase<Tkey>, new()
    {
        /// <summary>
        /// DTO转换领域的实体映射
        /// </summary>
        /// <param name="dto">DTO实体映射</param>
        /// <param name="entity">实体映射DTO</param>
        /// <returns>The entity</returns>
        protected override TEntity OnMapDtoToEntity(TDto dto, TEntity entity)
        {
            Mapper.Map<TDto, TEntity>(dto, entity);
            return entity;
        }
        /// <summary>
        /// 领域的实体转换DTO映射
        /// </summary>
        /// <param name="entity">实体映射</param>
        /// <param name="dto">DTO映射实体</param>
        /// <returns>The dto</returns>
        protected override TDto OnMapEntityToDto(TEntity entity, TDto dto)
        {
            Mapper.Map<TEntity, TDto>(entity, dto);
            return dto;
        }
        /// <summary>
        /// 领域的实体转换List<dto>映射
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected override IEnumerable<TDto> OnMapEntityToDtos(IEnumerable<TEntity> entitys)
        {
            var dtos = Mapper.Map<IEnumerable<TEntity>, IEnumerable<TDto>>(entitys);
            return dtos;
        }
    }
}
