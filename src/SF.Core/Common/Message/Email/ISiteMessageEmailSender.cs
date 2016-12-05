
using SF.Core.Extensions;
using SF.Core.WorkContexts;
using System.Threading.Tasks;

namespace SF.Core.Common.Message.Email
{
    public interface ISiteMessageEmailSender
    {
        Task SendAccountConfirmationEmailAsync(
            ISiteContext siteSettings,
            string toAddress, 
            string subject, 
            string confirmationUrl);

        Task SendSecurityCodeEmailAsync(
            ISiteContext siteSettings,
            string toAddress,
            string subject,
            string securityCode);

        Task SendPasswordResetEmailAsync(
            ISiteContext siteSettings,
            string toAddress,
            string subject,
            string resetUrl);

        Task AccountPendingApprovalAdminNotification(
            ISiteContext siteSettings,
            IWorkContext user);

        Task SendAccountApprovalNotificationAsync(
            ISiteContext siteSettings,
            string toAddress,
            string subject,
            string loginUrl);
    }
}
