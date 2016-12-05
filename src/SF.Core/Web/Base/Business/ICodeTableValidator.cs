
using SF.Core.Abstraction.Entitys;

namespace SF.Core.Web.Base.Business
{
    public interface ICodeTabelValidator<T> where T : BaseEntity
    {
        void CanUserInsert(T entity);
        void CanUserUpdate(T entity);
        void CanUserDelete(T entity);
    }
}
