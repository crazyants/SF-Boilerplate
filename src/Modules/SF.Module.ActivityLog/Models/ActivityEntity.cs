using System;
using SF.Core.Abstraction.Entitys;

namespace SF.Module.ActivityLog.Models
{
    public class ActivityEntity : BaseEntity
    {
        public long ActivityTypeId { get; set; }

        public ActivityType ActivityType { get; set; }

        public long EntityId { get; set; }

        public long EntityTypeId { get; set; }
    }
}
