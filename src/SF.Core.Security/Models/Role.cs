
using SF.Core.Abstraction.Entitys;
using SF.Core.Web.Base.Datatypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SF.Core.Security
{
    public class Role : EntityModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Permission[] Permissions { get; set; }
    }
}
