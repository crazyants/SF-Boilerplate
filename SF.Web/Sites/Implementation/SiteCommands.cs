/*******************************************************************************
* 命名空间: SF.Services.Site.Implementation
*
* 功 能： N/A
* 类 名： SiteCommands
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/13 14:07:43 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using Microsoft.EntityFrameworkCore;
using SF.Core.Abstraction.UoW.Helper;
using SF.Data;
using SF.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SF.Web.Site.Implementation
{
    public class SiteCommands : ISiteCommands
    {

        public SiteCommands(IBaseUnitOfWork baseUnitOfWork)
        {
            this.baseUnitOfWork = baseUnitOfWork;
        }

        private IBaseUnitOfWork baseUnitOfWork;

        public async Task Create(
            ISiteSettings site,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (site == null) throw new ArgumentException("site must not be null");
            if (site.Id == 0) throw new ArgumentException("site must have a non-empty Id");

            var siteSettings = SiteSettings.FromISiteSettings(site);

            await baseUnitOfWork.ExecuteAndCommitAsync((u, c) =>
           {
               return u.BaseWorkArea.SiteSettings.AddAsync(siteSettings);
           });

        }

        public async Task Update(
            ISiteSettings site,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (site == null) throw new ArgumentException("site must not be null");
            if (site.Id == 0) throw new ArgumentException("site must have a non-empty Id");

            var siteSettings = SiteSettings.FromISiteSettings(site);

            await baseUnitOfWork.ExecuteAndCommitAsync((u, c) =>
           {
               return u.BaseWorkArea.SiteSettings.UpdateAsync(siteSettings);
           });

        }

        public async Task Delete(
            long siteId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (siteId == 0) throw new ArgumentException("id must not be empty guid");


            var itemToRemove = await baseUnitOfWork.BaseWorkArea.SiteSettings.Query().SingleOrDefaultAsync(
                x => x.Id == siteId
                , cancellationToken)
                .ConfigureAwait(false);

            if (itemToRemove == null) throw new InvalidOperationException("site not found");

            await baseUnitOfWork.ExecuteAndCommitAsync((u, c) =>
           {
               return u.BaseWorkArea.SiteSettings.DeleteAsync(itemToRemove);
           });

        }

        public async Task AddHost(
            long siteId,
            string hostName,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (siteId == 0) throw new ArgumentException("siteId must not be empty guid");

            var host = new SiteHost();
            host.SiteId = siteId;
            host.HostName = hostName;

            await baseUnitOfWork.ExecuteAndCommitAsync((u, c) =>
           {
               return u.BaseWorkArea.SiteHost.AddAsync(host);
           });

        }

        public async Task DeleteHost(
            long siteId,
            long hostId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (hostId == 0) throw new ArgumentException("hostId must not be empty guid");

            var itemToRemove = await baseUnitOfWork.BaseWorkArea.SiteHost.Query().SingleOrDefaultAsync(x => x.Id == hostId, cancellationToken);
            if (itemToRemove == null) throw new InvalidOperationException("host not found");



            await baseUnitOfWork.ExecuteAndCommitAsync((u, c) =>
             {
                 return u.BaseWorkArea.SiteHost.DeleteAsync(itemToRemove);
             });

        }

        public async Task DeleteHostsBySite(
            long siteId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (siteId == 0) throw new ArgumentException("siteId must not be empty guid");

            var query = from x in baseUnitOfWork.BaseWorkArea.SiteHost.Query().Where(x => x.SiteId == siteId)
                        select x;
            await baseUnitOfWork.ExecuteAndCommitAsync((u, c) =>
             {
                 return u.BaseWorkArea.SiteHost.DeleteAsync(query);
             });


        }


        #region IDisposable Support

        private void ThrowIfDisposed()
        {
            if (disposedValue)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        private bool disposedValue = false; // To detect redundant calls

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }


        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion
    }
}
