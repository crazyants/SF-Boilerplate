 
namespace SF.Entitys.Abstraction
{
    using System;

#if !NET20

    /// <summary>
    /// Represents an entity that has an unique identifier and deleted metadata
    /// </summary>
    /// <typeparam name="TIdentity">The identifier type</typeparam>
    /// <typeparam name="TDeletedBy">The deleted by type</typeparam>
    public abstract class EntityWithDeletedMeta<TIdentity, TDeletedBy>
        : EntityWithTypedId<TIdentity>, IHaveDeletedMeta<TDeletedBy>
    {
        #region Implementation of IHaveDeletedMeta<TDeletedBy>

        /// <summary>
        /// The <see cref="DateTimeOffset"/> when it was soft deleted
        /// </summary>
        public virtual DateTimeOffset? DeletedOn { get; set; }

        /// <summary>
        /// The identifier (or entity) which soft deleted this entity
        /// </summary>
        public virtual TDeletedBy DeletedBy { get; set; }

        #endregion

        /// <summary>
        /// Creates a new instance
        /// </summary>
        protected EntityWithDeletedMeta()
        {
            
        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="id">The entity id</param>
        protected EntityWithDeletedMeta(TIdentity id) : base(id)
        {
            
        }
    }

    /// <summary>
    /// Represents an entity that has an unique identifier, created, updated and deleted metadata,
    /// using a <see cref="string"/> as an identifier for the  <see cref="IHaveDeletedMeta{T}.DeletedBy"/>
    /// </summary>
    /// <typeparam name="TIdentity">The identifier type</typeparam>
    public abstract class EntityWithDeletedMeta<TIdentity>
        : EntityWithDeletedMeta<TIdentity, string>, IHaveDeletedMeta
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        protected EntityWithDeletedMeta()
        {

        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="id">The entity id</param>
        protected EntityWithDeletedMeta(TIdentity id) : base(id)
        {

        }
    }

#endif
}
