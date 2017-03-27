/*******************************************************************************
* 命名空间: SF.Services.Site
*
* 功 能： N/A
* 类 名： ISiteQueries
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/13 14:06:51 疯狂蚂蚁 初版
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
    public interface ISiteQueries : IDisposable
    {
        Task<ISiteSettings> Fetch(long siteId, CancellationToken cancellationToken = default(CancellationToken));
        Task<ISiteSettings> Fetch(string hostName, CancellationToken cancellationToken = default(CancellationToken));
        Task<ISiteSettings> FetchByFolderName(string folderName, CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> AliasIdIsAvailable(
            long requestingSiteId,
            string aliasId,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> HostNameIsAvailable(
            long requestingSiteId,
            string hostName,
            CancellationToken cancellationToken = default(CancellationToken)
            );

        Task<int> GetCount(CancellationToken cancellationToken = default(CancellationToken));
        Task<int> CountOtherSites(long currentSitelong, CancellationToken cancellationToken = default(CancellationToken));
        Task<List<ISiteInfo>> GetList(CancellationToken cancellationToken = default(CancellationToken));
        Task<List<ISiteInfo>> GetPageOtherSites(
            long currentSiteId,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<List<ISiteHost>> GetSiteHosts(long siteId, CancellationToken cancellationToken = default(CancellationToken));
        Task<List<ISiteHost>> GetAllHosts(CancellationToken cancellationToken = default(CancellationToken));
        Task<ISiteHost> GetSiteHost(string hostName, CancellationToken cancellationToken = default(CancellationToken));
        Task<int> GetHostCount(CancellationToken cancellationToken = default(CancellationToken));
        Task<List<ISiteHost>> GetPageHosts(
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken = default(CancellationToken));

        Task<List<string>> GetAllSiteFolders(CancellationToken cancellationToken = default(CancellationToken));

    }
}
