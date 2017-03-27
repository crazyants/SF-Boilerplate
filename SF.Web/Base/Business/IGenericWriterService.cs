
using SF.Entitys.Abstraction;
using System.Threading.Tasks;

namespace SF.Web.Base.Business
{
    /// <summary>
    /// 一般写入服务处理类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericWriterService<T, TKey> where T : IEntityWithTypedId<TKey>
    {
        Task<T> InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(TKey id);
    }
}
