using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CacheManager.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection.Extensions;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using AutoMapper;
using NodaTime;
using NodaTime.TimeZones;
using SF.Core.Abstraction.Resolvers;
using SF.Core.Abstraction.Steup;
using SF.Data.Identity;
using SF.Data;
using SF.Core.Interceptors;
using SF.Entitys;
using SF.Core;
using SF.Web.Components;
using SF.Core.Localization;
using SF.Web.Middleware;
using SF.Core.Abstraction.Interceptors;
using SF.Core.Abstraction.UoW;
using SF.Core.EFCore.UoW;
using SF.Core.Common.Message.Email;
using SF.Core.Common.Message.Sms;
using SF.Core.EFCore.Repository;
using SF.Core.Common.Razor;
using SF.Web.Base.Business;
using SF.Core.Errors;
using SF.Web.Formatters.CsvImportExport;
using SF.Web.Attributes;
using SF.Core.Extensions;
using SF.Core.StartupTask;
using Scrutor;
using SF.Core.Abstraction.Domain;
using SF.Web.Base.DataContractMapper;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MediatR;
using SF.Web.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using SF.Web.Security.Filters;
using SF.Web.Security.Attributes;
using SF.Web;
using SF.Core.Abstraction.GenericServices;
using SF.Web.Security.AuthorizationHandlers.Custom;
using SF.Core.Storage;
using SF.Web.Site;
using SF.Web.Site.Implementation;
using SF.Web.Extensions;
using SF.Core.Json.JsonConverters;
using SF.Core.Infrastructure.Modules;
using SF.Core.Infrastructure.Modules.Builder;
using SF.Web.Modules;

namespace SF.WebHost
{
    public class GobalExtension : ModuleInitializerBase
    {
        public override IEnumerable<KeyValuePair<int, Action<IServiceCollection>>> ConfigureServicesActionsByPriorities
        {
            get
            {
                return new Dictionary<int, Action<IServiceCollection>>()
                {
                    [1] = this.AddStaticFiles,
                    [1] = this.AddCustomizedDataStore,
                    [2] = this.AddCoreServices,
                    [4] = this.AddGobalMvc
                };
            }
        }

        public override IEnumerable<KeyValuePair<int, Action<IApplicationBuilder>>> ConfigureActionsByPriorities
        {
            get
            {
                return new Dictionary<int, Action<IApplicationBuilder>>()
                {
                    [0] = this.UseCustomStaticFiles,
                    [1] = this.UseCustomizedRequestLocalization,
                    [2] = this.UseMvc,
                    [10000] = this.UserExtensionsBuilder,
                };
            }
        }

