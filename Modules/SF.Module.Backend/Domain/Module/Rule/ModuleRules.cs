using LinqKit;
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

namespace SF.Module.Backend.Domain.Module.Rule
{
    /// <summary>
    /// 业务规则处理
    /// </summary>
    public class ModuleRules : BaseRules<ModuleEntity>, IModuleRules
    {
        private readonly IBackendUnitOfWork _backendUnitOfWork;

        public ModuleRules(IBackendUnitOfWork backendUnitOfWork)
        {
            _backendUnitOfWork = backendUnitOfWork;
        }

        public bool DoesModuleExistChildren(long id)
        {
            return _backendUnitOfWork.Module.QueryFilter(e => e.ParentId == id).Any();

        }

        public bool IsModuleCodeUnique(string code, long moduleId = 0)
        {
            var predicate = PredicateBuilder.New<ModuleEntity>();
            predicate.And(d => d.EnCode == code);
            if (moduleId != 0)
            {
                predicate.And(d => d.Id != moduleId);
            }
            return _backendUnitOfWork.Module.Query().AsNoTracking().Any(predicate);

        }

        public bool IsModuleNameUnique(string name, long moduleId = 0)
        {
            var predicate = PredicateBuilder.New<ModuleEntity>();
            predicate.And(d => d.FullName == name);
            if (moduleId != 0)
            {
                predicate.And(d => d.Id != moduleId);
            }
            return _backendUnitOfWork.Module.Query().AsNoTracking().Any(predicate);

        }
    }
}
