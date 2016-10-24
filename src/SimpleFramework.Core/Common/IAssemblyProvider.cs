
using System.Collections.Generic;
using System.Reflection;

namespace SimpleFramework.Core.Common
{
  public interface IAssemblyProvider
  {
    IEnumerable<Assembly> GetAssemblies(string path);

    IEnumerable<ModuleInfo> GetModules(string path);
    }
}