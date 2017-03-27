 
namespace SF.Entitys.Abstraction
{
    using System;

#if !NET20

    /// <summary>
    /// Represents an entity that has an unique identifier, deleted metadata and version info, 
    /// using a byte[] for the <see cref="IHaveVersion{T}.Version"/>.
    /// </summary>
    /// <typeparam name="TIdentity">The identifier type</typeparam>
    /// <typeparam name="TDeletedBy">The deleted by type</typeparam>
    public abstract class EntityWithDeletedMetaAndVersionAsByteArray<TIdentity, TDeletedBy>
        : EntityWithTypedId<TIdentity>, IHaveDeletedMeta<TDeletedBy>, IHaveVersionAsByteArray
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

        #region Implementation of IHaveVersionAsByteArray

        /// <summary>
        /// The entity version
        /// </summary>
        public virtual byte[] Version { get; set; }

        #endregion

        /// <summary>
        /// Creates a new instance
        /// </summary>
        protected EntityWithDeletedMetaAndVersionAsByteArray()
        {
            
        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="id">The entity id</param>
        protected EntityWithDeletedMetaAndVersionAsByteArray(TIdentity id) : base(id)
        {
            
        }
    }

    /// <summary>
    /// Represents an entity that has an unique identifier, created, updated, deleted metadata and version info, 
    /// using a byte[] for the <see cref="IHaveVersion{T}.Version"/> and a <see cref="string"/> as an 
    /// identifier for the <see cref="IHaveDeletedMeta{T}.DeletedBy"/>
    /// </summary>
    /// <typeparam name="TIdentity">The identifier type</typeparam>
    public abstract class EntityWithDeletedMetaAndVersionAsByteArray<TIdentity>
        : EntityWithDeletedMetaAndVersionAsByteArray<TIdentity, string>, IHaveDeletedMeta
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        protected EntityWithDeletedMetaAndVersionAsByteArray()
        {

        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="id">The entity id</param>
        protected EntityWithDeletedMetaAndVersionAsByteArray(TIdentity id) : base(id)
        {

        }
    }

#endif
}
