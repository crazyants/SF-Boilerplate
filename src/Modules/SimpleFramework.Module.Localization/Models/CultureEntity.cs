using System.Collections.Generic;
using SimpleFramework.Core.Abstraction.Entitys;

namespace SimpleFramework.Module.Localization.Models
{
    public class CultureEntity : BaseEntity
    {
        public string Name { get; set; }

        public IList<ResourceEntity> Resources { get; set; }
    }
}
