
using SF.Entitys.Abstraction;
using SF.Web.Models;
using System.Collections.Generic;

namespace SF.Web.Base.DataContractMapper
{
    /// <summary>
    /// 从模型构建表单的构建器的接口
    /// </summary>
    public interface ICrudDtoMapper<TEntity, TDto, Tkey>
        where TEntity : IEntityWithTypedId<Tkey>
        where TDto : EntityModelBase<Tkey>
    {

        /// <summary>
        /// Maps the given entity to a dto
        /// </summary>
        /// <param name="entity">The entity to map</param>
        /// <returns>The mapped dto</returns>
        TDto MapEntityToDto(TEntity entity);

        /// <summary>
        /// Maps the given entity to the already existing dto
        /// </summary>
        /// <param name="entity">The entity to map</param>
        /// <param name="dto">The already existing dto</param>
        /// <returns>The mapped dto</returns>
        TDto MapEntityToDto(TEntity entity, TDto dto);

        /// <summary>
        /// Maps the given dto to an entity
        /// </summary>
        /// <param name="dto">The dto to map</param>
        /// <returns>The mapped entity</returns>
        TEntity MapDtoToEntity(TDto dto);

        /// <summary>
        /// maps the given dto to the existing entity
        /// </summary>
        /// <param name="dto">The dto to map</param>
        /// <param name="entity">The already existing entity</param>
        /// <returns>The mapped dto</returns>
        TEntity MapDtoToEntity(TDto dto, TEntity entity);

        /// <summary>
        /// Maps the given entitys to the already existing dtos
        /// </summary>
        /// <param name="entitys">The entitys to map</param>
        /// <returns>The mapped dtos</returns>
        IEnumerable<TDto> MapEntityToDtos(IEnumerable<TEntity> entitys);
    }
}
