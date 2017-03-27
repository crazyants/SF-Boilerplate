
using SF.Core.Infrastructure.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SF.Core
{
    public static class ExtensionManager
    {
        private static List<Assembly> assemblies;
        private static List<IModuleInitializer> modules;
        public static List<ModuleInfo> Modules { get; set; }

        public static List<Assembly> Assemblies
        {
            get
            {
                if (ExtensionManager.assemblies == null)
                    throw new InvalidOperationException("Assemblies not set");

                return ExtensionManager.assemblies;
            }
        }

        public static List<IModuleInitializer> Extensions
        {
            get
            {
                if (ExtensionManager.modules == null)
                    ExtensionManager.modules = ExtensionManager.GetInstances<IModuleInitializer>();

                return ExtensionManager.modules;
            }
        }

        public static void SetAssemblies(List<Assembly> assemblies)
        {
            ExtensionManager.assemblies = assemblies;
        }

        public static void SetExtension(IModuleInitializer moduleInitializer)
        {
            ExtensionManager.Extensions.Add(moduleInitializer);
        }

        public static void SetModules(List<ModuleInfo> modules)
        {
            ExtensionManager.Modules = modules;
        }

        public static Type GetImplementation<T>()
        {
            return ExtensionManager.GetImplementation<T>(null);
        }

        public static Type GetImplementation<T>(Func<Assembly, bool> predicate)
        {
            List<Type> implementations = ExtensionManager.GetImplementations<T>(predicate);

            if (implementations.Count() == 0)
                throw new ArgumentException("Implementation of " + typeof(T) + " not found");

            return implementations.FirstOrDefault();
        }

        public static List<Type> GetImplementations<T>()
        {
            return ExtensionManager.GetImplementations<T>(null);
        }

        public static List<Type> GetImplementations<T>(Func<Assembly, bool> predicate)
        {
            List<Type> implementations = new List<Type>();

            foreach (Assembly assembly in ExtensionManager.GetAssemblies(predicate))
                foreach (Type type in assembly.GetTypes())
                    if (typeof(T).GetTypeInfo().IsAssignableFrom(type) && type.GetTypeInfo().IsClass)
                        implementations.Add(type);

            return implementations;
        }

        public static T GetInstance<T>()
        {
            return ExtensionManager.GetInstance<T>(null);
        }

        public static T GetInstance<T>(Func<Assembly, bool> predicate)
        {
            List<T> instances = ExtensionManager.GetInstances<T>(predicate);

            if (instances.Count() == 0)
                throw new ArgumentException("Instance of " + typeof(T) + " can't be created");

            return instances.FirstOrDefault();
        }

        public static List<T> GetInstances<T>()
        {
            return ExtensionManager.GetInstances<T>(null);
        }

        public static List<T> GetInstances<T>(Func<Assembly, bool> predicate)
        {
            List<T> instances = new List<T>();

            foreach (Type implementation in ExtensionManager.GetImplementations<T>())
            {
                if (!implementation.GetTypeInfo().IsAbstract)
                {
                    T instance = (T)Activator.CreateInstance(implementation);

                    instances.Add(instance);
                }
            }

            return instances;
        }

        private static List<Assembly> GetAssemblies(Func<Assembly, bool> predicate)
        {
            if (predicate == null)
                return ExtensionManager.Assemblies;

            return ExtensionManager.Assemblies.Where(predicate).ToList();
        }
    }
}