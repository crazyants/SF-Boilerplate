/*******************************************************************************
* 命名空间: SF.Core.Abstraction.UoW
*
* 功 能： N/A
* 类 名： IAsyncRepository
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/11 10:33:24 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SF.Core.Abstraction.UoW
{

    /// <summary>
    /// Represents a repository that only exposes asynchronous operations 
    /// to manipulate persisted entities
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId">The entity id type</typeparam>
    public interface IAsyncRepository<TEntity, in TId> : ISimplePersistenceRepository
        where TEntity : class
    {
        #region GetById

        /// <summary>
        /// Gets an entity by its unique identifier from this repository asynchronously
        /// </summary>
        /// <param name="id">The entity unique identifier</param>
        /// <returns>A <see cref="Task"/> that will fetch the entity</returns>
        Task<TEntity> GetByIdAsync(TId id);

        /// <summary>
        /// Gets an entity by its unique identifier from this repository asynchronously
        /// </summary>
        /// <param name="id">The entity unique identifier</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> that will fetch the entity</returns>
        Task<TEntity> GetByIdAsync(TId id, CancellationToken ct);

        #endregion

        #region Add

        /// <summary>
        /// Adds the entity to the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="entities">The entity to add</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> AddAsync(
            IEnumerable<TEntity> entities, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="entities">The entity to add</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> AddAsync(params TEntity[] entities);

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <param name="entities">The entity to add</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> AddAsync(CancellationToken ct, params TEntity[] entities);

        #endregion

        #region Update

        /// <summary>
        /// Updates the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> UpdateAsync(
            IEnumerable<TEntity> entities, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> UpdateAsync(params TEntity[] entities);

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <param name="entities">The entities to update</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> UpdateAsync(CancellationToken ct, params TEntity[] entities);

        #endregion

        #region Delete

        /// <summary>
        /// Deletes the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> DeleteAsync(TEntity entity, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> DeleteAsync(
            IEnumerable<TEntity> entities, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> DeleteAsync(params TEntity[] entities);

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <param name="entities">The entities to delete</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> DeleteAsync(CancellationToken ct, params TEntity[] entities);

        #endregion

        #region Total

        /// <summary>
        /// Gets the total entities in the repository asynchronously
        /// </summary>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the total</returns>
        Task<long> TotalAsync(CancellationToken ct = default(CancellationToken));

        #endregion

        #region Exists

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id">The entity unique identifier</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>True if entity exists</returns>
        Task<bool> ExistsAsync(TId id, CancellationToken ct = default(CancellationToken));

        #endregion
    }

    /// <summary>
    /// Represents a repository that only exposes asynchronous operations 
    /// to manipulate persisted entities
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId01">The entity id first type</typeparam>
    /// <typeparam name="TId02">The entity id second type</typeparam>
    public interface IAsyncRepository<TEntity, in TId01, in TId02> : ISimplePersistenceRepository
        where TEntity : class
    {
        #region GetById

        /// <summary>
        /// Gets an entity by its unique identifier from this repository asynchronously
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> that will fetch the entity</returns>
        Task<TEntity> GetByIdAsync(TId01 id01, TId02 id02, CancellationToken ct = default(CancellationToken));

        #endregion

        #region Add

        /// <summary>
        /// Adds the entity to the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="entities">The entity to add</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> AddAsync(
            IEnumerable<TEntity> entities, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="entities">The entity to add</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> AddAsync(params TEntity[] entities);

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <param name="entities">The entity to add</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> AddAsync(CancellationToken ct, params TEntity[] entities);

        #endregion

        #region Update

        /// <summary>
        /// Updates the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> UpdateAsync(
            IEnumerable<TEntity> entities, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> UpdateAsync(params TEntity[] entities);

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <param name="entities">The entities to update</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> UpdateAsync(CancellationToken ct, params TEntity[] entities);

        #endregion

        #region Delete

        /// <summary>
        /// Deletes the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> DeleteAsync(TEntity entity, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> DeleteAsync(
            IEnumerable<TEntity> entities, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> DeleteAsync(params TEntity[] entities);

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <param name="entities">The entities to delete</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> DeleteAsync(CancellationToken ct, params TEntity[] entities);

        #endregion

        #region Total

        /// <summary>
        /// Gets the total entities in the repository asynchronously
        /// </summary>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the total</returns>
        Task<long> TotalAsync(CancellationToken ct = default(CancellationToken));

        #endregion

        #region Exists

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>True if entity exists</returns>
        Task<bool> ExistsAsync(TId01 id01, TId02 id02, CancellationToken ct = default(CancellationToken));

        #endregion
    }

    /// <summary>
    /// Represents a repository that only exposes asynchronous operations 
    /// to manipulate persisted entities
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId01">The entity id first type</typeparam>
    /// <typeparam name="TId02">The entity id second type</typeparam>
    /// <typeparam name="TId03">The entity id third type</typeparam>
    public interface IAsyncRepository<TEntity, in TId01, in TId02, in TId03> : ISimplePersistenceRepository
        where TEntity : class
    {
        #region GetById

        /// <summary>
        /// Gets an entity by its unique identifier from this repository asynchronously
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third identifier value</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> that will fetch the entity</returns>
        Task<TEntity> GetByIdAsync(TId01 id01, TId02 id02, TId03 id03, CancellationToken ct = default(CancellationToken));

        #endregion

        #region Add

        /// <summary>
        /// Adds the entity to the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="entities">The entity to add</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> AddAsync(
            IEnumerable<TEntity> entities, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="entities">The entity to add</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> AddAsync(params TEntity[] entities);

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <param name="entities">The entity to add</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> AddAsync(CancellationToken ct, params TEntity[] entities);

        #endregion

        #region Update

        /// <summary>
        /// Updates the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> UpdateAsync(
            IEnumerable<TEntity> entities, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> UpdateAsync(params TEntity[] entities);

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <param name="entities">The entities to update</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> UpdateAsync(CancellationToken ct, params TEntity[] entities);

        #endregion

        #region Delete

        /// <summary>
        /// Deletes the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> DeleteAsync(TEntity entity, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> DeleteAsync(
            IEnumerable<TEntity> entities, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> DeleteAsync(params TEntity[] entities);

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <param name="entities">The entities to delete</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> DeleteAsync(CancellationToken ct, params TEntity[] entities);

        #endregion

        #region Total

        /// <summary>
        /// Gets the total entities in the repository asynchronously
        /// </summary>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the total</returns>
        Task<long> TotalAsync(CancellationToken ct = default(CancellationToken));

        #endregion

        #region Exists

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third identifier value</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>True if entity exists</returns>
        Task<bool> ExistsAsync(TId01 id01, TId02 id02, TId03 id03, CancellationToken ct = default(CancellationToken));

        #endregion
    }

    /// <summary>
    /// Represents a repository that only exposes asynchronous operations 
    /// to manipulate persisted entities
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TId01">The entity id first type</typeparam>
    /// <typeparam name="TId02">The entity id second type</typeparam>
    /// <typeparam name="TId03">The entity id third type</typeparam>
    /// <typeparam name="TId04">The entity id fourth type</typeparam>
    public interface IAsyncRepository<TEntity, in TId01, in TId02, in TId03, in TId04> : ISimplePersistenceRepository
        where TEntity : class
    {
        #region GetById

        /// <summary>
        /// Gets an entity by its unique identifier from this repository asynchronously
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third identifier value</param>
        /// <param name="id04">The entity fourth identifier value</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> that will fetch the entity</returns>
        Task<TEntity> GetByIdAsync(
            TId01 id01, TId02 id02, TId03 id03, TId04 id04, CancellationToken ct = default(CancellationToken));

        #endregion

        #region Add

        /// <summary>
        /// Adds the entity to the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="entities">The entity to add</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> AddAsync(
            IEnumerable<TEntity> entities, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="entities">The entity to add</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> AddAsync(params TEntity[] entities);

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <param name="entities">The entity to add</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> AddAsync(CancellationToken ct, params TEntity[] entities);

        #endregion

        #region Update

        /// <summary>
        /// Updates the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> UpdateAsync(
            IEnumerable<TEntity> entities, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> UpdateAsync(params TEntity[] entities);

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <param name="entities">The entities to update</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> UpdateAsync(CancellationToken ct, params TEntity[] entities);

        #endregion

        #region Delete

        /// <summary>
        /// Deletes the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> DeleteAsync(TEntity entity, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> DeleteAsync(
            IEnumerable<TEntity> entities, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> DeleteAsync(params TEntity[] entities);

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <param name="entities">The entities to delete</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> DeleteAsync(CancellationToken ct, params TEntity[] entities);

        #endregion

        #region Total

        /// <summary>
        /// Gets the total entities in the repository asynchronously
        /// </summary>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the total</returns>
        Task<long> TotalAsync(CancellationToken ct = default(CancellationToken));

        #endregion

        #region Exists

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third identifier value</param>
        /// <param name="id04">The entity fourth identifier value</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>True if entity exists</returns>
        Task<bool> ExistsAsync(
            TId01 id01, TId02 id02, TId03 id03, TId04 id04, CancellationToken ct = default(CancellationToken));

        #endregion
    }

    /// <summary>
    /// Represents a repository that only exposes asynchronous operations 
    /// to manipulate persisted entities
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public interface IAsyncRepository<TEntity> : ISimplePersistenceRepository
        where TEntity : class
    {
        #region GetById

        /// <summary>
        /// Gets an entity by its unique identifier from this repository asynchronously
        /// </summary>
        /// <param name="ids">The entity unique identifiers</param>
        /// <returns>A <see cref="Task{TResult}"/> that will fetch the entity</returns>
        Task<TEntity> GetByIdAsync(params object[] ids);

        /// <summary>
        /// Gets an entity by its unique identifier from this repository asynchronously
        /// </summary>
        /// <param name="ids">The entity unique identifier</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> that will fetch the entity</returns>
        Task<TEntity> GetByIdAsync(CancellationToken ct, params object[] ids);

        #endregion

        #region Add

        /// <summary>
        /// Adds the entity to the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="entities">The entity to add</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> AddAsync(
            IEnumerable<TEntity> entities, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="entities">The entity to add</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> AddAsync(params TEntity[] entities);

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <param name="entities">The entity to add</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> AddAsync(CancellationToken ct, params TEntity[] entities);

        #endregion

        #region Update

        /// <summary>
        /// Updates the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> UpdateAsync(
            IEnumerable<TEntity> entities, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> UpdateAsync(params TEntity[] entities);

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <param name="entities">The entities to update</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> UpdateAsync(CancellationToken ct, params TEntity[] entities);

        #endregion

        #region Delete

        /// <summary>
        /// Deletes the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entity</returns>
        Task<TEntity> DeleteAsync(TEntity entity, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> DeleteAsync(
            IEnumerable<TEntity> entities, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> DeleteAsync(params TEntity[] entities);

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <param name="entities">The entities to delete</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the entities</returns>
        Task<IEnumerable<TEntity>> DeleteAsync(CancellationToken ct, params TEntity[] entities);

        #endregion

        #region Total

        /// <summary>
        /// Gets the total entities in the repository asynchronously
        /// </summary>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>A <see cref="Task{TResult}"/> containing the total</returns>
        Task<long> TotalAsync(CancellationToken ct = default(CancellationToken));

        #endregion

        #region Exists

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="ids">The entity unique identifiers</param>
        /// <returns>True if entity exists</returns>
        Task<bool> ExistsAsync(params object[] ids);

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="ids">The entity unique identifiers</param>
        /// <param name="ct">The <see cref="CancellationToken"/> for the returned task</param>
        /// <returns>True if entity exists</returns>
        Task<bool> ExistsAsync(CancellationToken ct, params object[] ids);

        #endregion
    }
}
