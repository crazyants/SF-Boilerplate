using SimpleFramework.Core.Models;
using SimpleFramework.Core.Abstraction.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleFramework.Core.Security
{
    public class Role : EntityModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Permission[] Permissions { get; set; }
    }
}
