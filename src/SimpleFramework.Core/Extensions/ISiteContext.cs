using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleFramework.Core.Extensions
{
    public interface ISiteContext
    {
        string SiteName { get; set; }
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
    }
}
