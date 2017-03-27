using AutoMapper;
using SF.Entitys;
using SF.Module.Backend.Data.Entitys;
using SF.Module.Backend.Domain.Module.ViewModel;
using SF.Web.Base.DataContractMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.Backend.Domain.Module.Mapping
{
    /// <summary>
    /// 区域映射
    /// </summary>
    public class ModuleDtoMapper : BaseCrudDtoMapper<ModuleEntity, ModuleViewModel, long>
    {

    }
}
