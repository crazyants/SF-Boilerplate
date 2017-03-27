
using Newtonsoft.Json.Linq;
using SF.Core.Json.JsonConverters;
using System;

namespace SF.Core.Infrastructure.Modules.Builder
{
    public class ModuleJsonConverter : JsonCreationConverter<ModuleConfig>
    {
        protected override ModuleConfig Create(Type objectType, JObject jObject)
        {
            if (jObject["Value"] == null) { return null; }

            ModuleConfig moduleConfigRoot = CreateTreeNode(null, jObject);


            return moduleConfigRoot;
        }

        private ModuleConfig CreateTreeNode(ModuleConfig tNode, JToken jNode)
        {
            //build the child node
            ModuleConfig moduleConfig = new ModuleConfig();

            if (jNode["Value"]["Key"] != null)
            {
                moduleConfig.Key = (string)jNode["Value"]["Key"];
            }

            if (jNode["Value"]["Version"] != null)
            {
                moduleConfig.Version = (string)jNode["Value"]["Version"];
            }

            if (jNode["Value"]["PlatformVersion"] != null)
            {
                moduleConfig.PlatformVersion = (string)jNode["Value"]["PlatformVersion"];
            }

            if (jNode["Value"]["Title"] != null)
            {
                moduleConfig.Title = (string)jNode["Value"]["Title"];
            }

            if (jNode["Value"]["Description"] != null)
            {
                moduleConfig.Description = (string)jNode["Value"]["Description"];
            }

            if (jNode["Value"]["Authors"] != null)
            {
                moduleConfig.Authors = (string)jNode["Value"]["Authors"];
            }

            if (jNode["Value"]["Owners"] != null)
            {
                moduleConfig.Owners = (string)jNode["Value"]["Owners"];
            }

            if (jNode["Value"]["IconUrl"] != null)
            {
                moduleConfig.IconUrl = (string)jNode["Value"]["IconUrl"];
            }

            if (jNode["Value"]["ReleaseNotes"] != null)
            {
                moduleConfig.ReleaseNotes = (string)jNode["Value"]["ReleaseNotes"];
            }

            if (jNode["Value"]["Copyright"] != null)
            {
                moduleConfig.Copyright = (string)jNode["Value"]["Copyright"];
            }

            if (jNode["Value"]["Tags"] != null)
            {
                moduleConfig.Tags = (string)jNode["Value"]["Tags"];
            }
            if (jNode["Value"]["SystemModule"] != null)
            {
                moduleConfig.SystemModule = (bool)jNode["Value"]["SystemModule"];
            }
            if (jNode["Value"]["ConnectionString"] != null)
            {
                moduleConfig.ConnectionString = (string)jNode["Value"]["ConnectionString"];
            }


            //TODO: add DataAttributes collection


            if (tNode == null)
            {

                return moduleConfig;
            }
            else
            {

                return moduleConfig;
            }


        }

    }
}
