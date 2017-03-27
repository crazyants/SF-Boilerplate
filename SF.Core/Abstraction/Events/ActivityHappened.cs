using System;
using MediatR;

namespace SF.Core.Abstraction.Events
{
    public class ActivityHappened : INotification
    {
        public long ActivityTypeId { get; set; }

        public long EntityId { get; set; }

        public long EntityTypeId { get; set; }

        public DateTimeOffset TimeHappened { get; set; }
    }
}
