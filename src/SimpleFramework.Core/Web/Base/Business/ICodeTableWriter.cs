
using SimpleFramework.Core.Abstraction.Entitys;
using System.Threading.Tasks;

namespace SimpleFramework.Core.Web.Base.Business
{
    public interface ICodetableWriter<T> where T : EntityBase
    {
        Task<T> InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
