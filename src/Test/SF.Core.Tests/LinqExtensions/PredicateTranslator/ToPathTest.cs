/*******************************************************************************
* 命名空间: SF.Core.Tests.LinqExtensions.PredicateTranslator
*
* 功 能： N/A
* 类 名： ToPathTest
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/19 15:34:47 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using SF.Core.Data.Extensions;
using SF.Core.Tests.LinqExtensions.Data;
using System;
using System.Linq;
using System.Linq.Expressions;
using Xunit;


namespace SF.Core.Tests.LinqExtensions.PredicateTranslator
{
    public class ToPathTest
    {
        readonly IQueryable<IDummy> data = DummyStore.Data.AsQueryable();

        [Fact]
        public void ShouldHandleInvalidArguments()
        {
            Expression<Func<ParentDummy, bool>> p = _ => false;

            Assert.Throws<ArgumentNullException>(() => p.Translate().To(default(Expression<Func<ChildDummy, ParentDummy>>)));
        }

        [Fact]
        public void ShouldSubstitute()
        {
            Expression<Func<ParentDummy, bool>> p = d => d.Name == "Narf";

            var r = data.OfType<ParentDummy>().Where(p).Count();
            var s = data.OfType<ChildDummy>().Where(p.Translate().To<ChildDummy>(c => c.Parent)).Count();

            Assert.Equal(1, r);
            Assert.Equal(1, s);
        }
    }
}
