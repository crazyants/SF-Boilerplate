 
namespace SF.Entitys.Abstraction
{
    /// <summary>
    /// Represents an entity that has an unique identifier and soft delete
    /// </summary>
    /// <typeparam name="TIdentity">The identifier type</typeparam>
    public abstract class EntityWithSoftDelete<TIdentity>
        : EntityWithTypedId<TIdentity>, IHaveSoftDelete
    {
        #region Implementation of IHaveSoftDelete

        /// <summary>
        /// Is the entity deleted?
        /// </summary>
        public virtual bool Deleted { get; set; }

        #endregion

        /// <summary>
        /// Creates a new instance
        /// </summary>
        protected EntityWithSoftDelete()
        {
        }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="id">The entity id</param>
        protected EntityWithSoftDelete(TIdentity id) : base(id)
        {
        }
    }
}
