/*******************************************************************************
* 命名空间: SimpleFramework.Core.Tests.LinqExtensions.Data
*
* 功 能： N/A
* 类 名： IDummy
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/19 15:25:34 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleFramework.Core.Tests.LinqExtensions.Data
{
    public interface IDummy
    {
        int Id { get; set; }

        string Name { get; set; }
    }
}
