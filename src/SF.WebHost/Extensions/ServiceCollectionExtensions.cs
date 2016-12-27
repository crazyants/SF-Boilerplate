using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CacheManager.Core;

namespace SF.WebHost.Extensions
{
    public static class ServiceCollectionExtensions
    {
    

        /// <summary>
        /// 配置审计日志
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="hostingEnvironment"></param>
        /// <returns></returns>
        public static IServiceCollection AddAuditStorageProviders(this IServiceCollection services,
            IConfigurationRoot configuration,
            IHostingEnvironment hostingEnvironment)
        {
            var auditStorage = configuration.GetValue<string>("AuditStorage");
            if (auditStorage == "SqlServer")
            {
                //必须手动创建数据库
                //https://github.com/thepirat000/Audit.NET/blob/master/src/Audit.NET.SqlServer/README.md
                Audit.Core.Configuration.DataProvider = new Audit.SqlServer.Providers.SqlDataProvider()
                {
                    ConnectionString = configuration.GetConnectionString("DefaultConnection"),
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
            return services;

        }

        //全局构建服务
        public static IServiceCollection Build(this IServiceCollection services,
            IConfigurationRoot configuration, IHostingEnvironment hostingEnvironment)
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


            return services;
        }




        /// <summary>
        /// AutoFac 注入模块
        /// </summary>
        //public class AutofacModule : Autofac.Module
        //{
        //    protected override void Load(ContainerBuilder builder)
        //    {

        //        //builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
        //        //builder.RegisterGeneric(typeof(RepositoryAsync<>)).As(typeof(IRepositoryAsync<>));
        //        //builder.RegisterGeneric(typeof(RepositoryWithTypedId<,>)).As(typeof(IRepositoryWithTypedId<,>));
        //        //// Sets the delegate resolver factories for Mediatr.
        //        //// These factories are used by Mediatr to find the appropriate Handlers
        //        //builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

        //        //builder.Register<SingleInstanceFactory>(ctx =>
        //        //{
        //        //    var c = ctx.Resolve<IComponentContext>();
        //        //    return t => c.Resolve(t);
        //        //});
        //}
        //        //builder.Register<MultiInstanceFactory>(ctx =>
        //        //{
        //        //    var c = ctx.Resolve<IComponentContext>();
        //        //    return t => (IEnumerable<object>)c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
        //        //});

        //        //foreach (var module in GlobalConfiguration.Modules)
        //        //{
        //        //    builder.RegisterAssemblyTypes(module.Assembly).AsImplementedInterfaces();
        //        //}

        //        //builder.RegisterType<CurrentUser>().As<ICurrentUser>();
        //        //builder.RegisterType<UserNameResolver>().As<IUserNameResolver>();
        //        //builder.Register<IUnitOfWorkAsync>(c =>
        //        //{
        //        //    var simpleDbContext = c.Resolve<CoreDbContext>();
        //        //    var userNameResolver = c.Resolve<IUserNameResolver>();
        //        //    return new UnitOfWork(simpleDbContext, new AuditableInterceptor(userNameResolver), new EntityPrimaryKeyGeneratorInterceptor());
        //        //});

        //    }



    }


}