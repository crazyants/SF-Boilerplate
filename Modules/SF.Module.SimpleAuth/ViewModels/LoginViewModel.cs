 
using System.ComponentModel.DataAnnotations;


namespace SF.Module.SimpleAuth.ViewModels
{
    public class LoginViewModel
    {

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public string RecaptchaSiteKey { get; set; } = string.Empty;

    }
}
