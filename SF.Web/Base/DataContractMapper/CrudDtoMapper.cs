
using SF.Entitys.Abstraction;
using SF.Web.Models;
using System.Collections.Generic;

namespace SF.Web.Base.DataContractMapper
{
    /// <summary>
    /// 数据映射自动映射基类的ID、创建、修改日期
    /// </summary>
    public abstract class CrudDtoMapper<TEntity, TDto, Tkey> : ICrudDtoMapper<TEntity, TDto, Tkey>
    where TEntity : IEntityWithTypedId<Tkey>, new()
    where TDto : EntityModelBase<Tkey>, new()
    {
        /// <summary>
        /// DTO转成领域的实体
        /// </summary>
        /// <param name="dto">DTO数据源</param>
        /// <returns></returns>
        public TEntity MapDtoToEntity(TDto dto)
        {
            var entity = OnMapDtoToEntity(dto, new TEntity());

            return entity;
        }

        /// <summary>
        /// 领域的实体转成DTO
        /// </summary>
        /// <param name="entity">领域的实体</param>
        /// <returns></returns>
        public TDto MapEntityToDto(TEntity entity)
        {
            var dto = OnMapEntityToDto(entity, new TDto());

            return dto;
        }

        /// <summary>
        /// 领域的实体转成DTO
        /// </summary>
        /// <param name="entity">领域的实体</param>
        /// <param name="existingDto">已实例化的DTO</param>
        /// <returns></returns>
        public TDto MapEntityToDto(TEntity entity, TDto existingDto)
        {
            var dto = OnMapEntityToDto(entity, existingDto);

            return dto;
        }

        /// <summary>
        /// DTO转成领域的实体
        /// </summary>
        /// <param name="dto">DTO数据源</param>
        /// <param name="existingEntity">已实例化的领域的实体</param>
        /// <returns></returns>
        public TEntity MapDtoToEntity(TDto dto, TEntity existingEntity)
        {
            var entity = OnMapDtoToEntity(dto, existingEntity);

            return entity;
        }

        /// <summary>
        /// 领域的实体转成DTO
        /// </summary>
        /// <param name="entity">领域的实体</param>
        /// <param name="existingDto">已实例化的DTO</param>
        /// <returns></returns>
        public IEnumerable<TDto> MapEntityToDtos(IEnumerable<TEntity> entitys)
        {
            var dtos = OnMapEntityToDtos(entitys);

            return dtos;
        }

        /// <summary>
        /// 领域的实体转换DTO映射
        /// </summary>
        /// <param name="entity">实体映射</param>
        /// <param name="dto">DTO映射实体</param>
        /// <returns>The dto</returns>
        protected abstract TDto OnMapEntityToDto(TEntity entity, TDto dto);
        /// <summary>
        /// 领域的实体转换DTO映射
        /// </summary>
        /// <param name="entity">实体映射</param>
        /// <param name="dto">DTO映射实体</param>
        /// <returns>The dto</returns>
        protected abstract IEnumerable<TDto> OnMapEntityToDtos(IEnumerable<TEntity> entitys);
        /// <summary>
        /// DTO转换领域的实体映射
        /// </summary>
        /// <param name="dto">DTO实体映射</param>
        /// <param name="entity">实体映射DTO</param>
        /// <returns>The entity</returns>
        protected abstract TEntity OnMapDtoToEntity(TDto dto, TEntity entity);


    }
}
