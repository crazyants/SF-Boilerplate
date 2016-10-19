
using SimpleFramework.Infrastructure.UI;
using System.Collections.Generic;


namespace SimpleFramework.Module.Backend
{
    public class BackendMetadata : BackendMetadataBase
    {
        public override IEnumerable<BackendStyleSheet> BackendStyleSheets
        {
            get
            {
                return new BackendStyleSheet[]
                {
          new BackendStyleSheet("/wwwroot.areas.backend.css.platformus.barebone.min.css", 1000),
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
          new BackendScript("/lib/jquery/jquery.min.js", 100),
          new BackendScript("/lib/jquery-validation/jquery.validate.min.js", 200),
          new BackendScript("/lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js", 300),
          new BackendScript("/lib/tinymce/tinymce.min.js", 400),
          new BackendScript("/wwwroot.areas.backend.js.platformus.barebone.min.js", 1000)
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
            "System",
            1000,
            new BackendMenuItem[]
            {
              new BackendMenuItem("/Backend/Admin", "admins", 4000)
            }
          )
                };
            }
        }
    }
}