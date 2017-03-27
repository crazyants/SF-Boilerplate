using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SF.Entitys
{
    public class UserRoleEntity : IdentityUserRole<long>
    {
        public override long UserId { get; set; }

        public UserEntity User { get; set; }

        public override long RoleId { get; set; }

        public RoleEntity Role { get; set; }
    }
}
