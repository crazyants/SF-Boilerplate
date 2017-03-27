
using MediatR;
using SF.Entitys.Abstraction;
using System.Collections.Generic;
namespace SF.Core.Abstraction.Events
{
    /// <summary>
    /// A container for entities that are updated.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class EntityUpdatedEventData<TEntity> : EntityEventData<TEntity>
    {
        public EntityUpdatedEventData(TEntity entity)
            : base(entity)
        {
        }
    }
}
