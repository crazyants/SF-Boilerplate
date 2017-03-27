 
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.SimpleAuth.ViewModels
{
    public class HashPasswordViewModel
    {
        [Required]
        [Display(Name = "Input Password")]
        public string InputPassword { get; set; } = string.Empty;

        [Display(Name = "Generated Hash")]
        public string OutputHash { get; set; } = string.Empty;
    }
}
