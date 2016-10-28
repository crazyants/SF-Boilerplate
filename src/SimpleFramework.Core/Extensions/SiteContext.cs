using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleFramework.Core.Extensions
{
    public class SiteContext : ISiteContext
    {
        public SiteContext()
        {
            this.Id = Guid.NewGuid();
        }
        //public int SiteId { get; set; } = -1;
        public Guid Id { get; set; } = Guid.Empty;

        public string SiteName { get; set; }

        public string DefaultEmailFromAddress { get; set; }

        public string DefaultEmailFromAlias { get; set; }

        public string SmtpUser { get; set; }

        public string SmtpPassword { get; set; }

        public int SmtpPort { get; set; } = 25;

        public string SmtpPreferredEncoding { get; set; }

        public string SmtpServer { get; set; }

        public bool SmtpRequiresAuth { get; set; } = false;

        public bool SmtpUseSsl { get; set; } = false;

        public string AccountApprovalEmailCsv { get; set; }

        public string SmsClientId { get; set; }

        public string SmsSecureToken { get; set; }

        public string SmsFrom { get; set; }

        public bool UseLdapAuth { get; set; }

        public bool AllowDbFallbackWithLdap { get; set; }

        public bool EmailLdapDbFallback { get; set; }

        public bool AutoCreateLdapUserOnFirstLogin { get; set; }

        public string LdapServer { get; set; }

        public string LdapDomain { get; set; }

        public int LdapPort { get; set; }

        public string LdapRootDN { get; set; }

        public string LdapUserDNKey { get; set; }

        public bool DisableDbAuth { get; set; }

        public bool RequireConfirmedEmail { get; set; }

        public bool UseEmailForLogin { get; set; }

        public bool RequiresQuestionAndAnswer { get; set; }

        public bool ReallyDeleteUsers { get; set; }

        public bool AllowNewRegistration { get; set; }

        public bool AllowPersistentLogin { get; set; }

        public string CompanyCountry { get; set; }

        public string CompanyFax { get; set; }

        public string CompanyLocality { get; set; }

        public string CompanyName { get; set; }

        public string CompanyPhone { get; set; }

        public string CompanyPostalCode { get; set; }

        public string CompanyPublicEmail { get; set; }

        public string CompanyRegion { get; set; }

        public string CompanyStreetAddress { get; set; }

        public string CompanyStreetAddress2 { get; set; }

        public string TimeZoneId { get; set; }

        public bool SiteIsClosed { get; set; }

        public string SiteIsClosedMessage { get; set; }

        public bool IsDataProtected { get; set; }

 

        public string AliasId { get; set; }

        public string PreferredHostName { get; set; }

        public string SiteFolderName { get; set; }

        public bool IsServerAdminSite { get; set; }


        public string ConnectionString { get; set; }
    }
}
