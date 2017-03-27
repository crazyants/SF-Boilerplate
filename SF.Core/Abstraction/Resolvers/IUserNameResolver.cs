
namespace SF.Core.Abstraction.Resolvers
{
    /// <summary>
    /// 用户名转换器
    /// </summary>
    public interface IUserNameResolver
    {
        string GetCurrentUserName();
    }
    public static class UserNameResolverExtension
    {
        /// <summary>
        ///  获取当前用户名
        /// </summary>
        /// <param name="userNameResolver"></param>
        /// <returns></returns>
        public static string GetCurrentUserName(this IUserNameResolver userNameResolver)
        {
            var result = userNameResolver != null ? userNameResolver.GetCurrentUserName() : "unknown";
            return result;
        }
    }
}
