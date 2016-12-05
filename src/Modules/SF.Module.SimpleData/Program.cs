/*******************************************************************************
* 命名空间: SF.Module.SimpleData
*
* 功 能： N/A
* 类 名： Program
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/2 11:26:17 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace SF.Module.SimpleData
{
    public class Program
    {
        public static void Main(string[] args)
        {
          
            var host = new WebHostBuilder()
              .UseKestrel()
              // .UseConfiguration(config)
              .UseContentRoot(Directory.GetCurrentDirectory())
              .UseIISIntegration()
              .UseStartup<Startup>()
              .Build();

            host.Run();

        }
    }
}
