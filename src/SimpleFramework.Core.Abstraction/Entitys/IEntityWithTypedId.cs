namespace SimpleFramework.Core.Abstraction.Entitys
{
    public interface IEntityWithTypedId<TId>
    {
        TId Id { get; set; }
    }
}
