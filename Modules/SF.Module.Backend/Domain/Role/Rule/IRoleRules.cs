using SF.Core.Abstraction.Domain;
using SF.Entitys;
using SF.Module.Backend.Data.Entitys;
using SF.Module.Backend.Domain.DataItem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.Backend.Domain.Role.Rule
{
    public interface IRoleRules : IRules<RoleEntity>
    {
        bool DoesRoleExist(long id);
        bool IsRoleIdUnique(long id);
        bool IsRoleNameUnique(string name, long roleId = 0);



    }
}
