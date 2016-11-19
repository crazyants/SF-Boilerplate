/*******************************************************************************
* 命名空间: SimpleFramework.Core.Tests.LinqExtensions.Data
*
* 功 能： N/A
* 类 名： DummyStore
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/19 15:23:58 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleFramework.Core.Tests.LinqExtensions.Data
{
    public static class DummyStore
    {
        public static IReadOnlyList<IDummy> Data { get; } = InitData();

        static IReadOnlyList<IDummy> InitData()
        {
            var d = new[]
            {
                new Dummy { Id = 1, Name = "Asdf" },
                new Dummy { Id = 2, Name = "Narf" },
                new Dummy { Id = 3, Name = "Qwer" }
            };

            var s = new[]
            {
                new SuperDummy { Id = 4, Name = "Asdf" },
                new SuperDummy { Id = 5, Name = "Narf" },
                new SuperDummy { Id = 6, Name = "Qwer" }
            };

            var p = new[]
            {
                new ParentDummy { Id = 7, Name = "Asdf" },
                new ParentDummy { Id = 8, Name = "Narf" },
                new ParentDummy { Id = 9, Name = "Qwer" }
            };

            var c = new[]
            {
                new ChildDummy { Id = 10, Name = "Asdf", Parent = p[1] },
                new ChildDummy { Id = 11, Name = "Narf", Parent = p[2] },
                new ChildDummy { Id = 12, Name = "Qwer", Parent = p[0] }
            };

            p[0].Children = new[] { c[0], c[1] };
            p[1].Children = new[] { c[1], c[2] };
            p[2].Children = new[] { c[0], c[2] };

            return d.Concat<IDummy>(s).Concat(p).Concat(c).ToList();
        }
    }
}
