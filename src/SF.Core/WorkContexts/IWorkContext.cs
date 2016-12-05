using System.Threading.Tasks;
using SF.Core.Entitys;

namespace SF.Core.WorkContexts
{
    public interface IWorkContext
    {
        Task<UserEntity> GetCurrentUser();
    }
}
