using LinqKit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SF.Core.Abstraction.Domain;
using SF.Data;
using SF.Entitys;
using SF.Module.Backend.Data.Entitys;
using SF.Module.Backend.Data.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SF.Module.Backend.Domain.User.Rule
{
    /// <summary>
    /// 业务规则处理
    /// </summary>
    public class UserRules : BaseRules<UserEntity>, IUserRules
    {
        private readonly UserManager<UserEntity> _UserManager;
        public UserRules(UserManager<UserEntity> backendUnitOfWork)
        {
            _UserManager = backendUnitOfWork;
        }
        public bool DoesUserExist(long id)
        {
            return _UserManager.Users.Where(x=>x.Id==id).Any();
           
        }
        public bool IsUserIdUnique(long id)
        {
            return _UserManager.Users.Where(x => x.Id == id).Any();
        }
        public bool IsUserNameUnique(string name, long userId = 0)
        {
            var predicate = PredicateBuilder.New<UserEntity>();
            predicate.And(d => d.UserName == name);
            if (userId != 0)
            {
                predicate.And(d => d.Id != userId);
            }
            return _UserManager.Users.AsNoTracking().Any(predicate);

        }
        public bool IsUserEmailUnique(string email, long userId = 0)
        {
            var predicate = PredicateBuilder.New<UserEntity>();
            predicate.And(d => d.Email == email);
            if (userId != 0)
            {
                predicate.And(d => d.Id != userId);
            }
            return _UserManager.Users.AsNoTracking().Any(predicate);

        }
    }
}
