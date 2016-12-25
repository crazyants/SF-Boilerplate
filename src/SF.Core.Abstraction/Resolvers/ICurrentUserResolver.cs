/*******************************************************************************
* 命名空间: SF.Core.Abstraction.Resolvers
*
* 功 能： N/A
* 类 名： ICurrentUserResolver
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/19 9:48:46 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Core.Abstraction.Resolvers
{
 
    /// <summary>
    /// 获取当前用户名
    /// </summary>
    public interface ICurrentUserResolver
    {
        string UserName { get; set; }
    }
}
