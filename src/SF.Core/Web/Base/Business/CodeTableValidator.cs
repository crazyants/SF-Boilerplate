
using SF.Core.Abstraction.Entitys;
using System;

namespace SF.Core.Web.Base.Business
{
    public class CodeTabelValidator<T> : ICodeTabelValidator<T> where T : BaseEntity
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
