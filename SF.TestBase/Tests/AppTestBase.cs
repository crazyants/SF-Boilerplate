using Microsoft.AspNetCore.Hosting;
using SF.TestBase.APP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace SF.TestBase.Tests
{
    public abstract class AppTestBase : AspNetCoreIntegratedTestBase<Startup>
    {
        protected Assembly GetAssembly(string assemblyName)
        {
            Assembly assembly = null;
            var hev = ServiceProvider.GetService(typeof(IHostingEnvironment)) as IHostingEnvironment;
            var binFolder = new DirectoryInfo(hev.ContentRootPath);

            foreach (var file in binFolder.GetFileSystemInfos(assemblyName, SearchOption.AllDirectories))
            {

                try
                {
                    assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file.FullName);
                }
                catch (FileLoadException)
                {
                    // Get loaded assembly
                    assembly = Assembly.Load(new AssemblyName(Path.GetFileNameWithoutExtension(file.Name)));

                    if (assembly == null)
                    {
                        throw;
                    }
                }

            }
            return assembly;
        }
    }
}
