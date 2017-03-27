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

namespace SF.Module.Backend.Domain.DMOS.Rule
{
    /// <summary>
    /// 业务规则处理
    /// </summary>
    public class DMOSRules : BaseRules<DMOSEntity>, IDMOSRules
    {
        private readonly IBackendUnitOfWork _backendUnitOfWork;

        public DMOSRules(IBackendUnitOfWork backendUnitOfWork)
        {
            _backendUnitOfWork = backendUnitOfWork;
        }

        public bool DoesDMOSExist(long id)
        {
            var app = _backendUnitOfWork.DMOS.GetById(id);
            return app != null;
        }

        public bool IsDMOSIdUnique(long id)
        {
            return _backendUnitOfWork.DMOS.Exists(id);

        }

        public bool IsDMOSCodeUnique(string code, long dataItemId = 0)
        {
            var predicate = PredicateBuilder.New<DMOSEntity>();
            predicate.And(d => d.EnCode == code);
            if (dataItemId != 0)
            {
                predicate.And(d => d.Id != dataItemId);
            }
            return _backendUnitOfWork.DMOS.Query().AsNoTracking().Any(predicate);

        }

        public bool IsDMOSNameUnique(string fullName, long organizeId = 0)
        {
            var predicate = PredicateBuilder.New<DMOSEntity>();
            predicate.And(d => d.FullName == fullName);
            if (organizeId != 0)
            {
                predicate.And(d => d.Id != organizeId);
            }
            return _backendUnitOfWork.DMOS.Query().AsNoTracking().Any(predicate);

        }
       
    }
}
