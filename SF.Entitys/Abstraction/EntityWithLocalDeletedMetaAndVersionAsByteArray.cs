 
namespace SF.Entitys.Abstraction
{
    using System;

    /// <summary>
    /// Represents an entity that has an unique identifier, local deleted metadata and version info, 
    /// using a byte[] for the <see cref="IHaveVersion{T}.Version"/>.
    /// </summary>
    /// <typeparam name="TIdentity">The identifier type</typeparam>
    /// <typeparam name="TDeletedBy">The deleted by type</typeparam>
    public abstract class EntityWithLocalDeletedMetaAndVersionAsByteArray<TIdentity, TDeletedBy>
        : EntityWithTypedId<TIdentity>, IHaveLocalDeletedMeta<TDeletedBy>, IHaveVersionAsByteArray
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

        #region Implementation of IHaveVersionAsByteArray

        /// <summary>
        /// The entity version
        /// </summary>
        public virtual byte[] Version { get; set; }

        #endregion

        /// <summary>
        /// Creates a new instance
        /// </summary>
        protected EntityWithLocalDeletedMetaAndVersionAsByteArray()
        {
            
        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="id">The entity id</param>
        protected EntityWithLocalDeletedMetaAndVersionAsByteArray(TIdentity id)
            : base(id)
        {
            
        }
    }

    /// <summary>
    /// Represents an entity that has an unique identifier, deleted metadata and version info, 
    /// using a byte[] for the <see cref="IHaveVersion{T}.Version"/> and a <see cref="string"/> as an 
    /// identifier for the <see cref="IHaveLocalDeletedMeta{T}.DeletedBy"/>
    /// </summary>
    /// <typeparam name="TIdentity">The identifier type</typeparam>
    public abstract class EntityWithLocalDeletedMetaAndVersionAsByteArray<TIdentity>
        : EntityWithLocalDeletedMetaAndVersionAsByteArray<TIdentity, string>, IHaveLocalDeletedMeta
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        protected EntityWithLocalDeletedMetaAndVersionAsByteArray()
        {

        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="id">The entity id</param>
        protected EntityWithLocalDeletedMetaAndVersionAsByteArray(TIdentity id)
            : base(id)
        {

        }
    }
}
