using AutoMapper;
using SF.Core.Entitys;
using SF.Module.Backend.Domain.DataItemDetail.ViewModel;
using SF.Web.Common.Base.DataContractMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.Backend.Domain.DataItemDetail.Mapping
{

    /// <summary>
    /// 字典映射
    /// </summary>
    public class DataItemDetailDtoMapper : CrudDtoMapper<DataItemDetailEntity, DataItemDetailViewModel>
    {


        /// <summary>
        /// DTO转换领域的实体映射
        /// </summary>
        /// <param name="dto">DTO实体映射</param>
        /// <param name="entity">实体映射DTO</param>
        /// <returns>The entity</returns>
        protected override DataItemDetailEntity OnMapDtoToEntity(DataItemDetailViewModel dto, DataItemDetailEntity entity)
        {
            Mapper.Map<DataItemDetailViewModel, DataItemDetailEntity>(dto, entity);
            return entity;
        }
        /// <summary>
        /// 领域的实体转换DTO映射
        /// </summary>
        /// <param name="entity">实体映射</param>
        /// <param name="dto">DTO映射实体</param>
        /// <returns>The dto</returns>
        protected override DataItemDetailViewModel OnMapEntityToDto(DataItemDetailEntity entity, DataItemDetailViewModel dto)
        {
            Mapper.Map<DataItemDetailEntity, DataItemDetailViewModel>(entity, dto);
            return dto;
        }
        /// <summary>
        /// 领域的实体转换List<dto>映射
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected override IEnumerable<DataItemDetailViewModel> OnMapEntityToDtos(IEnumerable<DataItemDetailEntity> entitys)
        {
            var dtos = Mapper.Map<IEnumerable<DataItemDetailEntity>, IEnumerable<DataItemDetailViewModel>>(entitys);
            return dtos;
        }
    }
}
