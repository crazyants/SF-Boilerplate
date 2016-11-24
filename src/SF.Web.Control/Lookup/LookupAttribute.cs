using System;
using System.Reflection;

namespace SF.Web.Control.Lookup
{
    public class LookupAttribute : Attribute
    {
        public Type Type { get; }

        public LookupAttribute(Type type)
        {
            if (!typeof(MvcLookup).IsAssignableFrom(type))
                throw new ArgumentException($"'{type?.Name}' type does not implement '{typeof(MvcLookup).Name}'.");

            Type = type;
        }
    }
}
