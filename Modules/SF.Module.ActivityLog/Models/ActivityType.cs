using SF.Entitys.Abstraction;
using System.Collections.Generic;

namespace SF.Module.ActivityLog.Models
{
    public class ActivityTypeEntity : BaseEntity
    {
        public ActivityTypeEntity()
        {
            ActivityEntitys = new List<ActivityEntity>();
        }
        public int Level { get; set; }
        public string Name { get; set; }

        public virtual IList<ActivityEntity> ActivityEntitys { get; set; }
    }
}
