
using MediatR;
using SF.Core.Entitys.Abstraction;

namespace SF.Core.Abstraction.Events
{
    /// <summary>
    /// A container for passing entities that have been deleted. This is not used for entities that are deleted logicaly via a bit column.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityDeleted<T>: INotification where T : BaseEntity
    {

        public EntityDeleted(T entity)
        {
            this.Entity = entity;
        }


        public T Entity { get; private set; }
    }
}
