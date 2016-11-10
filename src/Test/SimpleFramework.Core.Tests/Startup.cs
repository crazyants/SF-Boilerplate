/*******************************************************************************
* 命名空间: SimpleFramework.Core.Tests
*
* 功 能： N/A
* 类 名： Statup
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/10 11:09:52 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace SimpleFramework.Core.Tests
{
    public class Startup
    {
        [Fact]
        public void UseCentralRoutePrefixTest()
        {
            //IServiceCollection services=
            //services.AddMvc(opt =>
            //{
            //    opt.UseCentralRoutePrefix(new RouteAttribute("api/v{version}"));
            //});

            //services.AddMvc(opt =>
            //{
            //    opt.EnableTypedRouting();
            //    opt.Get("homepage", c => c.Action<HomeController>(x => x.Index()));
            //    opt.Get("aboutpage/{name}", c => c.Action<HomeController>(x => x.About(Param<string>.Any)));
            //    opt.Post("sendcontact", c => c.Action<HomeController>(x => x.Contact()));
            //});
        }

    }
}
