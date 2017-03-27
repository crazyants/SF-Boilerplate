using SF.Entitys;

namespace SF.Web.Security
{
    public class RoleSearchResponse
    {
        public RoleEntity[] Roles { get; set; }
        public int TotalCount { get; set; }
    }
}
