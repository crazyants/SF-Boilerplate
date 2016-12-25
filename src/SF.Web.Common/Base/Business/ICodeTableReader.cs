
using SF.Core.Entitys.Abstraction;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SF.Web.Common.Base.Business
{
    /// <summary>
    /// 读取处理
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICodetableReader<T,TKey>  where T : IEntityWithTypedId<TKey>
    {
        Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T> GetAll();
        T Get(TKey id);
        Task<T> GetAsync(TKey id);
    }
}