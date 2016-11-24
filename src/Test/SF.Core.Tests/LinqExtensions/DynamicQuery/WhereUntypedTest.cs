/*******************************************************************************
* 命名空间: SF.Core.Tests.LinqExtensions.DynamicQuery
*
* 功 能： N/A
* 类 名： WhereUntypedTest
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/21 9:50:24 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using SF.Core.Data.Extensions;
using SF.Core.Tests.LinqExtensions.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace SF.Core.Tests.LinqExtensions.DynamicQuery
{
    public class WhereUntypedTest
    {
        readonly IQueryable<IDummy> data = DummyStore.Data.AsQueryable();
        [Fact]
        public void ShouldHandleInvalidArguments()
        {
            Assert.Throws<ArgumentNullException>(() => default(IQueryable).Where("Number", DynamicCompare.Equal, null));
            Assert.Throws<ArgumentNullException>(() => data.Where(null, DynamicCompare.Equal, null));
            Assert.Throws<ArgumentOutOfRangeException>(() => data.Where("Number", (DynamicCompare)(object)-1, null));
            Assert.Throws<ArgumentNullException>(() => default(IQueryable).Where("Name", "Contains", "b"));
            Assert.Throws<ArgumentNullException>(() => data.Where(null, "Contains", "b"));
            Assert.Throws<ArgumentNullException>(() => data.Where("Name", null, "b"));
        }
        [Theory]
        [InlineData(DynamicCompare.Equal, new[] { 5 })]
        [InlineData(DynamicCompare.NotEqual, new[] { 1, 2, 3, 4, 6, 7, 8, 9 })]
        [InlineData(DynamicCompare.GreaterThan, new[] { 6, 7, 8, 9 })]
        [InlineData(DynamicCompare.GreaterThanOrEqual, new[] { 5, 6, 7, 8, 9 })]
        [InlineData(DynamicCompare.LessThan, new[] { 1, 2, 3, 4 })]
        [InlineData(DynamicCompare.LessThanOrEqual, new[] { 1, 2, 3, 4, 5 })]
        public void ShouldFilterByComparison(DynamicCompare comparison, int[] result)
        {
            var value = (222.222m).ToString(CultureInfo.CurrentCulture);

            var empty = data.Where("Number", comparison, null);
            var compare = data.Where("Number", comparison, value);

            var emptyResult = empty.Cast<Dummy>().Select(d => d.Id).ToArray();
            var compareResult = compare.Cast<Dummy>().Select(d => d.Id).ToArray();

            var count = comparison == DynamicCompare.NotEqual ? 9 : 0;

            Assert.Equal(count, emptyResult.Length);
            Assert.Equal(result, compareResult);
        }

        [Fact]
        public void ShouldFilterByCustomComparison()
        {
            var contains = data.Where("Name", "Contains", "b");

            var containsResult = contains.Cast<Dummy>().Select(d => d.Id).ToArray();

            Assert.Equal(new[] { 2, 5, 8 }, containsResult);
        }
    }
}
