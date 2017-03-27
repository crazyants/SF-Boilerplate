using SF.Entitys.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace SF.Core.Data.Dapper.Repositories
{
    public class DapperRepositoryBase<TDbContext, TEntity> : DapperRepositoryBase<TDbContext, TEntity, long>, IDapperRepository<TEntity>
       where TEntity : BaseEntity<long>
       where TDbContext : DbContext
    {
        public DapperRepositoryBase(TDbContext dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
