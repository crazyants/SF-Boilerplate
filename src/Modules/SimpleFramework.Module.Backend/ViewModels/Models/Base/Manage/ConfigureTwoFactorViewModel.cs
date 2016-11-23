using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SimpleFramework.Module.Backend.ViewModels.Manage
{
    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }

        public ICollection<SelectListItem> Providers { get; set; }
    }
}
