using AutoMapper;
using SF.Entitys;
using SF.Module.Backend.Data.Entitys;
using SF.Module.Backend.Domain.DataItemDetail.ViewModel;
using SF.Web.Base.DataContractMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.Backend.Domain.DataItemDetail.Mapping
{

    /// <summary>
    /// 字典映射
    /// 一般默认集成基础类BaseCrudDtoMapper，如特殊要求请继承CrudDtoMapper
    /// </summary>
    public class DataItemDetailDtoMapper : BaseCrudDtoMapper<DataItemDetailEntity, DataItemDetailViewModel, long>
    {

    }
}
