/*******************************************************************************
* 命名空间: SF.Core.Tests.LinqExtensions.DynamicQuery
*
* 功 能： N/A
* 类 名： OrderByTypedTest
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/21 9:47:10 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using SF.Core.QueryExtensions.Extensions;
using SF.Core.Tests.LinqExtensions.Data;
using System;
using System.Linq;
using Xunit;

namespace SF.Core.Tests.LinqExtensions.DynamicQuery
{
    public class OrderByTypedTest
    {
        readonly IQueryable<IDummy> data = DummyStore.Data.AsQueryable();
        [Fact]
        public void ShouldHandleInvalidArguments()
        {
            Assert.Throws<ArgumentNullException>(() => default(IQueryable<Dummy>).OrderBy("Name.Length"));
            Assert.Throws<ArgumentNullException>(() => data.OrderBy(null));
            Assert.Throws<ArgumentNullException>(() => default(IOrderedQueryable<Dummy>).ThenBy("Name"));
            Assert.Throws<ArgumentNullException>(() => data.OrderBy("Name.Length").ThenBy(null));
        }
        [Fact]
        public void ShouldSortBySelector()
        {
            var one = data.OrderBy("Name.Length").ThenBy("Name", true);
            var two = data.OrderBy("Name.Length", true).ThenBy("Name");

            var oneResult = one.Select(d => d.Id).ToArray();
            var twoResult = two.Select(d => d.Id).ToArray();

            Assert.Equal(new[] { 9, 8, 7, 6, 5, 4, 3, 2, 1 }, oneResult);
            Assert.Equal(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, twoResult);
        }
    }
}
