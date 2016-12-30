
namespace SF.Core.Abstraction.Resolvers
{
    /// <summary>
    /// 用户名转换器
    /// </summary>
    public interface IUserNameResolver
    {
        string GetCurrentUserName();
    }
}
