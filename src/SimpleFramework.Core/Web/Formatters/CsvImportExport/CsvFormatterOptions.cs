using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleFramework.Core.Formatters.CsvImportExport
{
    public class CsvFormatterOptions
    {
        public bool UseSingleLineHeaderInCsv { get; set; } = true;

        public string CsvDelimiter { get; set; } = ";";
    }
}
