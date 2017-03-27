using System.ComponentModel.DataAnnotations;

namespace SF.Module.Backend.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
