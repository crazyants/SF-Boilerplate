namespace SimpleFramework.Infrastructure.Entitys
{
    public abstract class EntityWithTypedId<TId> : ValidatableObject, IEntityWithTypedId<TId>
    {
        public TId Id { get;  set; }
    }
}
