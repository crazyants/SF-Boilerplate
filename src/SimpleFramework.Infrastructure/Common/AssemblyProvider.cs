
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Logging;

namespace SimpleFramework.Infrastructure.Common
{
    public class AssemblyProvider : IAssemblyProvider
    {
        protected ILogger<AssemblyProvider> logger;

        public Func<Assembly, bool> IsCandidateAssembly { get; set; }
        public Func<Library, bool> IsCandidateCompilationLibrary { get; set; }

        public AssemblyProvider(IServiceProvider serviceProvider)
        {
            this.logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<AssemblyProvider>();
            this.IsCandidateAssembly = assembly =>
             /* !assembly.FullName.StartsWith("Microsoft.") &&*/ !assembly.FullName.Contains("SimpleFramework.WebHost") && assembly.FullName.StartsWith("SimpleFramework.");
            this.IsCandidateCompilationLibrary = library =>
              library.Name != "NETStandard.Library" && !library.Name.StartsWith("Microsoft.") && !library.Name.StartsWith("System.");
        }

        public IEnumerable<Assembly> GetAssemblies(string path)
        {
            List<Assembly> assemblies = new List<Assembly>();

            assemblies.AddRange(this.GetAssembliesFromPath(path));
          //  assemblies.AddRange(this.GetAssembliesFromDependencyContext());
            return assemblies;
        }

        public IEnumerable<ModuleInfo> GetModules(string path)
        {
            IList<ModuleInfo> modules = new List<ModuleInfo>();

            if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
            {
                this.logger.LogInformation("Discovering and loading assemblies from path '{0}'", path);
                var moduleRootFolder = new DirectoryInfo(path);
                var moduleFolders = moduleRootFolder.GetDirectories();
                foreach (var moduleFolder in moduleFolders)
                {

                    var binFolder = new DirectoryInfo(Path.Combine(moduleFolder.FullName, "bin"));
                    if (!binFolder.Exists)
                    {
                        continue;
                    }

                    foreach (var file in binFolder.GetFileSystemInfos("*.dll", SearchOption.AllDirectories))
                    {
                        //获取程序集信息，如已加载具有相同名称的组件则获取已加载的程序集信息
                        Assembly assembly;
                        try
                        {
                            assembly = Assembly.Load(new AssemblyName(Path.GetFileNameWithoutExtension(file.Name)));

                        }
                        catch (FileLoadException ex)
                        {
                            this.logger.LogWarning("Error loading assembly '{0}'", file.FullName);
                            this.logger.LogInformation(ex.ToString());
                            continue;
                        }

                        if (assembly.FullName.Contains(moduleFolder.Name))
                        {
                            modules.Add(new ModuleInfo
                            {
                                Name = moduleFolder.Name,
                                Assembly = assembly,
                                Path = moduleFolder.FullName
                            });
                        }
                    }

                }
            }
            return modules;

        }

        private IEnumerable<Assembly> GetAssembliesFromPath(string path)
        {
            List<Assembly> assemblies = new List<Assembly>();

            if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
            {
                this.logger.LogInformation("Discovering and loading assemblies from path '{0}'", path);

                foreach (string extensionPath in Directory.EnumerateFileSystemEntries(path, "*.dll", SearchOption.AllDirectories))
                {
                    Assembly assembly = null;

                    try
                    {
                        assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(extensionPath);
                    }

                    catch (Exception ex)
                    {
                        if (ex.Message == "Assembly with same name is already loaded")
                        {
                            // Get loaded assembly
                            try
                            {
                                assembly = Assembly.Load(new AssemblyName(Path.GetFileNameWithoutExtension(extensionPath)));
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                        }
                        else
                        {
                            this.logger.LogWarning("Error loading assembly '{0}'", extensionPath);
                            this.logger.LogInformation(ex.ToString());
                            //throw;
                            continue;
                        }

                    }
                    if (this.IsCandidateAssembly(assembly)&&!assemblies.Contains(assembly))
                    {
                        assemblies.Add(assembly);
                        this.logger.LogInformation("Assembly '{0}' is discovered and loaded", assembly.FullName);
                    }
                }
            }

            else
            {
                if (string.IsNullOrEmpty(path))
                    this.logger.LogWarning("Discovering and loading assemblies from path skipped: path not provided", path);

                else this.logger.LogWarning("Discovering and loading assemblies from path '{0}' skipped: path not found", path);
            }

            return assemblies;
        }

        private IEnumerable<Assembly> GetAssembliesFromDependencyContext()
        {
            List<Assembly> assemblies = new List<Assembly>();

            this.logger.LogInformation("Discovering and loading assemblies from DependencyContext");

            foreach (CompilationLibrary compilationLibrary in DependencyContext.Default.CompileLibraries)
            {
                if (this.IsCandidateCompilationLibrary(compilationLibrary))
                {
                    Assembly assembly = null;

                    try
                    {
                        assembly = Assembly.Load(new AssemblyName(compilationLibrary.Name));
                        assemblies.Add(assembly);
                        this.logger.LogInformation("Assembly '{0}' is discovered and loaded", assembly.FullName);
                    }

                    catch (Exception e)
                    {
                        this.logger.LogWarning("Error loading assembly '{0}'", compilationLibrary.Name);
                        this.logger.LogInformation(e.ToString());
                    }
                }
            }

            return assemblies;
        }


    }
}