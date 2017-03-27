using Microsoft.EntityFrameworkCore;
using SF.Core.EFCore.UoW;
using SF.Entitys.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Data
{
    /// <summary>
    /// 基础仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseRepository<TEntity> : IEFCoreQueryableRepository<TEntity, long> where TEntity : BaseEntity
    {
    }
    /// <summary>
    /// 基础仓储类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseRepository<TEntity> : EFCoreQueryableRepository<TEntity, long>, IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        public BaseRepository(DbContext context) : base(context)
        {
        }
        public override IQueryable<TEntity> QueryById(long id)
        {
            return Query().Where(e => e.Id == id);
        }
    }
    /// <summary>
    /// 基础仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseRepository<TEntity, Tkey> : IEFCoreQueryableRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
    }
    /// <summary>
    /// 基础仓储类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseRepository<TEntity, Tkey> : EFCoreQueryableRepository<TEntity, Tkey>, IBaseRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public BaseRepository(DbContext context) : base(context)
        {
        }
        public override IQueryable<TEntity> QueryById(Tkey id)
        {
            return Query().Where(e => e.Id.Equals(id));
        }
    }
}
