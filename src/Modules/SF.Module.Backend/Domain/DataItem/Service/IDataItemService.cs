using SF.Module.Backend.Domain.DataItem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.Backend.Domain.DataItem.Service
{
    public interface IDataItemService
    {
        IEnumerable<DataItemViewModel> GetChildren(int id, int rootDataItemId);
    }
}
