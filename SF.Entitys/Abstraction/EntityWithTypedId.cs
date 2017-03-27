namespace SF.Entitys.Abstraction
{
    public abstract class EntityWithTypedId<TId> : ValidatableObject, IEntityWithTypedId<TId>
    {
        public TId Id { get;  set; }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        protected EntityWithTypedId()
        {

        }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        /// <param name="id">The id for the entity</param>
        protected EntityWithTypedId(TId id)
        {
            Id = id;
        }
    }
}
