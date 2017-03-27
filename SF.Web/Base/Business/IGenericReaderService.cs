
using SF.Entitys.Abstraction;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SF.Web.Base.Business
{
    /// <summary>
    /// 一般读取服务处理类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericReaderService<T,TKey>  where T : IEntityWithTypedId<TKey>
    {
        Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T> GetAll();
        T Get(TKey id);
        Task<T> GetAsync(TKey id);
    }
}