using System;
using System.Collections.Generic;

namespace SimpleFramework.Web.Control.Lookup
{
    public abstract class MvcLookup
    {
        public const String Prefix = "Lookup";
        public const String IdKey = "LookupIdKey";
        public const String AcKey = "LookupAcKey";

        public String Url { get; set; }
        public String Title { get; set; }

        public LookupFilter Filter { get; set; }
        public IList<LookupColumn> Columns { get; set; }
        public IList<String> AdditionalFilters { get; set; }

        protected MvcLookup()
        {
            AdditionalFilters = new List<String>();
            Columns = new List<LookupColumn>();
            Filter = new LookupFilter();
            Filter.Rows = 20;
        }

        public abstract LookupData GetData();
    }
}
