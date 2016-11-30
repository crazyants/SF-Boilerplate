/*******************************************************************************
* 命名空间: SF.Core.Tests.LinqExtensions.Data
*
* 功 能： N/A
* 类 名： DummyStore
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/19 15:23:58 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Core.Tests.LinqExtensions.Data
{
    public static class DummyStore
    {
        public static IReadOnlyList<IDummy> Data { get; } = InitData();

        static IReadOnlyList<IDummy> InitData()
        {
            var d = new[]
            {
                new Dummy { Id = 1, Name = "Asdf", Property1="Asdf",Property2="Asdf",Property3="Asdf" },
                new Dummy { Id = 2, Name = "Narf", Property1="Narf",Property2="Narf",Property3="Narf" },
                new Dummy { Id = 3, Name = "Qwer", Property1="Qwer",Property2="Qwer",Property3="Qwer" }
            };

            var s = new[]
            {
                new SuperDummy { Id = 4, Name = "Asdf",Property1="Asdf",Property2="Asdf",Property3="Asdf" },
                new SuperDummy { Id = 5, Name = "Narf",Property1="Narf",Property2="Narf",Property3="Narf" },
                new SuperDummy { Id = 6, Name = "Qwer",Property1="Qwer",Property2="Qwer",Property3="Qwer" }
            };

            var p = new[]
            {
                new ParentDummy { Id = 7, Name = "Asdf",Property1="Asdf",Property2="Asdf",Property3="Asdf" },
                new ParentDummy { Id = 8, Name = "Narf",Property1="Narf",Property2="Narf",Property3="Narf" },
                new ParentDummy { Id = 9, Name = "Qwer",Property1="Qwer",Property2="Qwer",Property3="Qwer" }
            };

            var c = new[]
            {
                new ChildDummy { Id = 10, Name = "Asdf", Parent = p[1],Property1="Asdf",Property2="Asdf",Property3="Asdf" },
                new ChildDummy { Id = 11, Name = "Narf", Parent = p[2],Property1="Narf",Property2="Narf",Property3="Narf" },
                new ChildDummy { Id = 12, Name = "Qwer", Parent = p[0],Property1="Qwer",Property2="Qwer",Property3="Qwer" }
            };

            p[0].Children = new[] { c[0], c[1] };
            p[1].Children = new[] { c[1], c[2] };
            p[2].Children = new[] { c[0], c[2] };

            return d.Concat<IDummy>(s).Concat(p).Concat(c).ToList();
        }
    }
}
