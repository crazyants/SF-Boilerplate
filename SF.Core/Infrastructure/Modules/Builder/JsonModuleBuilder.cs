using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SF.Core.Infrastructure.Modules.Builder
{
    public class JsonModuleBuilder : IModuleConfigBuilder
    {
        public JsonModuleBuilder(
            IServiceProvider serviceProvider)
        {
            this.log = serviceProvider.GetService<ILoggerFactory>().CreateLogger<JsonModuleBuilder>();

        }

        private const string cacheKey = "modulejsonbuild";
        private ILogger log;
        private ModuleConfig moduleConfig = null;
        private string FilePath;


        public async Task<ModuleConfig> BuildConfig(string filePath)
        {
            // ultimately we will need to cache sitemap per site
            FilePath = filePath;

            moduleConfig = await BuildConfigByFile();

            return moduleConfig;
        }

        private async Task<ModuleConfig> BuildConfigByFile()
        {
            string filePath = ResolveFilePath();

            if (!File.Exists(filePath))
            {
                log.LogError("unable to build navigation tree, could not find the file " + filePath);

                return null;
            }

            string json;
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    json = await streamReader.ReadToEndAsync();
                }
            }

            ModuleConfig result = BuildTreeFromJson(json);

            return result;

        }

        private string ResolveFilePath()
        {
            string filePath = Path.Combine(FilePath, "module.json");

            return filePath;
        }


        // started to make this async since there are async methods of deserializeobject
        // but found this thread which says not to use them as there is no benefit
        //https://github.com/JamesNK/Newtonsoft.Json/issues/66
        public ModuleConfig BuildTreeFromJson(string jsonString)
        {
            ModuleConfig moduleConfig =
                    JsonConvert.DeserializeObject<ModuleConfig>(jsonString, new ModuleJsonConverter());


            return moduleConfig;

        }
    }
}
