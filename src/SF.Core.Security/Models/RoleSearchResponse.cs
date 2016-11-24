using SF.Core.Entitys;

namespace SF.Core.Security
{
    public class RoleSearchResponse
    {
        public RoleEntity[] Roles { get; set; }
        public int TotalCount { get; set; }
    }
}
