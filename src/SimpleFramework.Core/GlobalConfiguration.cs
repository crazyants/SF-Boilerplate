using System.Collections.Generic;

namespace SimpleFramework.Core
{
    public static class GlobalConfiguration
    {
        static GlobalConfiguration()
        {
        }

        public static string WebRootPath { get; set; }

        public static string ContentRootPath { get; set; }
    }
}
