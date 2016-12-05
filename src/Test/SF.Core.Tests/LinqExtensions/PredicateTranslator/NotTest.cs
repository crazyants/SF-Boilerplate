/*******************************************************************************
* 命名空间: SF.Core.Tests.LinqExtensions.PredicateTranslator
*
* 功 能： N/A
* 类 名： NotTest
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/19 15:33:21 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using SF.Core.QueryExtensions.Extensions;
using SF.Core.Tests.LinqExtensions.Data;
using System;
using System.Linq;
using System.Linq.Expressions;
using Xunit;


namespace SF.Core.Tests.LinqExtensions.PredicateTranslator
{
    public class NotTest
    {
        readonly IQueryable<IDummy> data = DummyStore.Data.AsQueryable();

        [Fact]
        public void ShouldHandleInvalidArguments()
        {
            Expression<Func<IDummy, bool>> p = null;

            Assert.Throws<ArgumentNullException>(() => p.Not());
        }

        [Fact]
        public void ShouldNegatePredicate()
        {
            Expression<Func<IDummy, bool>> p = d => d.Name == "Narf";

            var r = data.Where(p).Count();
            var s = data.Where(p.Not()).Count();

            Assert.Equal(4, r);
            Assert.Equal(8, s);
        }
    }
}
