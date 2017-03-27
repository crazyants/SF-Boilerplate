using SF.Core.Abstraction.Domain;
using SF.Entitys;
using SF.Module.Backend.Data.Entitys;
using SF.Module.Backend.Domain.DataItem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.Backend.Domain.Module.Rule
{
    public interface IModuleRules : IRules<ModuleEntity>
    {
        bool DoesModuleExistChildren(long id);
        bool IsModuleNameUnique(string name, long moduleId = 0);
        bool IsModuleCodeUnique(string code, long moduleId = 0);
    }
}
