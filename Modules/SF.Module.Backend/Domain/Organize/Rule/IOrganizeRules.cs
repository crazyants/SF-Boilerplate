using SF.Core.Abstraction.Domain;
using SF.Entitys;
using SF.Module.Backend.Data.Entitys;
using SF.Module.Backend.Domain.DataItem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.Backend.Domain.Organize.Rule
{
    public interface IOrganizeRules : IRules<OrganizeEntity>
    {
        bool DoesOrganizeExist(long id);
        bool IsOrganizeIdUnique(long id);
        bool IsOrganizeCodeUnique(string code, long dataItemId = 0);
        bool IsOrganizeNameUnique(string fullName, long organizeId = 0);
        bool IsOrganizeShortNameUnique(string shortName, long organizeId = 0);
 
    }
}
