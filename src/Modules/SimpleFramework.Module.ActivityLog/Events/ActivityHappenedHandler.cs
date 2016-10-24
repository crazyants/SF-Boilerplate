using MediatR;
using SimpleFramework.Core.Events;
using SimpleFramework.Core.Abstraction.Data;
using SimpleFramework.Module.ActivityLog.Models;

namespace SimpleFramework.Module.ActivityLog.Events
{
    /// <summary>
    /// 操作日志事件
    /// 
    /// </summary>
    public class ActivityHappenedHandler : INotificationHandler<ActivityHappened>
    {
        private readonly IRepository<Activity> _activityRepository;

        public ActivityHappenedHandler(IRepository<Activity> activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public void Handle(ActivityHappened notification)
        {
            var activity = new Activity
            {
                ActivityTypeId = notification.ActivityTypeId,
                EntityId = notification.EntityId,
                EntityTypeId = notification.EntityTypeId,
                CreatedOn = notification.TimeHappened
            };

            _activityRepository.Insert(activity);
        }
    }
}
