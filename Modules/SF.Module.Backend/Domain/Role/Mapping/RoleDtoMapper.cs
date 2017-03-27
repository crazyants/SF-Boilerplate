using AutoMapper;
using SF.Entitys;
using SF.Module.Backend.Data.Entitys;
using SF.Module.Backend.Domain.Role.ViewModel;
using SF.Web.Base.DataContractMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.Backend.Domain.Role.Mapping
{
    /// <summary>
    /// 映射
    /// </summary>
    public class RoleDtoMapper : BaseCrudDtoMapper<RoleEntity, RoleViewModel, long>
    {

    }
}
