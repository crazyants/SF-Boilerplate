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

namespace SF.Module.Backend.Domain.Organize.Rule
{
    /// <summary>
    /// 业务规则处理
    /// </summary>
    public class OrganizeRules : BaseRules<OrganizeEntity>, IOrganizeRules
    {
        private readonly IBackendUnitOfWork _backendUnitOfWork;

        public OrganizeRules(IBackendUnitOfWork backendUnitOfWork)
        {
            _backendUnitOfWork = backendUnitOfWork;
        }

        public bool DoesOrganizeExist(long id)
        {
            var app = _backendUnitOfWork.Organize.GetById(id);
            return app != null;
        }

        public bool IsOrganizeIdUnique(long id)
        {
            return _backendUnitOfWork.Organize.Exists(id);

        }

        public bool IsOrganizeCodeUnique(string code, long dataItemId = 0)
        {
            var predicate = PredicateBuilder.New<OrganizeEntity>();
            predicate.And(d => d.EnCode == code);
            if (dataItemId != 0)
            {
                predicate.And(d => d.Id != dataItemId);
            }
            return _backendUnitOfWork.Organize.Query().AsNoTracking().Any(predicate);

        }

        public bool IsOrganizeNameUnique(string fullName, long organizeId = 0)
        {
            var predicate = PredicateBuilder.New<OrganizeEntity>();
            predicate.And(d => d.FullName == fullName);
            if (organizeId != 0)
            {
                predicate.And(d => d.Id != organizeId);
            }
            return _backendUnitOfWork.Organize.Query().AsNoTracking().Any(predicate);

        }

        public bool IsOrganizeShortNameUnique(string shortName, long organizeId = 0)
        {
            var predicate = PredicateBuilder.New<OrganizeEntity>();
            predicate.And(d => d.ShortName == shortName);
            if (organizeId != 0)
            {
                predicate.And(d => d.Id != organizeId);
            }
            return _backendUnitOfWork.Organize.Query().AsNoTracking().Any(predicate);

        }
    }
}
