using SF.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.Backend.Domain.Module.ViewModel
{
    public class ModuleSearchRequest : EntityModelBase
    {
        public string Condition { get; set; }
        public string KeyWord { get; set; }
        public long ParentId { get; set; }
 
    }
}
