using SF.Entitys.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Core.Abstraction.Domain
{
    public interface IRules<T> where T : IEntity
    {
        bool AllowDelete(T entity);
    }
}
