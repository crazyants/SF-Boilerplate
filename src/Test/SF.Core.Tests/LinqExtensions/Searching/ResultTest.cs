/*******************************************************************************
* 命名空间: SF.Core.Tests.LinqExtensions.SelectorTranslator
*
* 功 能： N/A
* 类 名： ResultTest
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/21 10:06:38 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using SF.Core.QueryExtensions;
using SF.Core.QueryExtensions.Extensions;
using SF.Core.QueryExtensions.SearchExtensions;
using SF.Core.Tests.LinqExtensions.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace SF.Core.Tests.LinqExtensions.Searching
{
    public class ResultTest
    {
        readonly IQueryable<IDummy> queryableData = DummyStore.Data.AsQueryable();

        [Fact]
        public void SingleSearchTermSingleProperty()
        {
           
            var result = queryableData.Search(x => x.Name)
                             .Containing("Asdf");
            Assert.Equal(4, result.Count());
        }
        [Fact]
        public void SingleSearchTermMultipleProperty()
        {

            var result = queryableData.Search(x => x.Property1,
                                    x => x.Property2,
                                    x => x.Property3)
                            .Containing("Asdf");
            Assert.Equal(4, result.Count());
        }
        [Fact]
        public void MultipleSearchTermSingleProperty()
        {

            var result = queryableData.Search(x => x.Property1)
                            .Containing("Asdf", "term");
            Assert.Equal(4, result.Count());
           
        }
    }
}
