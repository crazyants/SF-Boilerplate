
using MediatR;
using SF.Core.Entitys.Abstraction;
using System.Collections.Generic;
namespace SF.Core.Abstraction.Events
{
    /// <summary>
    /// A container for entities that are updated.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityUpdated<T> : INotification where T : BaseEntity
    {

        public EntityUpdated(T entity)
        {
            this.Entity = entity;
        }

        public T Entity { get; private set; }

    }
}
