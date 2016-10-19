using System.Linq;
using SimpleFramework.Core.Entitys;

namespace SimpleFramework.Core.Services
{
    public interface IWidgetInstanceService
    {
        IQueryable<WidgetInstanceEntity> GetPublished();
    }
}
