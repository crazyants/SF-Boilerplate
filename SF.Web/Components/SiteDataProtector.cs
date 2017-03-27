/*******************************************************************************
* 命名空间: SF.Web.Components
*
* 功 能： N/A
* 类 名： SiteDataProtector
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/13 16:24:45 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Logging;
using SF.Entitys;
using SF.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Web.Components
{
    public class SiteDataProtector
    {
        public SiteDataProtector(
            IDataProtectionProvider dataProtectionProvider,
            ILogger<SiteDataProtector> logger)
        {
            rawProtector = dataProtectionProvider.CreateProtector("SF.Entitys.SiteSettings");
            log = logger;
        }

        private ILogger log;
        private IDataProtector rawProtector = null;
        private IPersistedDataProtector dataProtector
        {
            get { return rawProtector as IPersistedDataProtector; }
        }

        public void Protect(ISiteSettings site)
        {
            if (site == null) { throw new ArgumentNullException("you must pass in an implementation of ISiteSettings"); }
            if (site.IsDataProtected) { return; }
            if (dataProtector == null) { return; }

            var countOfProtectedItems = 0;

            if (site.FacebookAppSecret.Length > 0)
            {
                try
                {
                    site.FacebookAppSecret = dataProtector.PersistentProtect(site.FacebookAppSecret);
                    countOfProtectedItems += 1;
                }
                catch (System.Security.Cryptography.CryptographicException ex)
                {
                    log.LogError("data protection error", ex);
                }

            }

            if (site.GoogleClientSecret.Length > 0)
            {
                try
                {
                    site.GoogleClientSecret = dataProtector.PersistentProtect(site.GoogleClientSecret);
                    countOfProtectedItems += 1;
                }
                catch (System.Security.Cryptography.CryptographicException ex)
                {
                    log.LogError("data protection error", ex);
                }

            }

            if (site.MicrosoftClientSecret.Length > 0)
            {
                try
                {
                    site.MicrosoftClientSecret = dataProtector.PersistentProtect(site.MicrosoftClientSecret);
                    countOfProtectedItems += 1;
                }
                catch (System.Security.Cryptography.CryptographicException ex)
                {
                    log.LogError("data protection error", ex);
                }

            }

            if (site.TwitterConsumerSecret.Length > 0)
            {
                try
                {
                    site.TwitterConsumerSecret = dataProtector.PersistentProtect(site.TwitterConsumerSecret);
                    countOfProtectedItems += 1;
                }
                catch (System.Security.Cryptography.CryptographicException ex)
                {
                    log.LogError("data protection error", ex);
                }

            }

            if (site.SmtpPassword.Length > 0)
            {
                try
                {
                    site.SmtpPassword = dataProtector.PersistentProtect(site.SmtpPassword);
                    countOfProtectedItems += 1;
                }
                catch (System.Security.Cryptography.CryptographicException ex)
                {
                    log.LogError("data protection error", ex);
                }

            }

            if (site.OidConnectAppSecret.Length > 0)
            {
                try
                {
                    site.OidConnectAppSecret = dataProtector.PersistentProtect(site.OidConnectAppSecret);
                    countOfProtectedItems += 1;
                }
                catch (System.Security.Cryptography.CryptographicException ex)
                {
                    log.LogError("data protection error", ex);
                }

            }

            if (site.DkimPrivateKey.Length > 0)
            {
                try
                {
                    site.DkimPrivateKey = dataProtector.PersistentProtect(site.DkimPrivateKey);
                    countOfProtectedItems += 1;
                }
                catch (System.Security.Cryptography.CryptographicException ex)
                {
                    log.LogError("data protection error", ex);
                }

            }

            if (site.SmsSecureToken.Length > 0)
            {
                try
                {
                    site.SmsSecureToken = dataProtector.PersistentProtect(site.SmsSecureToken);
                    countOfProtectedItems += 1;
                }
                catch (System.Security.Cryptography.CryptographicException ex)
                {
                    log.LogError("data protection error", ex);
                }

            }

            site.IsDataProtected = countOfProtectedItems > 0;


        }

        public void UnProtect(ISiteSettings site)
        {
            bool requiresMigration = false;
            bool wasRevoked = false;
            if (site == null) { throw new ArgumentNullException("you must pass in an implementation of ISiteSettings"); }
            if (!site.IsDataProtected) { return; }

            if (site.FacebookAppSecret.Length > 0)
            {
                try
                {
                    site.FacebookAppSecret = dataProtector.PersistentUnprotect(site.FacebookAppSecret, out requiresMigration, out wasRevoked);
                }
                catch (System.Security.Cryptography.CryptographicException ex)
                {
                    log.LogError("data protection error", ex);
                }
                catch (FormatException ex)
                {
                    log.LogError("data protection error", ex);
                }

            }

            if (site.GoogleClientSecret.Length > 0)
            {
                try
                {
                    site.GoogleClientSecret = dataProtector.PersistentUnprotect(site.GoogleClientSecret, out requiresMigration, out wasRevoked);
                }
                catch (System.Security.Cryptography.CryptographicException ex)
                {
                    log.LogError("data protection error", ex);
                }
                catch (FormatException ex)
                {
                    log.LogError("data protection error", ex);
                }

            }

            if (site.MicrosoftClientSecret.Length > 0)
            {
                try
                {
                    site.MicrosoftClientSecret = dataProtector.PersistentUnprotect(site.MicrosoftClientSecret, out requiresMigration, out wasRevoked);
                }
                catch (System.Security.Cryptography.CryptographicException ex)
                {
                    log.LogError("data protection error", ex);
                }
                catch (FormatException ex)
                {
                    log.LogError("data protection error", ex);
                }

            }

            if (site.TwitterConsumerSecret.Length > 0)
            {
                try
                {
                    site.TwitterConsumerSecret = dataProtector.PersistentUnprotect(site.TwitterConsumerSecret, out requiresMigration, out wasRevoked);
                }
                catch (System.Security.Cryptography.CryptographicException ex)
                {
                    log.LogError("data protection error", ex);
                }
                catch (FormatException ex)
                {
                    log.LogError("data protection error", ex);
                }

            }

            if (site.SmtpPassword.Length > 0)
            {
                try
                {
                    site.SmtpPassword = dataProtector.PersistentUnprotect(site.SmtpPassword, out requiresMigration, out wasRevoked);
                }
                catch (System.Security.Cryptography.CryptographicException ex)
                {
                    log.LogError("data protection error", ex);
                }
                catch (FormatException ex)
                {
                    log.LogError("data protection error", ex);
                }

            }

            if (site.OidConnectAppSecret.Length > 0)
            {
                try
                {
                    site.OidConnectAppSecret = dataProtector.PersistentUnprotect(site.OidConnectAppSecret, out requiresMigration, out wasRevoked);
                }
                catch (System.Security.Cryptography.CryptographicException ex)
                {
                    log.LogError("data protection error", ex);
                }
                catch (FormatException ex)
                {
                    log.LogError("data protection error", ex);
                }

            }

            if (site.SmsSecureToken.Length > 0)
            {
                try
                {
                    site.SmsSecureToken = dataProtector.PersistentUnprotect(site.SmsSecureToken, out requiresMigration, out wasRevoked);
                }
                catch (System.Security.Cryptography.CryptographicException ex)
                {
                    log.LogError("data protection error", ex);
                }
                catch (FormatException ex)
                {
                    log.LogError("data protection error", ex);
                }

            }

            if (site.DkimPrivateKey.Length > 0)
            {
                try
                {
                    site.DkimPrivateKey = dataProtector.PersistentUnprotect(site.DkimPrivateKey, out requiresMigration, out wasRevoked);
                }
                catch (System.Security.Cryptography.CryptographicException ex)
                {
                    log.LogError("data protection error", ex);
                }
                catch (FormatException ex)
                {
                    log.LogError("data protection error", ex);
                }

            }

            site.IsDataProtected = false;

            if (requiresMigration || wasRevoked)
            {
                log.LogWarning("DataProtection key wasRevoked or requires migration, save site settings for " + site.SiteName + " to protect with a new key");
            }


        }


    }
}
