using System.ComponentModel.DataAnnotations;

namespace SimpleFramework.Module.Security.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
