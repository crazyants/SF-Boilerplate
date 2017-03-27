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

namespace SF.Module.Backend.Domain.Department.Rule
{
    /// <summary>
    /// 业务规则处理
    /// </summary>
    public class DepartmentRules : BaseRules<DepartmentEntity>, IDepartmentRules
    {
        private readonly IBackendUnitOfWork _backendUnitOfWork;

        public DepartmentRules(IBackendUnitOfWork backendUnitOfWork)
        {
            _backendUnitOfWork = backendUnitOfWork;
        }

        public bool DoesDepartmentExist(long id)
        {
            var app = _backendUnitOfWork.Department.GetById(id);
            return app != null;
        }

        public bool IsDepartmentIdUnique(long id)
        {
            return _backendUnitOfWork.Department.Exists(id);

        }

        public bool IsDepartmentCodeUnique(string code, long dataItemId = 0)
        {
            var predicate = PredicateBuilder.New<DepartmentEntity>();
            predicate.And(d => d.EnCode == code);
            if (dataItemId != 0)
            {
                predicate.And(d => d.Id != dataItemId);
            }
            return _backendUnitOfWork.Department.Query().AsNoTracking().Any(predicate);

        }

        public bool IsDepartmentNameUnique(string fullName, long organizeId = 0)
        {
            var predicate = PredicateBuilder.New<DepartmentEntity>();
            predicate.And(d => d.FullName == fullName);
            if (organizeId != 0)
            {
                predicate.And(d => d.Id != organizeId);
            }
            return _backendUnitOfWork.Department.Query().AsNoTracking().Any(predicate);

        }

        public bool IsDepartmentShortNameUnique(string shortName, long organizeId = 0)
        {
            var predicate = PredicateBuilder.New<DepartmentEntity>();
            predicate.And(d => d.ShortName == shortName);
            if (organizeId != 0)
            {
                predicate.And(d => d.Id != organizeId);
            }
            return _backendUnitOfWork.Department.Query().AsNoTracking().Any(predicate);

        }
    }
}
