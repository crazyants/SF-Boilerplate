using SF.Core.Abstraction.Domain;
using SF.Entitys;
using SF.Module.Backend.Data.Entitys;
using SF.Module.Backend.Domain.DataItem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.Backend.Domain.DMOS.Rule
{
    public interface IDMOSRules : IRules<DMOSEntity>
    {
        bool DoesDMOSExist(long id);
        bool IsDMOSIdUnique(long id);
        bool IsDMOSCodeUnique(string code, long dataItemId = 0);
        bool IsDMOSNameUnique(string fullName, long organizeId = 0);
 
    }
}
