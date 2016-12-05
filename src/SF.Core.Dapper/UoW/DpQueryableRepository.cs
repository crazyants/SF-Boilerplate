using Dapper;
using SimpleFramework.Core.Abstraction.Entitys;
using SimpleFramework.Core.Dapper.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleFramework.Core.Dapper.UoW
{
    /// <summary>
    /// Implementation of an <see cref="IQueryableRepository{TEntity,TId}"/> for the NHibernate
    /// exposing both sync and async operations. It also exposes an <see cref="IQueryable{TEntity}"/>.
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TKey">The entity id type</typeparam>
    public abstract class DpQueryableRepository<TEntity, TKey> : IDpRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
    {
        private readonly string tableName;

        /// <summary>
        /// Creates a new repository
        /// </summary>
        /// <param name="session">The database session</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected DpQueryableRepository(DbConnection connection)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            Connection = connection;
        }

        #region Implementation of IAsyncRepository<TEntity,in TKey>

        /// <summary>
        /// Gets an entity by its unique identifier from this repository asynchronously
        /// </summary>
        /// <param name="id">The entity unique identifier</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task"/> that will fetch the entity
        /// </returns>
        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await GetByIdAsync(id, CancellationToken.None);
        }

        /// <summary>
        /// Gets an entity by its unique identifier from this repository asynchronously
        /// </summary>
        /// <param name="id">The entity unique identifier</param><param name="ct">The <see cref="T:System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1"/> that will fetch the entity
        /// </returns>
        public async Task<TEntity> GetByIdAsync(TKey id, CancellationToken ct)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await Task.Run(() => GetById(id), ct);
        }

        /// <summary>
        /// Adds the entity to the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to add</param><param name="ct">The <see cref="T:System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1"/> containing the entity
        /// </returns>
        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = new CancellationToken())
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return await Task.Run(() => Add(entity), ct);
        }

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="entities">The entity to add</param><param name="ct">The <see cref="T:System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1"/> containing the entities
        /// </returns>
        public async Task<IEnumerable<TEntity>> AddAsync(IEnumerable<TEntity> entities, CancellationToken ct = new CancellationToken())
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            return await AddAsync(ct, entities as TEntity[] ?? entities.ToArray());
        }

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="entities">The entity to add</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1"/> containing the entities
        /// </returns>
        public async Task<IEnumerable<TEntity>> AddAsync(params TEntity[] entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            return await AddAsync(CancellationToken.None, entities);
        }

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="ct">The <see cref="T:System.Threading.CancellationToken"/> for the returned task</param><param name="entities">The entity to add</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1"/> containing the entities
        /// </returns>
        public async Task<IEnumerable<TEntity>> AddAsync(CancellationToken ct, params TEntity[] entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            return await Task.Run(() => Add(entities), ct);
        }

        /// <summary>
        /// Updates the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to update</param><param name="ct">The <see cref="T:System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1"/> containing the entity
        /// </returns>
        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct = new CancellationToken())
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return await Task.Run(() => Update(entity), ct);
        }

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to update</param><param name="ct">The <see cref="T:System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1"/> containing the entities
        /// </returns>
        public async Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities, CancellationToken ct = new CancellationToken())
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            return await UpdateAsync(ct, entities as TEntity[] ?? entities.ToArray());
        }

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1"/> containing the entities
        /// </returns>
        public async Task<IEnumerable<TEntity>> UpdateAsync(params TEntity[] entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            return await UpdateAsync(CancellationToken.None, entities);
        }

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="ct">The <see cref="T:System.Threading.CancellationToken"/> for the returned task</param><param name="entities">The entities to update</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1"/> containing the entities
        /// </returns>
        public async Task<IEnumerable<TEntity>> UpdateAsync(CancellationToken ct, params TEntity[] entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            return await Task.Run(() => Update(entities), ct);
        }

        /// <summary>
        /// Deletes the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to delete</param><param name="ct">The <see cref="T:System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1"/> containing the entity
        /// </returns>
        public async Task<TEntity> DeleteAsync(TEntity entity, CancellationToken ct = new CancellationToken())
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return await Task.Run(() => Delete(entity), ct);
        }

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to delete</param><param name="ct">The <see cref="T:System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1"/> containing the entities
        /// </returns>
        public async Task<IEnumerable<TEntity>> DeleteAsync(IEnumerable<TEntity> entities, CancellationToken ct = new CancellationToken())
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            return await DeleteAsync(ct, entities as TEntity[] ?? entities.ToArray());
        }

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1"/> containing the entities
        /// </returns>
        public async Task<IEnumerable<TEntity>> DeleteAsync(params TEntity[] entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            return await DeleteAsync(CancellationToken.None, entities);
        }

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="ct">The <see cref="T:System.Threading.CancellationToken"/> for the returned task</param><param name="entities">The entities to delete</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1"/> containing the entities
        /// </returns>
        public async Task<IEnumerable<TEntity>> DeleteAsync(CancellationToken ct, params TEntity[] entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            return await Task.Run(() => Delete(entities), ct);
        }

        /// <summary>
        /// Gets the total entities in the repository asynchronously
        /// </summary>
        /// <param name="ct">The <see cref="T:System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1"/> containing the total
        /// </returns>
        public async Task<long> TotalAsync(CancellationToken ct = new CancellationToken())
        {
            return await Task.Run(() => Total(), ct);
        }

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id">The entity unique identifier</param><param name="ct">The <see cref="T:System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>
        /// True if entity exists
        /// </returns>
        public async Task<bool> ExistsAsync(TKey id, CancellationToken ct = new CancellationToken())
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return await Task.Run(() => Exists(id), ct);
        }

        #endregion

        #region Implementation of ISyncRepository<TEntity,in TKey>

        /// <summary>
        /// Gets an entity by its unique identifier from this repository
        /// </summary>
        /// <param name="id">The entity unique identifier</param>
        /// <returns>
        /// The entity or null if not found
        /// </returns>
        public TEntity GetById(TKey id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            TEntity item = default(TEntity);
            using (DbConnection cn = this.Connection)
            {
                cn.Open();
                item = cn.Query<TEntity>(this.tableName.ConstructInlineQuery<TEntity>("Id") + "=@ID", new { ID = id }).SingleOrDefault();
            }

            return item;
        }

        /// <summary>
        /// Adds the entity to the repository
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>
        /// The entity
        /// </returns>
        public TEntity Add(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            using (DbConnection cn = this.Connection)
            {
                var parameters = (object)this.Mapping(entity);
                cn.Open();
                entity.Id = cn.Insert<TKey>(this.tableName, parameters);
       
            }
            return entity;
        }

        /// <summary>
        /// Adds a range of entities to the repository
        /// </summary>
        /// <param name="entities">The entities to add</param>
        /// <returns>
        /// The range of entities added
        /// </returns>
        public IEnumerable<TEntity> Add(IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            return Add(entities as TEntity[] ?? entities.ToArray());
        }

        /// <summary>
        /// Adds a range of entities to the repository
        /// </summary>
        /// <param name="entities">The entities to add</param>
        /// <returns>
        /// The range of entities added
        /// </returns>
        public IEnumerable<TEntity> Add(params TEntity[] entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));

            var result = new TEntity[entities.Length];
            for (var i = 0; i < entities.Length; i++)
            {
                result[i] = Add(entities[i]);
            }
            return result;
        }

        /// <summary>
        /// Updates the entity in the repository
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns>
        /// The entity
        /// </returns>
        public TEntity Update(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            using (DbConnection cn = this.Connection)
            {
                var parameters = (object)this.Mapping(entity);
                cn.Open();
                cn.Update(this.tableName, parameters);
            }
            return entity;
        }

        /// <summary>
        /// Updates a range of entities in the repository
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>
        /// The entities
        /// </returns>
        public IEnumerable<TEntity> Update(IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            return Update(entities as TEntity[] ?? entities.ToArray());
        }

        /// <summary>
        /// Updates a range of entities in the repository
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>
        /// The entities
        /// </returns>
        public IEnumerable<TEntity> Update(params TEntity[] entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));

            var result = new TEntity[entities.Length];
            for (var i = 0; i < entities.Length; i++)
            {
                result[i] = Update(entities[i]);
            }
            return result;
        }

        /// <summary>
        /// Deletes the entity in the repository
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <returns>
        /// The entity
        /// </returns>
        public TEntity Delete(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            using (DbConnection cn = this.Connection)
            {
                cn.Open();
                cn.Execute("DELETE FROM dbo." + this.tableName + " WHERE Id=@ID", new { ID = entity.Id });
            }
            return entity;
        }

        /// <summary>
        /// Deletes a range of entity in the repository
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>
        /// The entities
        /// </returns>
        public IEnumerable<TEntity> Delete(IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            return Delete(entities as TEntity[] ?? entities.ToArray());
        }

        /// <summary>
        /// Deletes a range of entity in the repository
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>
        /// The entities
        /// </returns>
        public IEnumerable<TEntity> Delete(params TEntity[] entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));

            var result = new TEntity[entities.Length];
            for (var i = 0; i < entities.Length; i++)
            {
                result[i] = Delete(entities[i]);
            }
            return result;
        }

        /// <summary>
        /// Gets the total entities in the repository
        /// </summary>
        /// <returns>
        /// The total
        /// </returns>
        public long Total()
        {
            using (DbConnection cn = this.Connection)
            {
                cn.Open();
                return cn.Query<TKey>(this.tableName.ConstructInlineQuery<TKey>()).Count();
                
            }
            
        }

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id">The entity unique identifier</param>
        /// <returns>
        /// True if entity exists
        /// </returns>
        public bool Exists(TKey id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            return QueryById(id).Any();
        }

        #endregion

        #region Implementation of IExposeQueryable<TEntity,in TKey>

        /// <summary>
        /// Gets an <see cref="T:System.Linq.IQueryable`1"/>
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Linq.IQueryable`1"/> object
        /// </returns>
        public IQueryable<TEntity> Query()
        {
            return Session.Query<TEntity>();

        }

        /// <summary>
        /// Gets an <see cref="T:System.Linq.IQueryable`1"/> filtered by
        ///             the entity id
        /// </summary>
        /// <param name="id">The entity unique identifier</param>
        /// <returns>
        /// The <see cref="T:System.Linq.IQueryable`1"/> object
        /// </returns>
        public abstract IQueryable<TEntity> QueryById(TKey id);

        /// <summary>
        /// Gets an <see cref="T:System.Linq.IQueryable`1"/> 
        ///             that will also fetch, on execution, all the entity navigation properties
        /// </summary>
        /// <param name="propertiesToFetch">The navigation properties to also fetch on query execution</param>
        /// <returns>
        /// The <see cref="T:System.Linq.IQueryable`1"/> object
        /// </returns>
        public IQueryable<TEntity> QueryFetching(params Expression<Func<TEntity, object>>[] propertiesToFetch)
        {
            if (propertiesToFetch == null) throw new ArgumentNullException(nameof(propertiesToFetch));

            return propertiesToFetch.Aggregate(Query(), (current, expression) => current.Fetch(expression));
        }

        #endregion

        #region Implementation of INHRepository<TEntity,in TKey>

        /// <summary>
        /// The database session object
        /// </summary>
        public DbConnection Connection { get; }

        #endregion

        internal virtual dynamic Mapping(TEntity item)
        {
            return item;
        }
    }
}
