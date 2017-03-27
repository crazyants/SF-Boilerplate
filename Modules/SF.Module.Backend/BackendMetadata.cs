

using SF.Core.Abstraction.UI.Backends;
using System.Collections.Generic;


namespace SF.Module.Backend
{
    public class BackendMetadata : BackendMetadataBase
    {
        public override IEnumerable<BackendStyleSheet> BackendStyleSheets
        {
            get
            {
                return new BackendStyleSheet[]
                {
          new BackendStyleSheet("http://fonts.googleapis.com/css?family=PT+Sans:400,400italic&subset=latin,cyrillic", 10000)
                };
            }
        }

        public override IEnumerable<BackendScript> BackendScripts
        {
            get
            {
                return new BackendScript[]
                {
          //new BackendScript("/lib/jquery/jquery.min.js", 100),
          //new BackendScript("/lib/jquery-validation/jquery.validate.min.js", 200),
          //new BackendScript("/lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js", 300)
                };
            }
        }
        public override IEnumerable<BackendMenuGroup> BackendMenuGroups
        {
            get
            {
                return new BackendMenuGroup[]
                {
          new BackendMenuGroup(
            "系统管理",
            1000,
            new BackendMenuItem[]
            {
              new BackendMenuItem("/Admin", "admins", 4000)
            }
          )
                };
            }
        }
    }
}