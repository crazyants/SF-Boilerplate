namespace SimpleFramework.Infrastructure.Entitys
{
    public interface IEntityWithTypedId<TId>
    {
        TId Id { get; }
    }
}
