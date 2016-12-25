
using SF.Core.Entitys.Abstraction;

namespace SF.Web.Common.Base.Business
{
    public interface ICodeTabelValidator<T> where T : BaseEntity
    {
        void CanUserInsert(T entity);
        void CanUserUpdate(T entity);
        void CanUserDelete(T entity);
    }
}
