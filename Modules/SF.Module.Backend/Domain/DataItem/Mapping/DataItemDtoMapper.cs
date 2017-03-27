using AutoMapper;
using SF.Entitys;
using SF.Module.Backend.Data.Entitys;
using SF.Module.Backend.Domain.DataItem.ViewModel;
using SF.Web.Base.DataContractMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.Backend.Domain.DataItem.Mapping
{
    /// <summary>
    /// 字典映射
    /// </summary>
    public class DataItemDtoMapper : CrudDtoMapper<DataItemEntity, DataItemViewModel,long>
    {
        /// <summary>
        /// DTO转换领域的实体映射
        /// </summary>
        /// <param name="dto">DTO实体映射</param>
        /// <param name="entity">实体映射DTO</param>
        /// <returns>The entity</returns>
        protected override DataItemEntity OnMapDtoToEntity(DataItemViewModel dto, DataItemEntity entity)
        {
            Mapper.Map<DataItemViewModel, DataItemEntity>(dto, entity);
            return entity;
        }
        /// <summary>
        /// 领域的实体转换DTO映射
        /// </summary>
        /// <param name="entity">实体映射</param>
        /// <param name="dto">DTO映射实体</param>
        /// <returns>The dto</returns>
        protected override DataItemViewModel OnMapEntityToDto(DataItemEntity entity, DataItemViewModel dto)
        {
            Mapper.Map<DataItemEntity, DataItemViewModel>(entity, dto);
            return dto;
        }
        /// <summary>
        /// 领域的实体转换List<dto>映射
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected override IEnumerable<DataItemViewModel> OnMapEntityToDtos(IEnumerable<DataItemEntity> entitys)
        {
            var dtos = Mapper.Map<IEnumerable<DataItemEntity>, IEnumerable<DataItemViewModel>>(entitys);
            return dtos;
        }
    }
}
