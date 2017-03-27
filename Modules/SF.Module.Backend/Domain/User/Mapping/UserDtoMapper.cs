using AutoMapper;
using SF.Entitys;
using SF.Module.Backend.Data.Entitys;
using SF.Module.Backend.Domain.User.ViewModel;
using SF.Web.Base.DataContractMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.Backend.Domain.User.Mapping
{
    /// <summary>
    /// 映射
    /// </summary>
    public class UserDtoMapper : BaseCrudDtoMapper<UserEntity, UserViewModel, long>
    {

    }
}
