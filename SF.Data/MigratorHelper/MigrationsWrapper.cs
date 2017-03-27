using Dapper;
using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SF.Data.MigratorHelper
{
    /// <summary>
    /// 迁移
    /// </summary>
    public class MigrationsWrapper
    {
        private Assembly migrationsAssembly;

        private string connectionString;
        private Action<string> logMethod = Console.WriteLine;
        /// <summary>
        /// 迁移
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="migrationsAssembly">指定迁移文件所在的程序集</param>
        /// <param name="logMethod">日志方法</param>
        public MigrationsWrapper(string connectionString, Assembly migrationsAssembly, Action<string> logMethod = null)
        {
            this.connectionString = connectionString;
            this.migrationsAssembly = migrationsAssembly;
            if (logMethod != null)
            {
                this.logMethod = logMethod;
            }
        }


        /// <summary>
        /// 默认迁移数据库中最后的版本
        /// </summary>
        public void MigrateToLatestVersion()
        {
            var runner = GetMigrator();
            runner.MigrateUp(LatestVersionNumber);
        }
        /// <summary>
        /// 迁移指定版本
        /// </summary>
        /// <param name="version"></param>
        public void MigrateToVersion(int version)
        {
            var runner = GetMigrator();

            long currentVersion = CurrentVersionNumber;

            if (currentVersion < version)
            {
                runner.MigrateUp(version);
            }
            else if (currentVersion > version)
            {
                runner.MigrateDown(version);
            }
        }
        /// <summary>
        /// 回滚指定版本
        /// </summary>
        /// <param name="version"></param>
        public void MigrateCallback(int version)
        {
            var runner = GetMigrator();
            runner.MigrateDown(version);
        }
        /// <summary>
        /// 获取迁移文件最后的版本
        /// </summary>
        public long LatestVersionNumber
        {
            get
            {
                long toReturn = 0;
                // Look through all types
                foreach (Type t in migrationsAssembly.GetTypes())
                {
                    // Get all the types with MigrationAttribute (object[] because it can have multiple Migration attributes)
                    var attributeList = new List<object>();
                    attributeList.AddRange(t.GetTypeInfo().GetCustomAttributes(typeof(MigrationAttribute)));
                    if (attributeList.Count > 0)
                    {
                        // Get the max of (current max, max version specified in this Type's Migration attributes)
                        toReturn = Math.Max(toReturn, attributeList.Max(o => (o as MigrationAttribute).Version));
                    }
                }

                return toReturn;
            }
        }
        /// <summary>
        /// 获取数据库当前的版本
        /// </summary>
        public long CurrentVersionNumber
        {
            get
            {
                long toReturn = 0;

                using (var conn = new SqlConnection(this.connectionString))
                {
                    conn.Open();

                    try
                    {
                        toReturn = conn.Query<long>("SELECT MAX(Version) FROM VersionInfo").First();
                    }
                    catch (SqlException)
                    {
                        toReturn = 0;
                    }
                }
                return toReturn;
            }
        }

        private MigrationRunner GetMigrator()
        {
            var announcer = new TextWriterAnnouncer(s => logMethod.Invoke(s));

            var migrationContext = new RunnerContext(announcer);
            var options = new MigrationOptions { PreviewOnly = false, Timeout = 60 };
            var factory = new FluentMigrator.Runner.Processors.SqlServer.SqlServer2008ProcessorFactory();
            var processor = factory.Create(this.connectionString, announcer, options);
            var runner = new MigrationRunner(migrationsAssembly, migrationContext, processor);

            return runner;
        }
        private class MigrationOptions : IMigrationProcessorOptions
        {
            public bool PreviewOnly { get; set; }
            public string ProviderSwitches { get; set; }
            public int Timeout { get; set; }
        }
    }
}
