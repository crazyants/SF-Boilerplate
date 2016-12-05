using System;
using System.Collections.Generic;

namespace SF.Web.Control.Lookup
{
    public class LookupData
    {
        public Int32 FilteredRows { get; set; }
        public IList<LookupColumn> Columns { get; set; }
        public List<Dictionary<String, String>> Rows { get; set; }

        public LookupData()
        {
            Columns = new List<LookupColumn>();
            Rows = new List<Dictionary<String, String>>();
        }
    }
}