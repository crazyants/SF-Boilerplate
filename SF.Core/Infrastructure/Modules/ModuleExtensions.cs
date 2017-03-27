using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SF.Core.Infrastructure.Modules
{
    public static class ModuleExtensions
    {

        public static string ToJsonIndented(this ModuleConfig node)
        {
            return JsonConvert.SerializeObject(
                node,
                Formatting.Indented,
                new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore }
                );
        }

        public static string ToJsonCompact(this ModuleConfig node)
        {
            return JsonConvert.SerializeObject(
                node,
                Formatting.None,
                new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore }
                );
        }
    }
}
