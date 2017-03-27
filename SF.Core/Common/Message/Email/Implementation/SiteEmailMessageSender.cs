
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SF.Core.Common.Razor;
using SF.Entitys;
using SF.Core.Extensions;
using System;
using System.Threading.Tasks;

namespace SF.Core.Common.Message.Email
{
    public class SiteEmailMessageSender : ISiteMessageEmailSender
    {
        //TODO: we should have an option to force only plain text email
        // html emails are a lot more likely to be phished with copies
        // because the link urls are obfuscated to some degree

        public SiteEmailMessageSender(
            IViewRenderService viewRenderer,
             IOptions<SmtpOptions> smtpOptionsAccessor,
            ILogger<SiteEmailMessageSender> logger
            )
        {
            log = logger;
            this.viewRenderer = viewRenderer;
            globalSmtpSettings = smtpOptionsAccessor.Value;

        }
        private SmtpOptions globalSmtpSettings;
        private IViewRenderService viewRenderer;
        private ILogger log;

        private SmtpOptions GetSmptOptions(ISiteContext siteSettings)
        {
            if (string.IsNullOrWhiteSpace(siteSettings.SmtpServer)) { return globalSmtpSettings; }

            SmtpOptions smtpOptions = new SmtpOptions();
            smtpOptions.Password = siteSettings.SmtpPassword;
            smtpOptions.Port = siteSettings.SmtpPort;
            smtpOptions.PreferredEncoding = siteSettings.SmtpPreferredEncoding;
            smtpOptions.RequiresAuthentication = siteSettings.SmtpRequiresAuth;
            smtpOptions.Server = siteSettings.SmtpServer;
            smtpOptions.User = siteSettings.SmtpUser;
            smtpOptions.UseSsl = siteSettings.SmtpUseSsl;

            return smtpOptions;
        }

        public async Task SendAccountConfirmationEmailAsync(
            ISiteContext siteSettings,
            string toAddress,
            string subject,
            string confirmationUrl)
        {
            SmtpOptions smtpOptions = GetSmptOptions(siteSettings);
            if (smtpOptions == null)
            {
                var logMessage = $"failed to send account confirmation email because smtp settings are not populated for site {siteSettings.SiteName}";
                log.LogError(logMessage);
                return;
            }
            
            EmailSender sender = new EmailSender();
            try
            {
                var plainTextMessage
                = await viewRenderer.RenderViewAsString<string>("EmailTemplates/ConfirmAccountTextEmail", confirmationUrl).ConfigureAwait(false);

                var htmlMessage
                    = await viewRenderer.RenderViewAsString<string>("EmailTemplates/ConfirmAccountHtmlEmail", confirmationUrl).ConfigureAwait(false);

                await sender.SendEmailAsync(
                    smtpOptions,
                    toAddress,
                    siteSettings.DefaultEmailFromAddress,
                    subject,
                    plainTextMessage,
                    htmlMessage).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                log.LogError("error sending account confirmation email", ex);
            }

        }

        public async Task SendPasswordResetEmailAsync(
            ISiteContext siteSettings,
            string toAddress,
            string subject,
            string resetUrl)
        {
            SmtpOptions smtpOptions = GetSmptOptions(siteSettings);

            if (smtpOptions == null)
            {
                var logMessage = $"failed to send password reset email because smtp settings are not populated for site {siteSettings.SiteName}";
                log.LogError(logMessage);
                return;
            }
            
            EmailSender sender = new EmailSender();
            // in account controller we are calling this method without await
            // so it doesn't block the UI. Which means it is running on a background thread
            // similar as the old ThreadPool.QueueWorkItem
            // as such we need to handle any error that may happen so it doesn't
            // brind down the thread or the process
            try
            {
                var plainTextMessage
                   = await viewRenderer.RenderViewAsString<string>("EmailTemplates/PasswordResetTextEmail", resetUrl);

                var htmlMessage
                    = await viewRenderer.RenderViewAsString<string>("EmailTemplates/PasswordResetHtmlEmail", resetUrl);

                await sender.SendEmailAsync(
                    smtpOptions,
                    toAddress,
                    siteSettings.DefaultEmailFromAddress,
                    subject,
                    plainTextMessage,
                    htmlMessage).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                log.LogError("error sending password reset email", ex);
            }

        }

