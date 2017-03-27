using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace SF.WebHost
{
    public class Program
    {
        /// <summary>
        /// 注意：.Net Core 默认创建的项目部署完成以后，只能在本机内访问，外部通过IP是打不开的
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("config/hosting.json", optional: true)
               .Build();
            var host = new WebHostBuilder()
             .UseKestrel()
             .UseUrls("http://*:5000")//即可实现通过IP访问程序。
             .UseConfiguration(config)
             .UseContentRoot(Directory.GetCurrentDirectory())
             .UseIISIntegration()
             .UseApplicationInsights()
             .UseStartup<Startup>()
             .Build();

            host.Run();
        }
    }
}
