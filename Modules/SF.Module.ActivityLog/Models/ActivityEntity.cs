using System;
using SF.Entitys.Abstraction;

namespace SF.Module.ActivityLog.Models
{
    public class ActivityEntity : BaseEntity
    {
        public long ActivityTypeId { get; set; }

        public ActivityTypeEntity ActivityType { get; set; }

        public long EntityId { get; set; }

        public long EntityTypeId { get; set; }
    }
}
