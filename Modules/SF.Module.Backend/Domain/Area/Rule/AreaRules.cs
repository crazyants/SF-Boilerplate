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

namespace SF.Module.Backend.Domain.Area.Rule
{
    /// <summary>
    /// 业务规则处理
    /// </summary>
    public class AreaRules : BaseRules<AreaEntity>, IAreaRules
    {
        private readonly IBackendUnitOfWork _backendUnitOfWork;

        public AreaRules(IBackendUnitOfWork backendUnitOfWork)
        {
            _backendUnitOfWork = backendUnitOfWork;
        }

        public bool DoesAreaExistChildren(long id)
        {
            return _backendUnitOfWork.Area.QueryFilter(e => e.ParentId == id).Any();

        }

    }
}
