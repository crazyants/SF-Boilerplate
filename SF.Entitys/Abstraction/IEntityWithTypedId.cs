namespace SF.Entitys.Abstraction
{
    public interface IEntityWithTypedId<TId>: IEntity
    {
        TId Id { get; set; }
    }
  
}
