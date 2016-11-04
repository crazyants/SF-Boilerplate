
using SimpleFramework.Core.Abstraction.Entitys;
using SimpleFramework.Core.Web.Base.Datatypes;

namespace SimpleFramework.Core.Web.Base.DataContractMapper
{
    /// <summary>
    /// 数据映射自动映射基类的ID、创建、修改日期
    /// </summary>
    public abstract class CrudDtoMapper<TEntity, TDto> : ICrudDtoMapper<TEntity, TDto>
    where TEntity : IEntityWithTypedId<long>, new()
    where TDto : EntityModelBase, new()
    {
        /// <summary>
        /// DTO转成领域的实体
        /// </summary>
        /// <param name="dto">DTO数据源</param>
        /// <returns></returns>
        public TEntity MapDtoToEntity(TDto dto)
        {
            var entity = OnMapDtoToEntity(dto, new TEntity());

            // Making sure the derived class doesn't change these values
            entity.Id = dto.Id;

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

            // Making sure the derived class doesn't change these values
            dto.Id = entity.Id;

            return OnMapEntityToDto(entity, dto);
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

            // Making sure the derived class doesn't change these values
            dto.Id = entity.Id;


            return OnMapEntityToDto(entity, dto);
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

            // Making sure the derived class doesn't change these values
            entity.Id = dto.Id;


            return entity;
        }

        /// <summary>
        /// 领域的实体转换DTO映射
        /// </summary>
        /// <param name="entity">实体映射</param>
        /// <param name="dto">DTO映射实体</param>
        /// <returns>The dto</returns>
        protected abstract TDto OnMapEntityToDto(TEntity entity, TDto dto);

        /// <summary>
        /// DTO转换领域的实体映射
        /// </summary>
        /// <param name="dto">DTO实体映射</param>
        /// <param name="entity">实体映射DTO</param>
        /// <returns>The entity</returns>
        protected abstract TEntity OnMapDtoToEntity(TDto dto, TEntity entity);

    }
}
