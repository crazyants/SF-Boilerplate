using System;

namespace SimpleFramework.Web.Control.Lookup
{
    public class LookupColumnAttribute : Attribute
    {
        public Int32 Position { get; set; }
        public Boolean Hidden { get; set; }
        public String Format { get; set; }

        public LookupColumnAttribute()
        {
        }
        public LookupColumnAttribute(Int32 position)
        {
            Position = position;
        }
    }
}
