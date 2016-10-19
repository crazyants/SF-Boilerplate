using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleFramework.Core.Extensions
{
    public class SiteContext : ISiteContext
    {
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
    }
}
