
using MediatR;
using SF.Entitys.Abstraction;

namespace SF.Core.Abstraction.Events
{
    /// <summary>
    /// A container for passing entities that have been deleted. This is not used for entities that are deleted logicaly via a bit column.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class EntityDeletedEventData<TEntity> : EntityEventData<TEntity>
    {
        public EntityDeletedEventData(TEntity entity)
            : base(entity)
        {

        }

    }
}
