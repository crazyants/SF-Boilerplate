/*******************************************************************************
* 命名空间: SimpleFramework.Core.Tests.LinqExtensions.DynamicQuery
*
* 功 能： N/A
* 类 名： OrderByUntypedTest
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/21 9:48:20 疯狂蚂蚁 初版
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

namespace SimpleFramework.Core.Tests.LinqExtensions.DynamicQuery
{
    public class OrderByUntypedTest
    {
        readonly IQueryable<IDummy> data = DummyStore.Data.AsQueryable();

        [Fact]
        public void ShouldHandleInvalidArguments()
        {
            Assert.Throws<ArgumentNullException>(() => default(IQueryable).OrderBy("Name.Length"));
            Assert.Throws<ArgumentNullException>(() => data.OrderBy(null));
            Assert.Throws<ArgumentNullException>(() => default(IOrderedQueryable).ThenBy("Name"));
            Assert.Throws<ArgumentNullException>(() => data.OrderBy("Name.Length").ThenBy(null));
        }
        [Fact]
        public void ShouldSortBySelector()
        {
            var one = data.OrderBy("Name.Length").ThenBy("Name", true);
            var two = data.OrderBy("Name.Length", true).ThenBy("Name");

            var oneResult = one.Cast<Dummy>().Select(d => d.Id).ToArray();
            var twoResult = two.Cast<Dummy>().Select(d => d.Id).ToArray();

            Assert.Equal(new[] { 9, 8, 7, 6, 5, 4, 3, 2, 1 }, oneResult);
            Assert.Equal(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, twoResult);
        }
    }
}
