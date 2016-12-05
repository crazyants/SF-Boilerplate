
using Microsoft.Extensions.Options;
using SF.Web.SimpleAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Web.SimpleAuth.Services
{
    public class DefaultAuthSettingsResolver : IAuthSettingsResolver
    {
        public DefaultAuthSettingsResolver(IOptions<SimpleAuthSettings> settingsAccessor)
        {
            authSettings = settingsAccessor.Value;
        }

        private SimpleAuthSettings authSettings;

        public SimpleAuthSettings GetCurrentAuthSettings()
        {
            return authSettings;
        }
    }
}
