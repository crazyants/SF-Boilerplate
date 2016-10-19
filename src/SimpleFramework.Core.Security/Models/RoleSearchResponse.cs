using SimpleFramework.Core.Entitys;

namespace SimpleFramework.Core.Security
{
    public class RoleSearchResponse
    {
        public RoleEntity[] Roles { get; set; }
        public int TotalCount { get; set; }
    }
}
