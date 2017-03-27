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

namespace SF.Module.Backend.Domain.Role.Rule
{
    /// <summary>
    /// 业务规则处理
    /// </summary>
    public class RoleRules : BaseRules<RoleEntity>, IRoleRules
    {
        private readonly RoleManager<RoleEntity> _roleManager;

        public RoleRules(RoleManager<RoleEntity> backendUnitOfWork)
        {
            _roleManager = backendUnitOfWork;
        }

        public bool DoesRoleExist(long id)
        {
            return _roleManager.Roles.Where(x=>x.Id==id).Any();
           
        }

        public bool IsRoleIdUnique(long id)
        {
            return _roleManager.Roles.Where(x => x.Id == id).Any();
        }
 

        public bool IsRoleNameUnique(string name, long roleId = 0)
        {
            var predicate = PredicateBuilder.New<RoleEntity>();
            predicate.And(d => d.Name == name);
            if (roleId != 0)
            {
                predicate.And(d => d.Id != roleId);
            }
            return _roleManager.Roles.AsNoTracking().Any(predicate);

        }
 
    }
}
