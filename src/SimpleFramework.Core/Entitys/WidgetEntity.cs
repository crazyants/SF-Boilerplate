using System;
using SimpleFramework.Infrastructure.Entitys;

namespace SimpleFramework.Core.Entitys
{
    public class WidgetEntity :AuditableEntity
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string ViewComponentName { get; set; }

        public string CreateUrl { get; set; }

        public string EditUrl { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public bool IsPublished { get; set; }
    }
}
