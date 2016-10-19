using System.Threading.Tasks;
using SimpleFramework.Core.Entitys;

namespace SimpleFramework.Core.Extensions
{
    public interface IWorkContext
    {
        Task<UserEntity> GetCurrentUser();
    }
}
