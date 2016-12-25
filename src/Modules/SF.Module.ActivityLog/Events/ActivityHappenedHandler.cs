using MediatR;
using SF.Core.Abstraction.Data;
using SF.Module.ActivityLog.Models;
using SF.Module.ActivityLog.Data;
using SF.Core.Abstraction.UoW.Helper;
using SF.Core.Abstraction.Events;

namespace SF.Module.ActivityLog.Events
{
    /// <summary>
    /// 操作日志事件
    /// 
    /// </summary>
    public class ActivityHappenedHandler : INotificationHandler<ActivityHappened>
    {
        private readonly IActivityUnitOfWork _unitOfWork;

        public ActivityHappenedHandler(IActivityUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Handle(ActivityHappened notification)
        {
            var activity = new ActivityEntity
            {
                ActivityTypeId = notification.ActivityTypeId,
                EntityId = notification.EntityId,
                EntityTypeId = notification.EntityTypeId,
                CreatedOn = notification.TimeHappened
            };
            _unitOfWork.ExecuteAndCommit(uow =>
            {
                return uow.Activity.Update(activity);
            });

        }
    }
}
