using SF.Core.Abstraction.Domain;
using SF.Entitys;
using SF.Module.Backend.Data.Entitys;
using SF.Module.Backend.Domain.DataItem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.Backend.Domain.Area.Rule
{
    public interface IAreaRules : IRules<AreaEntity>
    {
        bool DoesAreaExistChildren(long id);
 
    }
}
