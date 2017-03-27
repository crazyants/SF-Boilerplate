using SF.Core.Abstraction.Domain;
using SF.Entitys;
using SF.Module.Backend.Data.Entitys;
using SF.Module.Backend.Domain.DataItem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.Backend.Domain.Department.Rule
{
    public interface IDepartmentRules : IRules<DepartmentEntity>
    {
        bool DoesDepartmentExist(long id);
        bool IsDepartmentIdUnique(long id);
        bool IsDepartmentCodeUnique(string code, long dataItemId = 0);
        bool IsDepartmentNameUnique(string fullName, long organizeId = 0);
        bool IsDepartmentShortNameUnique(string shortName, long organizeId = 0);
 
    }
}
