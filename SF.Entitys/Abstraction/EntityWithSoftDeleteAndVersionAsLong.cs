 
namespace SF.Entitys.Abstraction
{
    /// <summary>
    /// Represents an entity that has an unique identifier, soft delete and version info, 
    /// using a long for the <see cref="IHaveVersion{T}.Version"/>.
    /// </summary>
    /// <typeparam name="TIdentity">The identifier type</typeparam>
    public abstract class EntityWithSoftDeleteAndVersionAsLong<TIdentity>
        : EntityWithTypedId<TIdentity>, IHaveSoftDelete, IHaveVersionAsLong
    {
        #region Implementation of IHaveSoftDelete

        /// <summary>
        /// Is the entity deleted?
        /// </summary>
        public virtual bool Deleted { get; set; }

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
        protected EntityWithSoftDeleteAndVersionAsLong()
        {
        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="id">The entity id</param>
        protected EntityWithSoftDeleteAndVersionAsLong(TIdentity id) : base(id)
        {
        }
    }
}