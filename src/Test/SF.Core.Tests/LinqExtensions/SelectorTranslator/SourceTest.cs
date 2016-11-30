/*******************************************************************************
* 命名空间: SF.Core.Tests.LinqExtensions.SelectorTranslator
*
* 功 能： N/A
* 类 名： SourceTest
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/21 10:08:50 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using SF.Core.QueryExtensions.Extensions;
using SF.Core.Tests.LinqExtensions.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace SF.Core.Tests.LinqExtensions.SelectorTranslator
{
    public class SourceTest
    {
        readonly IQueryable<IDummy> data = DummyStore.Data.AsQueryable();

        [Fact]
        public void SubtypeShouldSubstitute()
        {
            Expression<Func<Dummy, DummyView>> s = d => new DummyView { Id = d.Id, Name = d.Name };

            var select = s.Translate().Source<SuperDummy>();
            var result = data.OfType<SuperDummy>().Select(select);

            Assert.Collection(result,
                v => { Assert.Equal(4, v.Id); Assert.Equal("Asdf", v.Name); },
                v => { Assert.Equal(5, v.Id); Assert.Equal("Narf", v.Name); },
                v => { Assert.Equal(6, v.Id); Assert.Equal("Qwer", v.Name); });
        }

        [Fact]
        public void PathShouldShouldHandleInvalidArguments()
        {
            Expression<Func<ParentDummy, ParentDummyView>> s = _ => new ParentDummyView();

            Assert.Throws<ArgumentNullException>(() => s.Translate().Source(default(Expression<Func<ChildDummy, ParentDummy>>)));
        }

        [Fact]
        public void PathShouldSubstitute()
        {
            Expression<Func<ParentDummy, ParentDummyView>> s = d => new ParentDummyView { Id = d.Id, Name = d.Name };

            var select = s.Translate().Source<ChildDummy>(d => d.Parent);
            var result = data.OfType<ChildDummy>().Select(select);

            Assert.Collection(result,
                v => { Assert.Equal(8, v.Id); Assert.Equal("Narf", v.Name); },
                v => { Assert.Equal(9, v.Id); Assert.Equal("Qwer", v.Name); },
                v => { Assert.Equal(7, v.Id); Assert.Equal("Asdf", v.Name); });
        }

        [Fact]
        public void TranslationShouldHandleInvalidArguments()
        {
            Expression<Func<ChildDummy, ChildDummyView>> s = _ => new ChildDummyView();

            Assert.Throws<ArgumentNullException>(() => s.Translate().Source(default(Expression<Func<ParentDummy, Func<ChildDummy, ChildDummyView>, ChildDummyView>>)));
        }

        [Fact]
        public void TranslationShouldSubstitute()
        {
            Expression<Func<ChildDummy, ChildDummyView>> s = d => new ChildDummyView { Id = d.Id, Name = d.Name };

            var select = s.Translate().Source<ParentDummy>((d, v) => d.Children.Select(v).First());
            var result = data.OfType<ParentDummy>().Select(select);

            Assert.Collection(result,
                v => { Assert.Equal(10, v.Id); Assert.Equal("Asdf", v.Name); },
                v => { Assert.Equal(11, v.Id); Assert.Equal("Narf", v.Name); },
                v => { Assert.Equal(12, v.Id); Assert.Equal("Qwer", v.Name); });
        }

        [Fact]
        public void TranslationCollectionShouldHandleInvalidArguments()
        {
            Expression<Func<ChildDummy, ChildDummyView>> s = _ => new ChildDummyView();

            Assert.Throws<ArgumentNullException>(() => s.Translate().Source(default(Expression<Func<ParentDummy, Func<ChildDummy, ChildDummyView>, IEnumerable<ChildDummyView>>>)));
        }

        [Fact]
        public void TranslationCollectionShouldSubstitute()
        {
            Expression<Func<ChildDummy, ChildDummyView>> s = d => new ChildDummyView { Id = d.Id, Name = d.Name };

            var select = s.Translate().Source<ParentDummy>((d, v) => d.Children.Select(v));
            var result = data.OfType<ParentDummy>().Select(select);

            Assert.Collection(result,
                v => Assert.Collection(v,
                    w => { Assert.Equal(10, w.Id); Assert.Equal("Asdf", w.Name); },
                    w => { Assert.Equal(11, w.Id); Assert.Equal("Narf", w.Name); }),
                v => Assert.Collection(v,
                    w => { Assert.Equal(11, w.Id); Assert.Equal("Narf", w.Name); },
                    w => { Assert.Equal(12, w.Id); Assert.Equal("Qwer", w.Name); }),
                v => Assert.Collection(v,
                    w => { Assert.Equal(12, w.Id); Assert.Equal("Qwer", w.Name); },
                    w => { Assert.Equal(10, w.Id); Assert.Equal("Asdf", w.Name); }));
        }
    }
}
