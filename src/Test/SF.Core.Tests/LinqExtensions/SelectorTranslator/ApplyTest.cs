/*******************************************************************************
* 命名空间: SF.Core.Tests.LinqExtensions.SelectorTranslator
*
* 功 能： N/A
* 类 名： ApplyTest
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/21 9:53:41 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using SF.Core.QueryExtensions.Extensions;
using SF.Core.Tests.LinqExtensions.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace SF.Core.Tests.LinqExtensions.SelectorTranslator
{
    public class ApplyTest
    {
        readonly IQueryable<IDummy> data = DummyStore.Data.AsQueryable();

        [Fact]
        public void ShouldHandleInvalidArguments()
        {
            Expression<Func<Dummy, DummyView>> s = _ => new DummyView { Id = 1 };
            Expression<Func<Dummy, DummyView>> t = null;

            Assert.Throws<ArgumentNullException>(() => s.Apply(t));
            Assert.Throws<ArgumentNullException>(() => t.Apply(s));

            t = _ => null;

            Assert.Throws<NotSupportedException>(() => s.Apply(t));
            Assert.Throws<NotSupportedException>(() => t.Apply(s));

            t = _ => new DummyView(1) { Name = "Narf" };

            Assert.Throws<NotSupportedException>(() => s.Apply(t));
            Assert.Throws<NotSupportedException>(() => t.Apply(s));
        }

        [Fact]
        public void ShouldMergeSelectors()
        {
            Expression<Func<Dummy, DummyView>> s = d => new DummyView { Id = d.Id };
            Expression<Func<Dummy, DummyView>> t = d => new DummyView { Name = d.Name };

            var select = s.Apply(t);
            var result = data.OfType<Dummy>().Except(data.OfType<SuperDummy>()).Select(select);

            Assert.Collection(result,
                v => { Assert.Equal(1, v.Id); Assert.Equal("Asdf", v.Name); },
                v => { Assert.Equal(2, v.Id); Assert.Equal("Narf", v.Name); },
                v => { Assert.Equal(3, v.Id); Assert.Equal("Qwer", v.Name); });
        }
    }
}