        #region MyRegion IServiceCollection
        /// <summary>
        /// 将项目文件变成内嵌资源
        /// </summary>
        /// <param name="services"></param>
        private void AddStaticFiles(IServiceCollection services)
        {
            this.serviceProvider.GetService<IHostingEnvironment>().WebRootFileProvider = this.CreateCompositeFileProvider();
        }
        /// <summary>
        /// 配置数据库链接 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private void AddCustomizedDataStore(IServiceCollection services)
        {
            var storage = configurationRoot["DevOptions:DbPlatform"];
            //services.AddDbContext<CoreDbContext>(options =>
            //    options.UseSqlServer(this.configurationRoot.GetConnectionString("DefaultConnection"),
            //        b => b.MigrationsAssembly("SF.WebHost")));
            if (storage == "mysql")
            {
                services.AddEntityFrameworkMySql()
                 .AddDbContext<CoreDbContext>((serviceProvider, options) =>
                 options.UseMySql(configurationRoot.GetConnectionString("MysqlDatabase"),
                         b => b.MigrationsAssembly("SF.WebHost"))
                        .UseInternalServiceProvider(serviceProvider)
                        );
            }
            else if (storage == "npgsql")
            {
                services.AddEntityFrameworkNpgsql()
                  .AddDbContext<CoreDbContext>((serviceProvider, options) =>
                  options.UseNpgsql(configurationRoot.GetConnectionString("DefaultConnection"),
                         b => b.MigrationsAssembly("SF.WebHost"))
                         .UseInternalServiceProvider(serviceProvider)
                         );
            }
            else
            {
                services.AddEntityFrameworkSqlServer()
               .AddDbContext<CoreDbContext>((serviceProvider, options) =>
               options.UseSqlServer(configurationRoot.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("SF.WebHost"))
                      .UseInternalServiceProvider(serviceProvider));


            }

        }
        /// <summary>
        /// 添加全局服务注册
        /// </summary>
        /// <param name="services"></param>
        private void AddCoreServices(IServiceCollection services)
        {
            //在使用session之前要注入cacheing，因为session依赖于cache进行存储
            AddDistributedCache(services);
            services.AddSession();
            services.AddOptions();


            services.Configure<RazorViewEngineOptions>(options => { options.ViewLocationExpanders.Add(new ModuleViewLocationExpander()); });
            services.AddScoped<IViewRenderService, ViewRenderService>();

            #region 系统基础配置参数

            services.Configure<MultiTenantOptions>(configurationRoot.GetSection("MultiTenantOptions"));
            services.Configure<SmtpOptions>(configurationRoot.GetSection("SmtpOptions"));
            services.Configure<SiteConfigOptions>(configurationRoot.GetSection("SiteConfigOptions"));
            services.Configure<CachingSiteResolverOptions>(configurationRoot.GetSection("CachingSiteResolverOptions"));

            #endregion

            #region 数据库

            //Identity配置
            services.AddScoped<SignInManager<UserEntity>, SimpleSignInManager<UserEntity>>();
            //注意 必须在AddMvc之前注册
            services.AddIdentity<UserEntity, RoleEntity>(configure =>
            {
                //配置身份选项
                configure.Password.RequireDigit = false;//是否需要数字(0-9).
                configure.Password.RequireLowercase = false;//是否需要小写字母(a-z).
                configure.Password.RequireUppercase = false;//是否需要大写字母(A-Z).
                configure.Password.RequireNonAlphanumeric = false;//是否包含非字母或数字字符。
                configure.Password.RequiredLength = 6;//设置密码长度最小为6
                                                      //  configure.Cookies.ApplicationCookie.LoginPath = "/login";
                configure.Cookies.ApplicationCookie.AuthenticationScheme = "Cookies";
                configure.Cookies.ApplicationCookie.LoginPath = new PathString("/login");
                configure.Cookies.ApplicationCookie.AccessDeniedPath = new PathString("/account/forbidden");//拒绝访问路径
                configure.Cookies.ApplicationCookie.AutomaticAuthenticate = true;
                configure.Cookies.ApplicationCookie.AutomaticChallenge = true;
            })
              .AddRoleStore<SimpleRoleStore>()
              .AddUserStore<SimpleUserStore>()
             .AddDefaultTokenProviders();
            services.AddTransient(typeof(IEFCoreQueryableRepository<>), typeof(EFCoreBaseRepository<>));
            services.AddTransient(typeof(IEFCoreQueryableRepository<,>), typeof(EFCoreBaseRepository<,>));

            services.TryAddScoped<ICoreTableNames, CoreTableNames>();
            services.AddSingleton<IInterceptor, AuditableCreateInterceptor>();
            services.AddSingleton<IInterceptor, AuditableDeleteInterceptor>();
            services.AddSingleton<IInterceptor, AuditableUpdateInterceptor>();
            services.AddSingleton<IInterceptor, AuditableSiteInterceptor>();

            services.AddSingleton<IBaseUnitOfWork, BaseUnitOfWork>();
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
            services.AddSingleton<IUnitOfWork>(sp => sp.GetService<IBaseUnitOfWork>());
            services.AddSingleton<IUnitOfWorkFactory>(uow => new UnitOfWorkFactory(services.BuildServiceProvider()));
            #endregion


            services.AddScoped<HandlerExceptionFilter>();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.TryAddSingleton<IDateTimeZoneProvider>(new DateTimeZoneCache(TzdbDateTimeZoneSource.Default));
            services.TryAddScoped<ITimeZoneHelper, TimeZoneHelper>();
            services.AddScoped<IVersionProviderFactory, VersionProviderFactory>();
            services.AddScoped<IVersionProvider, SFCoreVersionProvider>();

            services.AddScoped<ITimeZoneIdResolver, RequestTimeZoneIdResolver>();

            services.AddSingleton<ISFStarter, StartupTaskStarter>();
            services.AddSingleton<IStartupTask, CoreEFStartupTask>();
            services.AddSingleton<IFileStorage>(x =>
            {
                return new FolderFileStorage(hostingEnvironment.WebRootPath);
            });

            #region 缓存

            //EF Core二级缓存
            //services.AddEFSecondLevelCache();
            services.AddSingleton(typeof(ICacheManager<>), typeof(BaseCacheManager<>));

            services.AddCacheManagerConfiguration(configurationRoot, cfg => cfg.WithMicrosoftLogging(services));
            #endregion


            #region 基础业务Service
            services.AddMultitenancy<SiteContext, CachingSiteResolver>();
            services.AddScoped<CacheHelper, CacheHelper>();
            services.AddScoped<SiteManager, SiteManager>();
            services.AddScoped<SiteDataProtector>();
            services.AddSingleton<ICurrentUserResolver, CurrentUser>();
            services.AddSingleton<IUserNameResolver, UserNameResolver>();
            services.TryAddScoped<ISiteMessageEmailSender, SiteEmailMessageSender>();
            services.TryAddScoped<ISmsSender, SiteSmsSender>();

            services.TryAddScoped<IExceptionMapper, BaseExceptionMapper>();
            services.AddTransient(typeof(IGenericWriterService<,>), typeof(GenericWriterService<,>));
            services.AddTransient(typeof(IGenericReaderService<,>), typeof(GenericReaderService<,>));

            services.AddScoped<ISiteCommands, SiteCommands>();
            services.AddScoped<ISiteQueries, SiteQueries>();

            //扫描所有模块注册以下接口所有继承类
            services.Scan(scan => scan
                .FromAssemblies(ExtensionManager.Modules.Select(x => x.Assembly))
                .AddClasses(classes => classes.AssignableTo(typeof(IRules<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(ICrudDtoMapper<,,>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(IServiceBase)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());



            #endregion

            #region Plugin
            //services.AddPlugins();
            //services.AddPluginManager(configurationRoot, hostingEnvironment);
            #endregion

            services.AddModules();

            #region Swagger

            //services.AddSwaggerGen();
            //services.ConfigureSwaggerGen(options =>
            //{
            //    options.SingleApiVersion(new Info
            //    {
            //        Version = "v1",
            //        Title = "SF API",
            //        Description = "SF ASP.NET Core Web API",
            //        TermsOfService = "None",
            //        Contact = new Contact() { Name = "疯狂蚂蚁", Email = "578117441@qq.com", Url = "www.mayisite.com" }
            //    });

            //    options.DescribeAllEnumsAsStrings();
            //});

            #endregion
            services.AddSecuritys();
            AddMediator(services);
            AddAuditStorageProviders(services);
        }

        /// <summary>
        /// 注册MVC服务
        /// </summary>
        /// <param name="services"></param>
        private void AddGobalMvc(IServiceCollection services)
        {
            IMvcBuilder mvcBuilder = services.AddMvc();
            //Csv
            var csvFormatterOptions = new CsvFormatterOptions();
            mvcBuilder.AddMvcOptions(options =>
            {
                options.Filters.AddService(typeof(HandlerExceptionFilter));
                options.InputFormatters.Add(new CsvInputFormatter(csvFormatterOptions));
                options.OutputFormatters.Add(new CsvOutputFormatter(csvFormatterOptions));
                options.FormatterMappings.SetMediaTypeMappingForFormat("csv", MediaTypeHeaderValue.Parse("text/csv"));

                options.RespectBrowserAcceptHeader = true;
                options.InputFormatters.Add(new XmlSerializerInputFormatter());
                options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
            });
            // Force Camel Case to JSON
            mvcBuilder.AddJsonOptions(opts =>
            {
                opts.SerializerSettings.ContractResolver = new BaseContractResolver();
                opts.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                opts.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                opts.SerializerSettings.Converters.Add(new TimeSpanConverter());
                opts.SerializerSettings.Converters.Add(new GuidConverter());
                opts.SerializerSettings.Formatting = Formatting.None;
            });
            //foreach (var module in ExtensionManager.Modules)
            //    // Register controller from modules
            //    mvcBuilder.AddApplicationPart(module.Assembly);

            mvcBuilder.AddRazorOptions(
              o =>
              {
                  foreach (Assembly assembly in ExtensionManager.Assemblies)
                      o.FileProviders.Add(new EmbeddedFileProvider(assembly, assembly.GetName().Name));
                  foreach (var module in ExtensionManager.Modules)
                  {
                      o.AdditionalCompilationReferences.Add(MetadataReference.CreateFromFile(module.Assembly.Location));
                  }
              }
            ).AddViewLocalization()
                .AddDataAnnotationsLocalization();

            foreach (Action<IMvcBuilder> prioritizedAddMvcAction in GetPrioritizedAddMvcActions())
            {
                this.logger.LogInformation("Executing prioritized AddMvc action '{0}' of {1}", GetActionMethodInfo(prioritizedAddMvcAction));
                prioritizedAddMvcAction(mvcBuilder);
            }

            //注册模块FluentValidation验证
            foreach (ModuleInfo moduleInfo in ExtensionManager.Modules)
            {
                mvcBuilder.AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(moduleInfo.Assembly));
            }
        }


        /// <summary>
        /// 配置审计日志
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="hostingEnvironment"></param>
        /// <returns></returns>
        private void AddAuditStorageProviders(IServiceCollection services)
        {
            var auditStorage = configurationRoot.GetValue<string>("AuditStorage");
            if (auditStorage == "SqlServer")
            {
                //必须手动创建数据库
                //https://github.com/thepirat000/Audit.NET/blob/master/src/Audit.NET.SqlServer/README.md
                Audit.Core.Configuration.DataProvider = new Audit.SqlServer.Providers.SqlDataProvider()
                {
                    ConnectionString = configurationRoot.GetConnectionString("DefaultConnection"),
                    TableName = "Event",
                    IdColumnName = "EventId",
                    JsonColumnName = "Data",
                    LastUpdatedDateColumnName = "LastUpdatedDate"
                };
            }
            else if (auditStorage == "MongoDB")
            {
                //Audit.Core.Configuration.DataProvider = new Audit.MongoDB.Providers.MongoDataProvider()
                //{
                //    ConnectionString = "mongodb://localhost:27017",
                //    Database = "Audit",
                //    Collection = "Event"
                //};
            }
            else
            {
                Audit.Core.Configuration.Setup()
                 .UseFileLogProvider(config => config
                .DirectoryBuilder(_ => $@"{Path.Combine(hostingEnvironment.ContentRootPath, "Logs")}\{DateTime.Now:yyyy-MM-dd}")
                .FilenameBuilder(auditEvent => $"{auditEvent.Environment.UserName}_{DateTime.Now.Ticks}.json"));
            }


        }
        /// <summary>
        /// 添加TagHelper分布式缓存配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private void AddDistributedCache(IServiceCollection services)
        {
            //http://www.davepaquette.com/archive/2016/05/22/ASP-NET-Core-Distributed-Cache-Tag-Helper.aspx
            //services.AddSingleton<IDistributedCache>(serviceProvider =>
            //    new SqlServerCache(new SqlServerCacheOptions()
            //    {
            //        ConnectionString = configuration.GetConnectionString("DefaultConnection"),
            //        SchemaName = "dbo",
            //        TableName = "Core_TagDistributedCache"
            //    }));
            services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
            //  services.AddSqlServerCache(SqlServerCacheOptions =>
            // {
            //     SqlServerCacheOptions.ConnectionString = configuration.GetConnectionString("DefaultConnection");
            //     SqlServerCacheOptions.SchemaName = "dbo";
            //     SqlServerCacheOptions.TableName = "Core_TagDistributedCache";
            // });
            //  services.Configure<RedisCacheOptions>(opt =>
            //{
            //    opt.Configuration = "localhost";
            //    opt.InstanceName = "test";
            //});
            //services.AddMemoryCache();
        }


        //全局构建服务
        private void AddMediator(IServiceCollection services)
        {

            services.AddScoped<IMediator, Mediator>();
            services.AddScoped<SingleInstanceFactory>(p => t => p.GetRequiredService(t));
            services.AddScoped<MultiInstanceFactory>(p => t => p.GetRequiredServices(t));
            services.AddMediatorHandlers();


            //AutoFac第三方DI
            //builder.RegisterModule(new AutofacModule());
            //builder.RegisterInstance(configuration);
            //builder.RegisterInstance(hostingEnvironment);
            //builder.Populate(services);

            //var container = builder.Build();
            //var serviceProvider = container.Resolve<IServiceProvider>();

        }

        private Action<IMvcBuilder>[] GetPrioritizedAddMvcActions()
        {
            List<KeyValuePair<int, Action<IMvcBuilder>>> addMvcActionsByPriorities = new List<KeyValuePair<int, Action<IMvcBuilder>>>();

            foreach (IModuleInitializer extension in ExtensionManager.Extensions)
                if (extension is IModuleInitializer)
                    if ((extension as IModuleInitializer).AddMvcActionsByPriorities != null)
                        addMvcActionsByPriorities.AddRange((extension as IModuleInitializer).AddMvcActionsByPriorities);

            return this.GetPrioritizedActions(addMvcActionsByPriorities);
        }

        #endregion

        #region MyRegion IApplicationBuilder


        /// <summary>
        /// 配置多语言信息
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        private void UseCustomizedRequestLocalization(IApplicationBuilder app)
        {
            var supportedCultures = new[]
            {
                new CultureInfo("zh"),
                new CultureInfo("en"),
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en", "en"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

        }
        /// <summary>
        /// 配置静态文件
        /// </summary>
        /// <param name="applicationBuilder"></param>
        private void UseCustomStaticFiles(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = (context) =>
                {
                    var headers = context.Context.Response.GetTypedHeaders();
                    headers.CacheControl = new CacheControlHeaderValue()
                    {
                        Public = true,
                        MaxAge = TimeSpan.FromDays(60)
                    };
                }
            });



            //模块的静态文件
            foreach (var module in ExtensionManager.Modules)
            {
                var wwwrootDir = new DirectoryInfo(Path.Combine(module.Path, "wwwroot"));
                if (!wwwrootDir.Exists)
                {
                    continue;
                }

                applicationBuilder.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(wwwrootDir.FullName),
                    //默认模块.分隔符最后的名称 如（SF.Module.Web）/Core
                    RequestPath = new PathString("/" + module.ShortName)
                });
            }
        }

