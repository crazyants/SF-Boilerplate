 
namespace SF.Entitys.Abstraction
{
    /// <summary>
    /// Represents an entity that has an unique identifier, soft delete and version info, 
    /// using a byte[] for the <see cref="IHaveVersion{T}.Version"/>.
    /// </summary>
    /// <typeparam name="TIdentity">The identifier type</typeparam>
    public abstract class EntityWithSoftDeleteAndVersionAsByteArray<TIdentity>
        : EntityWithTypedId<TIdentity>, IHaveSoftDelete, IHaveVersionAsByteArray
    {
        #region Implementation of IHaveSoftDelete

        /// <summary>
        /// Is the entity deleted?
        /// </summary>
        public virtual bool Deleted { get; set; }

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
        protected EntityWithSoftDeleteAndVersionAsByteArray()
        {
        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="id">The entity id</param>
        protected EntityWithSoftDeleteAndVersionAsByteArray(TIdentity id) : base(id)
        {
        }
    }
}
