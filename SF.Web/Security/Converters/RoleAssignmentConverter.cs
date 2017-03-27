using System;
using System.Linq;
using SF.Entitys;
using AutoMapper;

namespace SF.Web.Security.Converters
{
    public static class RoleAssignmentConverter
    {
       
        public static void Patch(this UserRoleEntity source, UserRoleEntity target)
        {
            if (target == null)
                throw new ArgumentNullException("target");

            target = Mapper.Map<UserRoleEntity, UserRoleEntity>(source);
        }
    }
}