        private void UseMvc(IApplicationBuilder applicationBuilder)
        {
            // 重要: session的注册必须在UseMvc之前，因为MVC里面要用 
            applicationBuilder.UseSession(new SessionOptions() { IdleTimeout = TimeSpan.FromMinutes(30) });
            applicationBuilder.UseIdentity();
            applicationBuilder.UseMultitenancy<SiteContext>();
            //EF Core二级缓存
            // applicationBuilder.UseEFSecondLevelCache();

            applicationBuilder.UseMiddleware<CurrentUserMiddleware>();
            applicationBuilder.UseMiddleware<CustomErrorPagesMiddleware>();
            // applicationBuilder.UseMiddleware<RequestLoggerMiddleware>();
            //  applicationBuilder.UseMiddleware<ProcessingTimeMiddleware>();
            //注册MVC请求
            applicationBuilder.UseMvc(
              routeBuilder =>
              {
                  routeBuilder.Routes.Add(new UrlSlugRoute(routeBuilder.DefaultHandler));
                  routeBuilder.MapRoute(
             "areaRoute",
             "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                  routeBuilder.MapRoute(
                 "default",
                 "{controller=Home}/{action=Index}/{id?}");

                  foreach (Action<IRouteBuilder> prioritizedUseMvcAction in this.GetPrioritizedUseMvcActions())
                  {
                      this.logger.LogInformation("Executing prioritized UseMvc action '{0}' of {1}", this.GetActionMethodInfo(prioritizedUseMvcAction));
                      prioritizedUseMvcAction(routeBuilder);
                  }
              }
            );

            applicationBuilder.UseCors(builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials();
            });



