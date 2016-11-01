using System.Collections.Generic;
using SimpleFramework.Core.Abstraction.Entitys;

namespace SimpleFramework.Module.Localization.Models
{
    public class Culture : EntityBase
    {
        public string Name { get; set; }

        public IList<Resource> Resources { get; set; }
    }
}
