 
namespace SF.Entitys.Abstraction
{
    using System;

    /// <summary>
    /// Represents an entity that has an unique identifier, created, updated and deleted metadata
    /// </summary>
    /// <typeparam name="TIdentity">The identifier type</typeparam>
    /// <typeparam name="TDeletedBy">The deleted by type</typeparam>
    public abstract class EntityWithLocalDeletedMeta<TIdentity, TDeletedBy>
        : EntityWithTypedId<TIdentity>, IHaveLocalDeletedMeta<TDeletedBy>
    {
        #region Implementation of IHaveLocalDeletedMeta<TDeletedBy>

        /// <summary>
        /// The <see cref="DateTime"/> when it was soft deleted
        /// </summary>
        public virtual DateTime? DeletedOn { get; set; }

        /// <summary>
        /// The identifier (or entity) which soft deleted this entity
        /// </summary>
        public virtual TDeletedBy DeletedBy { get; set; }

        #endregion

        /// <summary>
        /// Creates a new instance
        /// </summary>
        protected EntityWithLocalDeletedMeta()
        {
            
        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="id">The entity id</param>
        protected EntityWithLocalDeletedMeta(TIdentity id) : base(id)
        {
            
        }
    }

    /// <summary>
    /// Represents an entity that has an unique identifier and deleted metadata,
    /// using a <see cref="string"/> as an identifier for the <see cref="IHaveLocalDeletedMeta{T}.DeletedBy"/>
    /// </summary>
    /// <typeparam name="TIdentity">The identifier type</typeparam>
    public abstract class EntityWithLocalDeletedMeta<TIdentity>
        : EntityWithLocalDeletedMeta<TIdentity, string>, IHaveLocalDeletedMeta
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        protected EntityWithLocalDeletedMeta()
        {

        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="id">The entity id</param>
        protected EntityWithLocalDeletedMeta(TIdentity id) : base(id)
        {

        }
    }
}
