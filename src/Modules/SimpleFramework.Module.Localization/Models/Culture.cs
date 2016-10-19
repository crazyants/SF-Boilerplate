using System.Collections.Generic;
using SimpleFramework.Infrastructure.Entitys;

namespace SimpleFramework.Module.Localization.Models
{
    public class Culture : Entity
    {
        public string Name { get; set; }

        public IList<Resource> Resources { get; set; }
    }
}
