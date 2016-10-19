using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SimpleFramework.Infrastructure.Data;
using SimpleFramework.Core.Entitys;

namespace SimpleFramework.Core.Services
{
    public class WidgetInstanceService : IWidgetInstanceService
    {
        private IRepository<WidgetInstanceEntity> _widgetInstanceRepository;

        public WidgetInstanceService(IRepository<WidgetInstanceEntity> widgetInstanceRepository)
        {
            _widgetInstanceRepository = widgetInstanceRepository;
        }

        public IQueryable<WidgetInstanceEntity> GetPublished()
        {
            return _widgetInstanceRepository.Queryable().Include(x => x.Widget).Where(x =>
                x.PublishStart.HasValue && x.PublishStart.Value < DateTimeOffset.Now
                && (!x.PublishEnd.HasValue || x.PublishEnd.Value > DateTimeOffset.Now));
        }
    }
}