        public async Task SendSecurityCodeEmailAsync(
            ISiteContext siteSettings,
            string toAddress,
            string subject,
            string securityCode)
        {
            SmtpOptions smtpOptions = GetSmptOptions(siteSettings);

            if (smtpOptions == null)
            {
                var logMessage = $"failed to send security code email because smtp settings are not populated for site {siteSettings.SiteName}";
                log.LogError(logMessage);
                return;
            }
            
            EmailSender sender = new EmailSender();
            try
            {
                var plainTextMessage
                   = await viewRenderer.RenderViewAsString<string>("EmailTemplates/SendSecurityCodeTextEmail", securityCode);

                var htmlMessage
                    = await viewRenderer.RenderViewAsString<string>("EmailTemplates/SendSecurityCodeHtmlEmail", securityCode);

                await sender.SendEmailAsync(
                smtpOptions,
                toAddress,
                siteSettings.DefaultEmailFromAddress,
                subject,
                plainTextMessage,
                htmlMessage).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                log.LogError("error sending security code email", ex);
            }
        }

        public async Task AccountPendingApprovalAdminNotification(
            ISiteContext siteSettings,
            ISiteUser user)
        {
            if (siteSettings.AccountApprovalEmailCsv.Length == 0) { return; }

            SmtpOptions smtpOptions = GetSmptOptions(siteSettings);

            if (smtpOptions == null)
            {
                var logMessage = $"failed to send new account notifications to admins because smtp settings are not populated for site {siteSettings.SiteName}";
                log.LogError(logMessage);
                return;
            }

            string subject = "新用户审批";
           
            EmailSender sender = new EmailSender();
            try
            {
                var plainTextMessage
                   = await viewRenderer.RenderViewAsString<ISiteUser>("EmailTemplates/AccountPendingApprovalAdminNotificationTextEmail", user).ConfigureAwait(false);

                var htmlMessage
                    = await viewRenderer.RenderViewAsString<ISiteUser>("EmailTemplates/AccountPendingApprovalAdminNotificationHtmlEmail", user).ConfigureAwait(false);

                await sender.SendMultipleEmailAsync(
                    smtpOptions,
                    siteSettings.AccountApprovalEmailCsv,
                    siteSettings.DefaultEmailFromAddress,
                    subject,
                    plainTextMessage,
                    htmlMessage).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                log.LogError("error sending email verification email", ex);
            }

        }

        public async Task SendAccountApprovalNotificationAsync(
            ISiteContext siteSettings,
            string toAddress,
            string subject,
            string loginUrl)
        {
            SmtpOptions smtpOptions = GetSmptOptions(siteSettings);

            if (smtpOptions == null)
            {
                var logMessage = $"failed to send account approval email because smtp settings are not populated for site {siteSettings.SiteName}";
                log.LogError(logMessage);
                return;
            }
            
            EmailSender sender = new EmailSender();
            // in account controller we are calling this method without await
            // so it doesn't block the UI. Which means it is running on a background thread
            // similar as the old ThreadPool.QueueWorkItem
            // as such we need to handle any error that may happen so it doesn't
            // brind down the thread or the process
            try
            {
                var plainTextMessage
                   = await viewRenderer.RenderViewAsString<string>("EmailTemplates/AccountApprovedTextEmail", loginUrl);

                var htmlMessage
                    = await viewRenderer.RenderViewAsString<string>("EmailTemplates/AccountApprovedHtmlEmail", loginUrl);

                await sender.SendEmailAsync(
                    smtpOptions,
                    toAddress,
                    siteSettings.DefaultEmailFromAddress,
                    subject,
                    plainTextMessage,
                    htmlMessage).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                log.LogError("error sending password reset email", ex);
            }

        }


    }


}
