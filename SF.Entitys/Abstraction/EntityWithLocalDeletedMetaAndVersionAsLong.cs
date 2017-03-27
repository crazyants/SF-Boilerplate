 
namespace SF.Entitys.Abstraction
{
    using System;

    /// <summary>
    /// Represents an entity that has an unique identifier, local deleted metadata and version info, 
    /// using a long for the <see cref="IHaveVersion{T}.Version"/>.
    /// </summary>
    /// <typeparam name="TIdentity">The identifier type</typeparam>
    /// <typeparam name="TDeletedBy">The deleted by type</typeparam>
    public abstract class EntityWithLocalDeletedMetaAndVersionAsLong<TIdentity, TDeletedBy>
        : EntityWithTypedId<TIdentity>, IHaveLocalDeletedMeta<TDeletedBy>, IHaveVersionAsLong
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

        #region Implementation of IHaveVersionAsLong

        /// <summary>
        /// The entity version
        /// </summary>
        public virtual long Version { get; set; }

        #endregion

        /// <summary>
        /// Creates a new instance
        /// </summary>
        protected EntityWithLocalDeletedMetaAndVersionAsLong()
        {
            
        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="id"></param>
        protected EntityWithLocalDeletedMetaAndVersionAsLong(TIdentity id)
            : base(id)
        {
            
        }
    }

    /// <summary>
    /// Represents an entity that has an unique identifier, created, updated, deleted metadata and version info, 
    /// using a long for the <see cref="IHaveVersion{T}.Version"/> and a <see cref="string"/> as an 
    /// identifier for the <see cref="IHaveLocalDeletedMeta{T}.DeletedBy"/>
    /// </summary>
    /// <typeparam name="TIdentity">The identifier type</typeparam>
    public abstract class EntityWithLocalDeletedMetaAndVersionAsLong<TIdentity>
        : EntityWithLocalDeletedMetaAndVersionAsLong<TIdentity, string>, IHaveLocalDeletedMeta
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        protected EntityWithLocalDeletedMetaAndVersionAsLong()
        {

        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="id"></param>
        protected EntityWithLocalDeletedMetaAndVersionAsLong(TIdentity id)
            : base(id)
        {

        }
    }
}
