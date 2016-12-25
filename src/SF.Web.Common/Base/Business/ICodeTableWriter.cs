
using SF.Core.Entitys.Abstraction;
using System.Threading.Tasks;

namespace SF.Web.Common.Base.Business
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
