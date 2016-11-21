/*******************************************************************************
* 命名空间: SimpleFramework.Core.Tests.LinqExtensions.SelectorTranslator
*
* 功 能： N/A
* 类 名： ChildDummyView
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/21 9:55:28 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleFramework.Core.Tests.LinqExtensions.SelectorTranslator
{
    public class ChildDummyView
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ParentDummyView Parent { get; set; }
    }
}
