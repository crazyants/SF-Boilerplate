using  SF.Core.ServiceAgents.OAuth;
using  SF.Core.ServiceAgents.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;

namespace SF.Core.ServiceAgents
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSingleServiceAgent<T>(this IServiceCollection services, Action<ServiceSettings> setupAction, Assembly assembly = null) where T : AgentBase
        {
            assembly = assembly == null ? Assembly.GetEntryAssembly() : assembly;
            return AddSingleServiceAgent<T>(services, assembly, setupAction, null);
        }

        public static IServiceCollection AddSingleServiceAgent<T>(this IServiceCollection services, Action<ServiceSettings> setupAction, Action<IServiceProvider, HttpClient> clientAction, Assembly assembly = null) where T : AgentBase
        {
            assembly = assembly == null ? Assembly.GetEntryAssembly() : assembly;
            return AddSingleServiceAgent<T>(services, assembly, setupAction, clientAction);
        }

        private static IServiceCollection AddSingleServiceAgent<T>(this IServiceCollection services,
            Assembly callingAssembly, 
            Action<ServiceSettings> setupAction, 
            Action<IServiceProvider, HttpClient> clientAction) where T : AgentBase
        {
            if (setupAction == null) throw new ArgumentNullException(nameof(setupAction), $"{nameof(setupAction)} cannot be null.");

            var serviceSettings = new ServiceSettings();
            setupAction.Invoke(serviceSettings);
            serviceSettings.UseGlobalApiKey = false;

            var type = typeof(T);

            services.Configure<ServiceAgentSettings>(s =>
            {
                s.Services.Add(type.Name, serviceSettings);
            });

            RegisterServices<T>(services, callingAssembly, clientAction);

            return services;
        }

        public static IServiceCollection AddServiceAgents(this IServiceCollection services, Action<ServiceSettingsJsonFile> setupAction, Assembly assembly = null)
        {
            assembly = assembly == null ? Assembly.GetEntryAssembly() : assembly;
            return AddServiceAgents(services, assembly, setupAction, null, null);
        }

        public static IServiceCollection AddServiceAgents(this IServiceCollection services, Action<ServiceSettingsJsonFile> setupAction, Action<IServiceProvider, HttpClient> clientAction, Assembly assembly = null)
        {
            assembly = assembly == null ? Assembly.GetEntryAssembly() : assembly;
            return AddServiceAgents(services, assembly, setupAction, null, clientAction);
        }

        public static IServiceCollection AddServiceAgents(this IServiceCollection services, 
            Action<ServiceSettingsJsonFile> jsonSetupAction,
            Action<ServiceAgentSettings> settingsSetupAction,
            Action<IServiceProvider, HttpClient> clientAction,
            Assembly assembly = null)
        {
            assembly = assembly == null ? Assembly.GetEntryAssembly() : assembly;
            return AddServiceAgents(services, assembly, jsonSetupAction, settingsSetupAction, clientAction);
        }

        private static IServiceCollection AddServiceAgents(this IServiceCollection services,
            Assembly callingAssembly,
            Action<ServiceSettingsJsonFile> jsonSetupAction,
            Action<ServiceAgentSettings> settingsSetupAction,
            Action<IServiceProvider, HttpClient> clientAction)
        {
            if (jsonSetupAction == null) throw new ArgumentNullException(nameof(jsonSetupAction), $"{nameof(jsonSetupAction)} cannot be null.");

            var serviceSettingsJsonFile = new ServiceSettingsJsonFile();
            jsonSetupAction.Invoke(serviceSettingsJsonFile);

            var serviceAgentSettings = ConfigureServiceAgentSettings(services, serviceSettingsJsonFile);

            if (settingsSetupAction != null)
                settingsSetupAction.Invoke(serviceAgentSettings);

            services.Configure<ServiceAgentSettings>(s =>
            {
                s.GlobalApiKey = serviceAgentSettings.GlobalApiKey;
                foreach (var item in serviceAgentSettings.Services)
                {
                    s.Services.Add(item.Key, item.Value);
                }
            });

            RegisterServices(services, serviceAgentSettings, callingAssembly, clientAction);

            return services;
        }

        private static ServiceAgentSettings ConfigureServiceAgentSettings(IServiceCollection services, ServiceSettingsJsonFile serviceSettingsJsonFile)
        {
             var builder = new ConfigurationBuilder().AddJsonFile(serviceSettingsJsonFile.FileName);
            var config = builder.Build();
            
            var configReader = new ServiceSettingsConfigReader();
            var serviceAgentSettings = configReader.ReadConfig(config);

            return serviceAgentSettings;
        }

        private static void RegisterServices<T>(IServiceCollection services, Assembly assembly, Action<IServiceProvider, HttpClient> clientAction) where T : AgentBase
        {
            RegisterClientFactory(services, clientAction);

            RegisterAgentType(services, assembly.GetTypes(), typeof(T));

            services.AddScoped<ITokenHelper, TokenHelper>();
        }

        private static void RegisterServices(IServiceCollection services, ServiceAgentSettings settings, Assembly assembly, Action<IServiceProvider, HttpClient> clientAction)
        {
            RegisterClientFactory(services, clientAction);

            var assemblyTypes = assembly.GetTypes();

            foreach (var item in settings.Services)
            {
                var type = assemblyTypes.Single(t => t.GetTypeInfo().BaseType == typeof(AgentBase) &&
                                                     t.Name.StartsWith(item.Key));

                RegisterAgentType(services, assemblyTypes, type);
            }

            services.AddScoped<ITokenHelper, TokenHelper>();
        }

        private static void RegisterClientFactory(IServiceCollection services, Action<IServiceProvider, HttpClient> clientAction)
        {
            services.AddSingleton<IHttpClientFactory, HttpClientFactory>(sp =>
            {
                var factory = new HttpClientFactory(sp);

                if (clientAction != null)
                    factory.AfterClientCreated += clientAction;

                return factory;
            });
        }

        private static void RegisterAgentType(IServiceCollection services, Type[] assemblyTypes, Type implementationType)
        {
            var interfaceTypeName = $"I{implementationType.Name}";
            var interfaceType = assemblyTypes.SingleOrDefault(t => t.Name == interfaceTypeName && t.GetTypeInfo().IsInterface);

            if (interfaceType != null)
            {
                services.AddScoped(interfaceType, implementationType);
            }
            else
            {
                services.AddScoped(implementationType);
            }
        }

        private static Type TryGetInterface(Type[] assemblyTypes, Type type)
        {
            var interfaceTypeName = $"I{type.Name}";
            var interfaceType = assemblyTypes.SingleOrDefault(t => t.Name == interfaceTypeName && t.GetTypeInfo().IsInterface);
            return interfaceType;
        }
    }
}
