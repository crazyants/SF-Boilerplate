using Microsoft.AspNetCore.Hosting;
using SF.Data.MigratorHelper;
using SF.TestBase.Tests;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using Xunit;

namespace SF.Data.Test
{
    public class MigratorUnitTest : AppTestBase
    {
        private string ConnectionString = "Server=.;Database=SF_Team_Blog;uid=sa;pwd=123.com.cn;Pooling=True;Min Pool Size=1;Max Pool Size=100;Trusted_Connection=True;MultipleActiveResultSets=true;Integrated Security=false;";
        private Assembly migrationAssembly;
        public MigratorUnitTest()
        {

            migrationAssembly = GetAssembly("SF.TestBase");
        }
        [Fact]
        public void Test1()
        {
            new MigrationsWrapper(ConnectionString, migrationAssembly).MigrateToLatestVersion();
        }

       
    }


}
