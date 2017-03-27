
using SF.Entitys.Abstraction;
using System;

namespace SF.Web.Base.Business
{
    public class GenericValidator<T> : IGenericValidator<T> where T : BaseEntity
    {
        public void CanUserDelete(T entity)
        {
            throw new NotImplementedException();
        }

        public void CanUserInsert(T entity)
        {
            throw new NotImplementedException();
        }

        public void CanUserUpdate(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
