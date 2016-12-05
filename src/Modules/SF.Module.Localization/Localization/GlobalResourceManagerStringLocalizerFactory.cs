using System;
using System.Collections.Concurrent;
using System.IO;
using System.Reflection;
using System.Resources;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

// http://stackoverflow.com/questions/34890555/asp-net-5-localization-using-view-components-in-a-separate-assembly

namespace Microsoft.Extensions.Localization
{
    // https://github.com/aspnet/Localization/blob/dev/src/Microsoft.Extensions.Localization/ResourceManagerStringLocalizerFactory.cs
    // https://github.com/dotnet/corefx/blob/master/src/System.Resources.ResourceManager/src/System/Resources/ResourceManager.cs

    /// <summary>
    /// An <see cref="IStringLocalizerFactory"/> that creates instances of <see cref="ResourceManagerStringLocalizer"/>.
    /// </summary>
    public class GlobalResourceManagerStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly IResourceNamesCache _resourceNamesCache = new ResourceNamesCache();
        private readonly ConcurrentDictionary<string, GlobalResourceManagerStringLocalizer> _localizerCache =
            new ConcurrentDictionary<string, GlobalResourceManagerStringLocalizer>();
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly string _resourcesRelativePath;
        private readonly GlobalResourceOptions globalResourceOptions;

        /// <summary>
        /// Creates a new <see cref="ResourceManagerStringLocalizer"/>.
        /// </summary>
        /// <param name="hostingEnvironment">The <see cref="IHostingEnvironment"/>.</param>
        /// <param name="localizationOptions">The <see cref="IOptions{LocalizationOptions}"/>.</param>
        public GlobalResourceManagerStringLocalizerFactory(
            IHostingEnvironment hostingEnvironment,
            IOptions<LocalizationOptions> localizationOptions,
            IOptions<GlobalResourceOptions> globalResourceOptionsAccessor
            )
        {
            if (hostingEnvironment == null)
            {
                throw new ArgumentNullException(nameof(hostingEnvironment));
            }

            if (localizationOptions == null)
            {
                throw new ArgumentNullException(nameof(localizationOptions));
            }

            _hostingEnvironment = hostingEnvironment;
            _resourcesRelativePath = localizationOptions.Value.ResourcesPath ?? string.Empty;
            if (!string.IsNullOrEmpty(_resourcesRelativePath))
            {
                _resourcesRelativePath = _resourcesRelativePath.Replace(Path.AltDirectorySeparatorChar, '.')
                    .Replace(Path.DirectorySeparatorChar, '.') + ".";
            }

            globalResourceOptions = globalResourceOptionsAccessor.Value;
        }

        /// <summary>
        /// Creates a <see cref="ResourceManagerStringLocalizer"/> using the <see cref="Assembly"/> and
        /// <see cref="Type.FullName"/> of the specified <see cref="Type"/>.
        /// </summary>
        /// <param name="resourceSource">The <see cref="Type"/>.</param>
        /// <returns>The <see cref="ResourceManagerStringLocalizer"/>.</returns>
        public IStringLocalizer Create(Type resourceSource)
        {
            if (resourceSource == null)
            {
                throw new ArgumentNullException(nameof(resourceSource));
            }

            var typeInfo = resourceSource.GetTypeInfo();
            var assembly = typeInfo.Assembly;

            Assembly appAssembly = null;
            ResourceManager appResourceManager = null;
            string appBaseName = null;

            // Re-root the base name if a resources path is set
            string baseName;
            if (assembly.FullName.StartsWith(_hostingEnvironment.ApplicationName))
            {
                baseName = string.IsNullOrEmpty(_resourcesRelativePath)
                ? typeInfo.FullName
                : _hostingEnvironment.ApplicationName + "." + _resourcesRelativePath
                    + TrimPrefix(typeInfo.FullName, _hostingEnvironment.ApplicationName + ".");
            }
            else
            {
                var libName = TrimOnFirstComma(assembly.FullName);
                appAssembly = Assembly.GetEntryAssembly();
                appBaseName = _hostingEnvironment.ApplicationName
                    + "." + _resourcesRelativePath + TrimPrefix(typeInfo.FullName, libName + ".");
                appResourceManager = new ResourceManager(appBaseName, appAssembly);
                
                baseName = libName + "." + TrimPrefix(typeInfo.FullName, libName + ".");
            }

            return _localizerCache.GetOrAdd(baseName, _ =>
                    new GlobalResourceManagerStringLocalizer(
                        new ResourceManager(baseName, assembly),
                        assembly,
                        baseName,
                        _resourceNamesCache,
                        globalResourceOptions,
                        appResourceManager
                        )
                        );


        }

        
        /// <summary>
        /// Creates a <see cref="ResourceManagerStringLocalizer"/>.
        /// </summary>
        /// <param name="baseName">The base name of the resource to load strings from.</param>
        /// <param name="location">The location to load resources from.</param>
        /// <returns>The <see cref="ResourceManagerStringLocalizer"/>.</returns>
        public IStringLocalizer Create(string baseName, string location)
        {
            if (baseName == null)
            {
                throw new ArgumentNullException(nameof(baseName));
            }

            location = location ?? _hostingEnvironment.ApplicationName;

            Assembly appAssembly = null;
            ResourceManager appResourceManager = null;
            string appBaseName = null;

            if (location.StartsWith(_hostingEnvironment.ApplicationName))
            {
                baseName = location + "." + _resourcesRelativePath + TrimPrefix(baseName, location + ".");
            }
            else
            {
                
                appAssembly = Assembly.GetEntryAssembly();
                appBaseName = _hostingEnvironment.ApplicationName
                    + "." + _resourcesRelativePath + TrimPrefix(baseName, location + ".");
                appResourceManager = new ResourceManager(appBaseName, appAssembly);

                baseName = location + "." + TrimPrefix(baseName, location + ".");
            }

            return _localizerCache.GetOrAdd($"B={baseName},L={location}", _ =>
            {
                
                var assembly = Assembly.Load(new AssemblyName(location));
                return new GlobalResourceManagerStringLocalizer(
                    new ResourceManager(baseName, assembly),
                    assembly,
                    baseName,
                    _resourceNamesCache,
                    globalResourceOptions,
                    appResourceManager
                    );
            });
        }
        
        private static string TrimPrefix(string name, string prefix)
        {
            if (name.StartsWith(prefix, StringComparison.Ordinal))
            {
                return name.Substring(prefix.Length);
            }

            return name;
        }

        private static string TrimOnFirstComma(string name)
        {
            return name.Substring(0, name.IndexOf(","));
        }
    }
}
