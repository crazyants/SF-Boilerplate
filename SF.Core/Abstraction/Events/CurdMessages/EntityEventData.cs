using MediatR;
using SF.Entitys.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Core.Abstraction.Events
{

    public class EntityEventData<T> : INotification
    {

        public EntityEventData(T entity)
        {
            this.Entity = entity;
        }


        public T Entity { get; private set; }
    }
}
