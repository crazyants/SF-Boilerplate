using SF.Entitys.Abstraction;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using System;
using SF.Web.Models;

namespace SF.Module.Backend.Domain.Module.ViewModel
{
    public class PermissionViemModel : EntityModelBase
    {
        public PermissionViemModel()
        {
        }

        public string Name { get; set; }
        public string ActionAddress { get; set; }
        public string Description { get; set; }
        public long ModuleId { get; set; } = -1;
    }
}
