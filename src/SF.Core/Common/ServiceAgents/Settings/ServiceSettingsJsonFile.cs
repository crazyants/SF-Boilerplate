using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Core.ServiceAgents.Settings
{
    public class ServiceSettingsJsonFile
    {
        public ServiceSettingsJsonFile() : this(Defaults.ServiceSettingsJsonFile.FileName)
        { }

        public ServiceSettingsJsonFile(string fileName)
        {
            FileName = fileName;
        }

        public string FileName { get; set; }
        public string Section { get; set; }
    }   
}
