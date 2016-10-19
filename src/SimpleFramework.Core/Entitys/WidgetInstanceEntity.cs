using System;
using SimpleFramework.Infrastructure.Entitys;

namespace SimpleFramework.Core.Entitys
{
    public class WidgetInstanceEntity :AuditableEntity
    {
        public WidgetInstanceEntity()
        {
        }

        public string Name { get; set; }

        public DateTimeOffset? PublishStart { get; set; }

        public DateTimeOffset? PublishEnd { get; set; }

        public long WidgetId { get; set; }

        public WidgetEntity Widget { get; set; }

        public long WidgetZoneId { get; set; }

        public WidgetZoneEntity WidgetZone { get; set; }

        public int DisplayOrder { get; set; }

        public string Data { get; set; }

        public string HtmlData { get; set; }

        /// <summary>
        /// This property cannot be used to filter again DB because it don't exist in database
        /// </summary>
        public bool IsPublished
        {
            get
            {
                return PublishStart.HasValue && PublishStart.Value < DateTimeOffset.Now && (!PublishEnd.HasValue || PublishEnd.Value > DateTimeOffset.Now);
            }
        }
    }
}
