using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Core.WorkContexts
{

    // lighter base version for lists
    public interface ISiteInfo
    {
        Guid Id { get; }
        string AliasId { get; set; }
        string SiteName { get; set; }
        string PreferredHostName { get; set; }
        string SiteFolderName { get; set; }
        bool IsServerAdminSite { get; set; }

    }
    public interface ISiteContext : ISiteInfo
    {
        bool UseLdapAuth { get; set; }
        bool AllowDbFallbackWithLdap { get; set; }
        bool EmailLdapDbFallback { get; set; }
        bool AutoCreateLdapUserOnFirstLogin { get; set; }
        string LdapServer { get; set; }
        string LdapDomain { get; set; }
        int LdapPort { get; set; }
        string LdapRootDN { get; set; }
        string LdapUserDNKey { get; set; }
        // TODO use this to force ONLY social logins or ldap option?
        bool DisableDbAuth { get; set; }

        bool RequireConfirmedEmail { get; set; } // maps to UseSecureRegistration in ado data layers

        bool UseEmailForLogin { get; set; }

        bool RequiresQuestionAndAnswer { get; set; }
        bool ReallyDeleteUsers { get; set; }
        //TODO: implement
        bool AllowNewRegistration { get; set; }

        bool AllowPersistentLogin { get; set; }

        string DefaultEmailFromAddress { get; set; }
        string DefaultEmailFromAlias { get; set; }
        string SmtpPassword { get; set; }
        int SmtpPort { get; set; }
        string SmtpPreferredEncoding { get; set; }
        bool SmtpRequiresAuth { get; set; }
        string SmtpServer { get; set; }
        string SmtpUser { get; set; }
        bool SmtpUseSsl { get; set; }
        string AccountApprovalEmailCsv { get; set; }
        string SmsClientId { get; set; }
        string SmsSecureToken { get; set; }
        string SmsFrom { get; set; }



        //company info
        string CompanyCountry { get; set; }
        string CompanyFax { get; set; }
        string CompanyLocality { get; set; }
        string CompanyName { get; set; }
        string CompanyPhone { get; set; }
        string CompanyPostalCode { get; set; }
        string CompanyPublicEmail { get; set; }
        string CompanyRegion { get; set; }
        string CompanyStreetAddress { get; set; }
        string CompanyStreetAddress2 { get; set; }


        string TimeZoneId { get; set; }
        bool SiteIsClosed { get; set; }
        string SiteIsClosedMessage { get; set; }


        bool IsDataProtected { get; set; }

        string ConnectionString { get; set; }
    }
}
