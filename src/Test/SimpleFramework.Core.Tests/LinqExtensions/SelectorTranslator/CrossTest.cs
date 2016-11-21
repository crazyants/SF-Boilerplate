/*******************************************************************************
* 命名空间: SimpleFramework.Core.Tests.LinqExtensions.SelectorTranslator
*
* 功 能： N/A
* 类 名： CrossTest
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/21 9:56:49 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using SimpleFramework.Core.Data.Extensions;
using SimpleFramework.Core.Tests.LinqExtensions.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace SimpleFramework.Core.Tests.LinqExtensions.SelectorTranslator
{
    public class CrossTest
    {
        readonly IQueryable<IDummy> data = DummyStore.Data.AsQueryable();

        [Fact]
        public void SubtypeShouldSubstitute()
        {
            Expression<Func<Dummy, DummyView>> s = d => new DummyView { Id = d.Id, Name = d.Name };
            Expression<Func<SuperDummy, SuperDummyView>> t = d => new SuperDummyView { Description = d.Description };

            var select = s.Translate().Cross<SuperDummy>().Apply(t);
            var result = data.OfType<SuperDummy>().Select(select);

            Assert.Collection(result,
                v => { Assert.Equal(4, v.Id); Assert.Equal("Asdf", v.Name); Assert.Equal("Asdf", v.Description); },
                v => { Assert.Equal(5, v.Id); Assert.Equal("Narf", v.Name); Assert.Equal("Narf", v.Description); },
                v => { Assert.Equal(6, v.Id); Assert.Equal("Qwer", v.Name); Assert.Equal("Qwer", v.Description); });
        }

        [Fact]
        public void PathShouldSubstitute()
        {
            Expression<Func<ParentDummy, ParentDummyView>> s = d => new ParentDummyView { Id = d.Id, Name = d.Name };
            Expression<Func<ChildDummy, ChildDummyView>> t = d => new ChildDummyView { Id = d.Id, Name = d.Name };

            var select = s.Translate().Cross<ChildDummy>(d => d.Parent).Apply(d => d.Parent, t);
            var result = data.OfType<ChildDummy>().Select(select);

            Assert.Collection(result,
                v => { Assert.Equal(10, v.Id); Assert.Equal("Asdf", v.Name); Assert.Equal(8, v.Parent.Id); Assert.Equal("Narf", v.Parent.Name); },
                v => { Assert.Equal(11, v.Id); Assert.Equal("Narf", v.Name); Assert.Equal(9, v.Parent.Id); Assert.Equal("Qwer", v.Parent.Name); },
                v => { Assert.Equal(12, v.Id); Assert.Equal("Qwer", v.Name); Assert.Equal(7, v.Parent.Id); Assert.Equal("Asdf", v.Parent.Name); });
        }

        [Fact]
        public void TranslationShouldSubstitute()
        {
            Expression<Func<ChildDummy, ChildDummyView>> s = d => new ChildDummyView { Id = d.Id, Name = d.Name };
            Expression<Func<ParentDummy, ParentDummyView>> t = d => new ParentDummyView { Id = d.Id, Name = d.Name };

            var select = s.Translate().Cross<ParentDummy>((d, v) => d.Children.Select(v).First()).Apply((d, v) => new ParentDummyView { FirstChild = v(d) }, t);
            var result = data.OfType<ParentDummy>().Select(select);

            Assert.Collection(result,
                v => { Assert.Equal(7, v.Id); Assert.Equal("Asdf", v.Name); Assert.Equal(10, v.FirstChild.Id); Assert.Equal("Asdf", v.FirstChild.Name); },
                v => { Assert.Equal(8, v.Id); Assert.Equal("Narf", v.Name); Assert.Equal(11, v.FirstChild.Id); Assert.Equal("Narf", v.FirstChild.Name); },
                v => { Assert.Equal(9, v.Id); Assert.Equal("Qwer", v.Name); Assert.Equal(12, v.FirstChild.Id); Assert.Equal("Qwer", v.FirstChild.Name); });
        }

        [Fact]
        public void TranslationCollectionShouldSubstitute()
        {
            Expression<Func<ChildDummy, ChildDummyView>> s = d => new ChildDummyView { Id = d.Id, Name = d.Name };
            Expression<Func<ParentDummy, ParentDummyView>> t = d => new ParentDummyView { Id = d.Id, Name = d.Name };

            var select = s.Translate().Cross<ParentDummy>((d, v) => d.Children.Select(v)).Apply((d, v) => new ParentDummyView { FirstChild = v(d).First() }, t);
            var result = data.OfType<ParentDummy>().Select(select);

            Assert.Collection(result,
                v => { Assert.Equal(7, v.Id); Assert.Equal("Asdf", v.Name); Assert.Equal(10, v.FirstChild.Id); Assert.Equal("Asdf", v.FirstChild.Name); },
                v => { Assert.Equal(8, v.Id); Assert.Equal("Narf", v.Name); Assert.Equal(11, v.FirstChild.Id); Assert.Equal("Narf", v.FirstChild.Name); },
                v => { Assert.Equal(9, v.Id); Assert.Equal("Qwer", v.Name); Assert.Equal(12, v.FirstChild.Id); Assert.Equal("Qwer", v.FirstChild.Name); });
        }
    }
}
