using SF.Core.Abstraction.Domain;
using SF.Entitys;
using SF.Module.Backend.Data.Entitys;
using SF.Module.Backend.Domain.DataItem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.Backend.Domain.User.Rule
{
    public interface IUserRules : IRules<UserEntity>
    {
        bool DoesUserExist(long id);
        bool IsUserIdUnique(long id);
        bool IsUserNameUnique(string name, long userId = 0);
        bool IsUserEmailUnique(string email, long userId = 0);


    }
}
