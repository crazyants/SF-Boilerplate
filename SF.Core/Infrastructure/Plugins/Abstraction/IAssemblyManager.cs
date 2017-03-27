using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyModel;

namespace SF.Core.Infrastructure.Plugins.Abstraction
{
    public interface IAssemblyManager
    {

        IEnumerable<Assembly> CoreApplicationAssemblies { get; }

        IEnumerable<Dependency> ApplicationDependencies { get; }

        IEnumerable<Assembly> GetReferencingAssemblies(string assemblyNameStartsWith);

        IEnumerable<Assembly> GetAssemblies(string path);

        IList<Tuple<IPlugin, IList<Assembly>>> GetPluginAndAssemblies(string path);


    }
}
