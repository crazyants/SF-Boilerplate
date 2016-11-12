using MediatR;
using SimpleFramework.Core.Events;
using SimpleFramework.Core.Abstraction.Data;
using SimpleFramework.Module.ActivityLog.Models;
using SimpleFramework.Module.ActivityLog.Data;
using SimpleFramework.Core.Abstraction.UoW.Helper;

namespace SimpleFramework.Module.ActivityLog.Events
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
