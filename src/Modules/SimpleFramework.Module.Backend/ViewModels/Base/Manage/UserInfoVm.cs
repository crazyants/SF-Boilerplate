using System.ComponentModel.DataAnnotations;

namespace SimpleFramework.Module.Backend.ViewModels.Manage
{
    public class UserInfoVm
    {
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
