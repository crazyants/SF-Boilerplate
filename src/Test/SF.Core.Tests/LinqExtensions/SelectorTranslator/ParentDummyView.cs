/*******************************************************************************
* 命名空间: SF.Core.Tests.LinqExtensions.SelectorTranslator
*
* 功 能： N/A
* 类 名： ParentDummyView
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/21 9:55:41 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Core.Tests.LinqExtensions.SelectorTranslator
{
    public class ParentDummyView
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ChildDummyView FirstChild { get; set; }

        public IEnumerable<ChildDummyView> Children { get; set; }
    }
}