            //多租户

            //applicationBuilder.UsePerTenant<SiteContext>((ctx, builder) =>
            //{
            //    // custom 404 and error page - this preserves the status code (ie 404)
            //    if (string.IsNullOrEmpty(ctx.Tenant.SiteFolderName))
            //    {
            //        builder.UseStatusCodePagesWithReExecute("/Home/Error/{0}");
            //    }
            //    else
            //    {
            //        builder.UseStatusCodePagesWithReExecute("/" + ctx.Tenant.SiteFolderName + "/Home/Error/{0}");
            //    }

            //    // todo how to make this multi tenant for folders?
            //    // https://github.com/IdentityServer/IdentityServer4/issues/19
            //    //https://github.com/IdentityServer/IdentityServer4/blob/dev/src/IdentityServer4/Configuration/IdentityServerApplicationBuilderExtensions.cs
            //    //https://github.com/IdentityServer/IdentityServer4/blob/dev/src/IdentityServer4/Hosting/IdentityServerMiddleware.cs
            //    // perhaps will need to plugin custom IEndpointRouter?
            //    if (storage == "mssql")
            //    {
            //        // with this uncommented it breaks folder tenants
            //        // builder.UseIdentityServer();

            //        // this sets up the authentication for apis within this endpoint
            //        // ie apis that are hosted in the same web app endpoint with the authority server
            //        // this is not needed here if you are only using separate api endpoints
            //        // it is needed in the startup of those separate endpoints
            //        //applicationBuilder.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            //        //{
            //        //    Authority = "https://localhost:44399",
            //        //    // using the site aliasid as the scope so each tenant has a different scope
            //        //    // you can view the aliasid from site settings
            //        //    // clients must be configured with the scope to have access to the apis for the tenant
            //        //    //  ScopeName = ctx.Tenant.AliasId,

