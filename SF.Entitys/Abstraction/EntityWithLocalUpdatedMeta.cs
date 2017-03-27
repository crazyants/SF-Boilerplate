 
namespace SF.Entitys.Abstraction
{
    using System;

    /// <summary>
    /// Represents an entity that has an unique identifier and updated metadata
    /// </summary>
    /// <typeparam name="TIdentity">The identifier type</typeparam>
    /// <typeparam name="TUpdatedBy">The updated by type</typeparam>
    public abstract class EntityWithLocalUpdatedMeta<TIdentity, TUpdatedBy>
        : EntityWithTypedId<TIdentity>, IHaveLocalUpdatedMeta<TUpdatedBy>
    {
        private DateTime _updatedOn;

        #region Implementation of IHaveLocalUpdatedMeta<TUpdatedBy>

        /// <summary>
        /// The <see cref="DateTime"/> when it was last updated
        /// </summary>
        public virtual DateTime UpdatedOn
        {
            get { return _updatedOn; }
            set { _updatedOn = value; }
        }

        /// <summary>
        /// The identifier (or entity) which last updated this entity
        /// </summary>
        public virtual TUpdatedBy UpdatedBy { get; set; }

        #endregion

        /// <summary>
        /// Creates a new instance and sets the <see cref="UpdatedOn"/> 
        /// to <see cref="DateTime.Now"/>
        /// </summary>
        protected EntityWithLocalUpdatedMeta()
        {
            _updatedOn = DateTime.Now;
        }

        /// <summary>
        /// Creates a new instance and sets the <see cref="UpdatedOn"/> 
        /// to <see cref="DateTime.Now"/>
        /// </summary>
        /// <param name="id">The entity id</param>
        protected EntityWithLocalUpdatedMeta(TIdentity id) : base(id)
        {
            _updatedOn = DateTime.Now;
        }
    }

    /// <summary>
    /// Represents an entity that has an unique identifier and updated metadata,
    /// using a <see cref="string"/> as an identifier for the <see cref="IHaveLocalUpdatedMeta{T}.UpdatedBy"/>
    /// </summary>
    /// <typeparam name="TIdentity">The identifier type</typeparam>
    public abstract class EntityWithLocalUpdatedMeta<TIdentity>
        : EntityWithLocalUpdatedMeta<TIdentity, string>, IHaveLocalUpdatedMeta
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        protected EntityWithLocalUpdatedMeta()
        {
            
        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="id">The entity id</param>
        protected EntityWithLocalUpdatedMeta(TIdentity id) : base(id)
        {
            
        }
    }
}
