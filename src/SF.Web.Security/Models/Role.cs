
using SF.Core.Entitys.Abstraction;
using SF.Web.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SF.Web.Security
{
    public class Role : EntityModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Permission[] Permissions { get; set; }
    }
}
