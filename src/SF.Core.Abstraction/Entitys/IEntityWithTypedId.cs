namespace SF.Core.Abstraction.Entitys
{
    public interface IEntityWithTypedId<TId>: IEntity
    {
        TId Id { get; set; }
    }
  
}
