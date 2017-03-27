
using SF.Entitys.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SF.Web.Security
{
    public class Role  
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Permission[] Permissions { get; set; }
    }
}
