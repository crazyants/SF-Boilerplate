
using SF.Core.Entitys.Abstraction;
using System;

namespace SF.Web.Common.Base.Business
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
