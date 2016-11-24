/*******************************************************************************
* 命名空间: SF.Core.Tests.LinqExtensions.PredicateTranslator
*
* 功 能： N/A
* 类 名： AndTest
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/19 15:27:01 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using SF.Core.Data.Extensions;
using SF.Core.Tests.LinqExtensions.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace SF.Core.Tests.LinqExtensions.PredicateTranslator
{
    public class AndTest
    {
        readonly IQueryable<IDummy> data = DummyStore.Data.AsQueryable();

        [Fact]
        public void ShouldHandleInvalidArguments()
        {
            Expression<Func<IDummy, bool>> p = _ => false;
            Expression<Func<IDummy, bool>> q = null;

            Assert.Throws<ArgumentNullException>(() => p.And(q));
            Assert.Throws<ArgumentNullException>(() => q.And(p));
        }

        [Fact]
        public void ShouldCombinePredicates()
        {
            Expression<Func<IDummy, bool>> p = d => d.Id % 2 == 1;
            Expression<Func<IDummy, bool>> q = d => d.Name == "Narf";

            var r = data.Where(p).Count();
            var s = data.Where(q).Count();
            var t = data.Where(p.And(q)).Count();

            Assert.Equal(6, r);
            Assert.Equal(4, s);
            Assert.Equal(2, t);
        }
    }
}
