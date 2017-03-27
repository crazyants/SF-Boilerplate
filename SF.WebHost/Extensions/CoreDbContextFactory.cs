/*******************************************************************************
* 命名空间: SF.Core.Data
*
* 功 能： N/A
* 类 名： CoreDbContextFactory
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2017/1/5 10:05:35 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SF.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.WebHost
{
    //public class CoreDbContextFactory : IDbContextFactory<CoreDbContext>
    //{

    //    public CoreDbContext Create(DbContextFactoryOptions options)
    //    {

    //        var builder = new DbContextOptionsBuilder<CoreDbContext>();
    //        builder.UseSqlServer("Server=.;Database=SF_Team_2017_Dev;uid=sa;pwd=123.com.cn;Trusted_Connection=True;MultipleActiveResultSets=true;Integrated Security=false;"
    //             , b => b.MigrationsAssembly("SF.WebHost"));

    //        return new CoreDbContext(builder.Options);
    //    }

    //}

    //public class MyDbContextFactory : IDbContextFactory<CoreDbContext>
    //{
    //    public CoreDbContext Create()
    //    {
    //        var environmentName =
    //                    Environment.GetEnvironmentVariable(
    //                        "Hosting:Environment");

    //        var basePath = AppContext.BaseDirectory;

    //        return Create(basePath, environmentName);
    //    }

    //    public CoreDbContext Create(DbContextFactoryOptions options)
    //    {
    //        return Create(
    //            options.ContentRootPath,
    //            options.EnvironmentName);
    //    }

    //    private CoreDbContext Create(string basePath, string environmentName)
    //    {
    //        var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
    //            .SetBasePath(basePath)
    //            .AddJsonFile("appsettings.json")
    //            .AddJsonFile($"appsettings.{environmentName}.json", true)
    //            .AddEnvironmentVariables();

    //        var config = builder.Build();

    //        var connstr = "Server=.;Database=SF_Team_2017;uid=sa;pwd=123.com.cn;Pooling=True;Min Pool Size=1;Max Pool Size=100;Trusted_Connection=True;MultipleActiveResultSets=true;Integrated Security=false;";

    //        if (String.IsNullOrWhiteSpace(connstr) == true)
    //        {
    //            throw new InvalidOperationException(
    //                "Could not find a connection string named '(default)'.");
    //        }
    //        else
    //        {
    //            return Create(connstr);
    //        }
    //    }

    //    private CoreDbContext Create(string connectionString)
    //    {
    //        if (string.IsNullOrEmpty(connectionString))
    //            throw new ArgumentException(
    //                $"{nameof(connectionString)} is null or empty.",
    //                nameof(connectionString));

    //        var optionsBuilder =
    //            new DbContextOptionsBuilder<CoreDbContext>();

    //        optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("SF.WebHost"));

    //        return new CoreDbContext(optionsBuilder.Options);
    //    }
    //}
}
