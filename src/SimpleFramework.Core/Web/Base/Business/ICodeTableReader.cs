
using SimpleFramework.Core.Abstraction.Entitys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleFramework.Core.Web.Base.Business
{
    public interface ICodetableReader<T>  where T : IEntityWithTypedId<long>
    {
        Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T> GetAll();
        T Get(int id);
        Task<T> GetAsync(int id);
    }
}