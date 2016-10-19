using System;
using System.Linq;
using Omu.ValueInjecter;
using SimpleFramework.Core.Entitys;

namespace SimpleFramework.Core.Security.Converters
{
    public static class RoleAssignmentConverter
    {
       
        public static void Patch(this UserRoleEntity source, UserRoleEntity target)
        {
            if (target == null)
                throw new ArgumentNullException("target");

            var patchInjection = new PatchInjection<UserRoleEntity>(x => x.RoleId, x => x.UserId);
            target.InjectFrom(patchInjection, source);

        }
    }
}
