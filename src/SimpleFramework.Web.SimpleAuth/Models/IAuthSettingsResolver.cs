using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleFramework.Web.SimpleAuth.Models
{
    public interface IAuthSettingsResolver
    {
        SimpleAuthSettings GetCurrentAuthSettings();
    }
}
