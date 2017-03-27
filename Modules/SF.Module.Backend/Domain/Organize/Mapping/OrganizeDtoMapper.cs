using AutoMapper;
using SF.Entitys;
using SF.Module.Backend.Data.Entitys;
using SF.Module.Backend.Domain.Organize.ViewModel;
using SF.Web.Base.DataContractMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.Backend.Domain.Organize.Mapping
{
    /// <summary>
    /// 区域映射
    /// </summary>
    public class OrganizeDtoMapper : BaseCrudDtoMapper<OrganizeEntity, OrganizeViewModel, long>
    {

    }
}
