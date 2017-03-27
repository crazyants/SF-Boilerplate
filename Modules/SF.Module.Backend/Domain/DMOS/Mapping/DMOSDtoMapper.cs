using AutoMapper;
using SF.Entitys;
using SF.Module.Backend.Data.Entitys;
using SF.Module.Backend.Domain.DMOS.ViewModel;
using SF.Web.Base.DataContractMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.Backend.Domain.DMOS.Mapping
{
    /// <summary>
    /// 岗位职位工作组映射
    /// </summary>
    public class DMOSDtoMapper : BaseCrudDtoMapper<DMOSEntity, DMOSViewModel, long>
    {

    }
}
