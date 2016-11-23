
using SimpleFramework.Core.Abstraction.Entitys;
using System.Threading.Tasks;

namespace SimpleFramework.Core.Web.Base.Business
{
    /// <summary>
    /// 写入处理
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICodetableWriter<T, TKey> where T : IEntityWithTypedId<TKey>
    {
        Task<T> InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(TKey id);
    }
}
