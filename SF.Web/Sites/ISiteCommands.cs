/*******************************************************************************
* 命名空间: SF.Services.Site
*
* 功 能： N/A
* 类 名： ISiteCommands
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/13 14:06:10 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using SF.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SF.Web.Site
{
    public interface ISiteCommands : IDisposable
    {
        Task Create(ISiteSettings site, CancellationToken cancellationToken = default(CancellationToken));
        Task Update(ISiteSettings site, CancellationToken cancellationToken = default(CancellationToken));
        Task Delete(long siteId, CancellationToken cancellationToken = default(CancellationToken));
        Task AddHost(long siteId, string hostName, CancellationToken cancellationToken = default(CancellationToken));
        Task DeleteHost(long siteId, long hostId, CancellationToken cancellationToken = default(CancellationToken));
        Task DeleteHostsBySite(long siteId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
