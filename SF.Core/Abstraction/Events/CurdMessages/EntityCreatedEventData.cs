
using MediatR;
using SF.Entitys.Abstraction;

namespace SF.Core.Abstraction.Events
{
    /// <summary>
    /// A container for entities that have been inserted.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class EntityCreatedEventData<TEntity> : EntityEventData<TEntity>
    {
        public EntityCreatedEventData(TEntity entity)
            : base(entity)
        {
        }
    }

}