            //        //    RequireHttpsMetadata = true
            //        //});

            //    }
            //});
            #region Swagger
            //applicationBuilder.UseSwagger();
            //applicationBuilder.UseSwaggerUi();
            #endregion


        }

        /// <summary>
        /// 添加其他扩展应用构建
        /// </summary>
        /// <param name="services"></param>
        private void UserExtensionsBuilder(IApplicationBuilder applicationBuilder)
        {
            UseCustomizedHangfire(applicationBuilder);
            #region Securitys
            applicationBuilder.UseSecurity();
            #endregion

            #region Plugin
            //applicationBuilder.UseMvcWithPlugin();
            //applicationBuilder.AddPluginCustomizedMvc();
            #endregion
            applicationBuilder.AddmoduleCustomizedMvc(configurationRoot, hostingEnvironment);

        }

        /// <summary>
        /// Hangfire配置
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private void UseCustomizedHangfire(IApplicationBuilder app)
        {
            //   Hangfire.GlobalConfiguration.Configuration.UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection"));
            //app.UseHangfireDashboard();
            //app.UseHangfireServer();
            //

        }
        private Action<IRouteBuilder>[] GetPrioritizedUseMvcActions()
        {
            List<KeyValuePair<int, Action<IRouteBuilder>>> useMvcActionsByPriorities = new List<KeyValuePair<int, Action<IRouteBuilder>>>();

            foreach (IModuleInitializer extension in ExtensionManager.Extensions)
                if (extension is IModuleInitializer)
                    if ((extension as IModuleInitializer).UseMvcActionsByPriorities != null)
                        useMvcActionsByPriorities.AddRange((extension as IModuleInitializer).UseMvcActionsByPriorities);

            return this.GetPrioritizedActions(useMvcActionsByPriorities);
        }

        private IFileProvider CreateCompositeFileProvider()
        {
            IFileProvider[] fileProviders = new IFileProvider[] {
        this.serviceProvider.GetService<IHostingEnvironment>().WebRootFileProvider
      };

            return new CompositeFileProvider(
              fileProviders.Concat(
                ExtensionManager.Assemblies.Select(a => new EmbeddedFileProvider(a, a.GetName().Name))
              )
            );
        }

        private Action<T>[] GetPrioritizedActions<T>(IEnumerable<KeyValuePair<int, Action<T>>> actionsByPriorities)
        {
            return actionsByPriorities
              .OrderBy(actionByPriority => actionByPriority.Key)
              .Select(actionByPriority => actionByPriority.Value)
              .ToArray();
        }

        private string[] GetActionMethodInfo<T>(Action<T> action)
        {
            MethodInfo methodInfo = action.GetMethodInfo();

            return new string[] { methodInfo.Name, methodInfo.DeclaringType.FullName };
        }
        #endregion
    }
}