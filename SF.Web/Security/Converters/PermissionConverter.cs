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
    public static class PermissionEntityConverter
    {
        public static Permission ToCoreModel(this PermissionEntity source)
        {
            var result = new Permission();
            result = Mapper.Map<PermissionEntity, Permission>(source);
            return result;
        }


        public static PermissionEntity ToPemissionDataModel(this Permission source)
        {
            var result = new PermissionEntity();
            result = Mapper.Map<Permission,PermissionEntity > (source);
            return result;
        }

       
    }
}
