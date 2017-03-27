 
namespace SF.Entitys.Abstraction
{
    using System;

#if !NET20

    /// <summary>
    /// Represents an entity that has an unique identifier and created metadata
    /// </summary>
    /// <typeparam name="TIdentity">The identifier type</typeparam>
    /// <typeparam name="TCreatedBy">The created by type</typeparam>
    public abstract class EntityWithCreatedMeta<TIdentity, TCreatedBy>
        : EntityWithTypedId<TIdentity>, IHaveCreatedMeta<TCreatedBy>
    {
        private DateTimeOffset _createdOn;

        #region Implementation of IHaveCreatedMeta<TCreatedBy>

        /// <summary>
        /// The <see cref="DateTimeOffset"/> when it was created
        /// </summary>
        public virtual DateTimeOffset CreatedOn
        {
            get { return _createdOn; }
            set { _createdOn = value; }
        }

        /// <summary>
        /// The identifier (or entity) which first created this entity
        /// </summary>
        public virtual TCreatedBy CreatedBy { get; set; }

        #endregion

        /// <summary>
        /// Creates a new instance and sets the <see cref="CreatedOn"/> 
        /// to <see cref="DateTimeOffset.Now"/>
        /// </summary>
        protected EntityWithCreatedMeta()
        {
            _createdOn = DateTimeOffset.Now;
        }

        /// <summary>
        /// Creates a new instance and sets the <see cref="CreatedOn"/> 
        /// to <see cref="DateTimeOffset.Now"/>
        /// </summary>
        /// <param name="id">The entity id</param>
        protected EntityWithCreatedMeta(TIdentity id) : base(id)
        {
            _createdOn = DateTimeOffset.Now;
        }
    }

    /// <summary>
    /// Represents an entity that has an unique identifier, created, updated and deleted metadata,
    /// using a <see cref="string"/> as an identifier for the <see cref="IHaveCreatedMeta{T}.CreatedBy"/>
    /// </summary>
    /// <typeparam name="TIdentity">The identifier type</typeparam>
    public abstract class EntityWithCreatedMeta<TIdentity>
        : EntityWithCreatedMeta<TIdentity, string>, IHaveCreatedMeta
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        protected EntityWithCreatedMeta()
        {
            
        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="id">The entity id</param>
        protected EntityWithCreatedMeta(TIdentity id) : base(id)
        {
            
        }
    }

#endif
}
