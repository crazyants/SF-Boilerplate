/*******************************************************************************
* 命名空间: SF.Core.Abstraction.新文件夹
*
* 功 能： N/A
* 类 名： ITimeZoneIdResolver
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/15 9:43:01 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SF.Core.Abstraction.Resolvers
{
    public interface ITimeZoneIdResolver
    {
        Task<string> GetUserTimeZoneId(CancellationToken cancellationToken = default(CancellationToken));
        Task<string> GetSiteTimeZoneId(CancellationToken cancellationToken = default(CancellationToken));
    }
}
