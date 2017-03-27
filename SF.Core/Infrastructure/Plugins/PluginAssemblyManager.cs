using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;
using System.Runtime.Loader;
using SF.Core.Infrastructure.Plugins.Abstraction;

namespace SF.Core.Infrastructure.Plugins
{
    public class PluginAssemblyManager : IAssemblyManager
    {
        internal static HashSet<string> ReferenceAssembliesStartsWith { get; } = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "ModCore"
        };

        private List<Assembly> _coreApplicationAssemblies;
        private List<Dependency> _dependencies;

        public IEnumerable<Assembly> CoreApplicationAssemblies
        {
            get
            {
                if (_coreApplicationAssemblies == null)
                {
                    _coreApplicationAssemblies = new List<Assembly>();

                    foreach (var assemblyStart in ReferenceAssembliesStartsWith)
                    {
                        _coreApplicationAssemblies.AddRange(GetReferencingAssemblies(assemblyStart));
                    }
                }

                return _coreApplicationAssemblies;
            }
        }

        public IEnumerable<Dependency> ApplicationDependencies
        {
            get
            {
                if (_dependencies == null)
                {
                    _dependencies = new List<Dependency>();

                    foreach (var assemblyStart in ReferenceAssembliesStartsWith)
                    {
                        _dependencies.AddRange(GetDependencies(assemblyStart));
                    }
                }

                return _dependencies;
            }
        }

        public PluginAssemblyManager()
        {

        }

        public IEnumerable<Assembly> GetReferencingAssemblies(string assemblyNameStartsWith)
        {
            var assemblies = new List<Assembly>();
            var dependencies = DependencyContext.Default.RuntimeLibraries;
            foreach (var library in dependencies)
            {
                if (IsCandidateLibrary(library, assemblyNameStartsWith))
                {
                    var assembly = Assembly.Load(new AssemblyName(library.Name));
                    assemblies.Add(assembly);

                }
            }
            return assemblies;
        }

        public IEnumerable<Assembly> GetAssemblies(string path)
        {

            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");

            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException(path);

            var assemblies = new List<Assembly>();

            foreach (var folder in Directory.EnumerateDirectories(path))
            {
                var binFolder = new DirectoryInfo(Path.Combine(folder, "bin"));
                if (!binFolder.Exists)
                    continue;
                var dllsToAdd = Directory.EnumerateFiles(Path.Combine(folder, "bin"), "*.dll");
                foreach (string dllPath in dllsToAdd)
                {
                    var assemblyName = FromPath(dllPath);

                    if (!AlreadyLoaded(assemblyName))
                    {
                        Assembly assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(dllPath);
                        assemblies.Add(assembly);
                    }
                }
            }

            return assemblies;
        }

        public IList<Tuple<IPlugin, IList<Assembly>>> GetPluginAndAssemblies(string path)
        {
            var tuples = new List<Tuple<IPlugin, IList<Assembly>>>();

            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");

            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException(path);


            foreach (var folder in Directory.EnumerateDirectories(path))
            {
                var assemblies = new List<Assembly>();
                IPlugin pluginInstance = null;
                var binFolder = new DirectoryInfo(Path.Combine(folder, "bin"));
                if (!binFolder.Exists)
                    continue;

                var dllsToAdd = Directory.EnumerateFiles(Path.Combine(folder, "bin"), "*.dll");
                foreach (string dllPath in dllsToAdd)
                {
                    var assemblyName = FromPath(dllPath);

                    if (!AlreadyLoaded(assemblyName))
                    {
                        Assembly assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(dllPath);
                        assemblies.Add(assembly);

                        var pluginType = assembly.GetImplementationOrDefault<IPlugin>();
                        if (pluginType != null)
                        {
                            pluginInstance = assembly.GetInstance<IPlugin>();
                        }
                    }
                }

                if (pluginInstance != null)
                    tuples.Add(new Tuple<IPlugin, IList<Assembly>>(pluginInstance, assemblies));

            }

            return tuples;
        }

        public IList<Assembly> GetAssembliesWithPlugin(string path)
        {
            return this.GetAssemblies(path)
                    .Where(a => a.GetTypes().Any(type =>
                         typeof(IPlugin).GetTypeInfo().IsAssignableFrom(type)
                     && typeof(BasePlugin).GetTypeInfo().IsAssignableFrom(type)
                     && type.GetTypeInfo().IsClass
                    ))
                    .ToList();
        }


        private AssemblyName FromPath(string dllPath)
        {
            var pieces = dllPath.Split('\\').ToArray();
            var dllName = pieces[pieces.Length - 1];
            var assemblyName = dllName.Substring(0, dllName.Length - 4);
            return new AssemblyName(assemblyName);
        }

        private IEnumerable<Dependency> GetDependencies(string assemblyStartsWith)
        {
            var dependencies = new HashSet<Dependency>();

            var depcontext = DependencyContext.Default.RuntimeLibraries;
            foreach (var library in depcontext)
            {
                if (IsCandidateLibrary(library, assemblyStartsWith))
                {
                    foreach (var dep in library.Dependencies)
                    {
                        dependencies.Add(dep);
                    }
                }
            }

            return dependencies;
        }

        private bool IsCandidateLibrary(RuntimeLibrary library, string assemblyName)
        {
            return library.Name.StartsWith(assemblyName)
                || library.Dependencies.Any(d => d.Name.StartsWith(assemblyName));
        }

        private bool AlreadyLoaded(AssemblyName assemblyName)
        {
            return CoreApplicationAssemblies.Any(a => a.GetName().Name == assemblyName.Name)
                || ApplicationDependencies.Any(a => a.Name == assemblyName.Name);
        }

    }
}
