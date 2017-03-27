using SF.Entitys.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace SF.Core.Data.Dapper.Repositories
{
    public interface IDapperRepository<TEntity> : IDapperRepository<TEntity, long> where TEntity :  BaseEntity<long>
    {
    }
}
