using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace SF.Module.LoggingStorage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                //.UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
