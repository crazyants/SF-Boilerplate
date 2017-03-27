/*******************************************************************************
* 命名空间: SF.Web.Components
*
* 功 能： N/A
* 类 名： SiteManager
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/13 16:19:53 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SF.Entitys;
using SF.Web.Site;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SF.Web.Components
{
    public class SiteManager
    {

        public SiteManager(
            SiteContext currentSite,
            ISiteCommands siteCommands,
            ISiteQueries siteQueries,
            SiteDataProtector dataProtector,
            IHttpContextAccessor contextAccessor,
            ILogger<SiteManager> logger,
            IOptions<MultiTenantOptions> multiTenantOptionsAccessor,
            IOptions<SiteConfigOptions> setupOptionsAccessor,
                        CacheHelper cacheHelper
            )
        {

            commands = siteCommands;
            queries = siteQueries;
            multiTenantOptions = multiTenantOptionsAccessor.Value;
            setupOptions = setupOptionsAccessor.Value;
            _context = contextAccessor?.HttpContext;
            this.dataProtector = dataProtector;
            this.cacheHelper = cacheHelper;
            log = logger;
            siteSettings = currentSite;
        }

        private readonly HttpContext _context;
        private CancellationToken CancellationToken => _context?.RequestAborted ?? CancellationToken.None;
        private ILogger log;
        private SiteDataProtector dataProtector;
        private CacheHelper cacheHelper;
        private MultiTenantOptions multiTenantOptions;
        private SiteConfigOptions setupOptions;
        private ISiteCommands commands;
        private ISiteQueries queries;
        private ISiteContext siteSettings = null;
        public ISiteContext CurrentSite
        {
            get { return siteSettings; }
        }

        public async Task<ISiteSettings> GetCurrentSiteSettings()
        {
            return await queries.Fetch(CurrentSite.Id, CancellationToken);
        }

        public async Task<ISiteContext> GetSiteForDataOperations(long? siteId, bool useRelatedSiteId = false)
        {
            if (multiTenantOptions.UseRelatedSitesMode)
            {
                if (useRelatedSiteId)
                {
                    return await Fetch(multiTenantOptions.RelatedSiteId) as ISiteContext;
                }
            }

            if ((siteId.HasValue) && (siteId.Value != 0)
                && (siteId.Value != CurrentSite.Id)
                && (CurrentSite.IsServerAdminSite))
            {
                return await Fetch(siteId.Value) as ISiteContext;
            }

            return CurrentSite;
        }

        public async Task<ISiteSettings> GetSiteForEdit(long? siteId)
        {
            if ((siteId.HasValue) && (siteId.Value != 0)
                && (siteId.Value != CurrentSite.Id)
                && (CurrentSite.IsServerAdminSite))
            {
                return await Fetch(siteId.Value);
            }

            return await Fetch(CurrentSite.Id);
        }

        public Task<List<ISiteInfo>> GetPageOtherSites(
            long currentSiteId,
            int pageNumber,
            int pageSize)
        {
            return queries.GetPageOtherSites(currentSiteId, pageNumber, pageSize, CancellationToken);

        }

        public Task<int> CountOtherSites(long currentSiteId)
        {
            return queries.CountOtherSites(currentSiteId, CancellationToken);
        }

        public async Task<ISiteSettings> Fetch(long siteId)
        {
            var site = await queries.Fetch(siteId, CancellationToken);
            dataProtector.UnProtect(site);
            return site;
        }

        public async Task<ISiteSettings> Fetch(string hostname)
        {
            var site = await queries.Fetch(hostname, CancellationToken);
            dataProtector.UnProtect(site);
            return site;
        }

        /// <summary>
        /// returns true if the folder is not in use or is in use only on the passed in ISiteSettings
        /// </summary>
        /// <param name=""></param>
        /// <param name="requestedFolderName"></param>
        /// <returns></returns>
        public async Task<bool> FolderNameIsAvailable(long requestingSiteId, string requestedFolderName)
        {
            var matchingSite = await queries.FetchByFolderName(requestedFolderName, CancellationToken).ConfigureAwait(false);
            if (matchingSite == null) { return true; }
            if (matchingSite.SiteFolderName != requestedFolderName) { return true; }
            if (matchingSite.Id == requestingSiteId) { return true; }

            return false;

        }

        public async Task<bool> HostNameIsAvailable(long requestingSiteId, string requestedHostName)
        {
            return await queries.HostNameIsAvailable(requestingSiteId, requestedHostName, CancellationToken).ConfigureAwait(false);

        }

        public async Task<bool> AliasIdIsAvailable(long requestingSiteId, string requestedAliasId)
        {
            if (multiTenantOptions.AllowSharedAliasId) return true;

            if (string.IsNullOrWhiteSpace(requestedAliasId)) return false;
            if (requestedAliasId.Length > 36) return false;
            return await queries.AliasIdIsAvailable(requestingSiteId, requestedAliasId, CancellationToken).ConfigureAwait(false);

        }

        public async Task Update(ISiteSettings site)
        {

            dataProtector.Protect(site);
            if (site.Id == 0)
            {
                await commands.Update(site, CancellationToken.None);
            }
            else
            {
                await commands.Update(site, CancellationToken.None);
            }
            if (multiTenantOptions.Mode == MultiTenantMode.FolderName)
            {
                if (string.IsNullOrEmpty(site.SiteFolderName))
                {
                    cacheHelper.ClearCache("root");
                }
                else
                {
                    cacheHelper.ClearCache(site.SiteFolderName);
                }
            }
            else
            {
                if (_context != null && !string.IsNullOrEmpty(_context.Request.Host.Value))
                    cacheHelper.ClearCache(_context.Request.Host.Value);
            }


        }

        public async Task Delete(ISiteSettings site)
        {


            // delete users
            //await userCommands.DeleteUsersBySite(site.Id, CancellationToken.None); // this also deletes userroles claims logins

            //await userCommands.DeleteRolesBySite(site.Id, CancellationToken.None);
            await commands.DeleteHostsBySite(site.Id, CancellationToken.None);
            //resultStep = await siteRepo.DeleteFoldersBySite(site.Sitelong, CancellationToken.None);


            // the below method deletes a lot of things by siteid including the following tables
            // Exec mp_Sites_Delete
            // mp_UserRoles
            // mp_UserProperties
            // mp_UserLocation
            // mp_Users
            // mp_Roles
            // mp_SiteHosts
            // mp_SiteFolders
            // mp_SiteSettingsEx
            // mp_Sites

            await commands.Delete(site.Id, CancellationToken.None);

            cacheHelper.ClearCache("folderList");
        }

        public async Task<SiteSettings> CreateNewSite(bool isServerAdminSite)
        {
            var newSite = new SiteSettings();
            newSite.SiteName = "Sample Site";
            newSite.IsServerAdminSite = isServerAdminSite;
            var siteNumber = 1 + await queries.CountOtherSites(0);
            newSite.AliasId = $"s{siteNumber}";

            await CreateNewSite(newSite);

            return newSite;
        }

        public async Task CreateNewSite(ISiteSettings newSite)
        {
            if (newSite == null) { throw new ArgumentNullException("you must pass in an instance of ISiteSettings"); }

            newSite.Theme = setupOptions.DefaultLayout;

            await commands.Create(newSite, CancellationToken.None);

            if (multiTenantOptions.Mode == MultiTenantMode.FolderName)
                cacheHelper.ClearCache("folderList");


        }
    }
}
