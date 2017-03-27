using System;
using System.Collections.ObjectModel;
using System.Linq;
using SF.Web.Security;
using SF.Entitys;
using SF.Core.Common;
using SF.Core.Extensions;
using System.Collections.Generic;
using AutoMapper;

namespace SF.Web.Security.Converters
{
    public static class RolePermissionConverter
    {
        public static Permission ToCoreModel(this RolePermissionEntity source)
        {
            var result = new Permission();
            //  result.InjectFrom(source.Permission);
            result = Mapper.Map<PermissionEntity, Permission>(source.Permission);
            return result;
        }


        public static RolePermissionEntity ToRolePemissionDataModel(this Permission source)
        {
            var result = new RolePermissionEntity();
            result.PermissionId = source.Id;
         
            return result;
        }

      
    }
}
