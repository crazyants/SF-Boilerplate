using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Web.Base.Mappers.ModelBinders
{
    /// <summary>
    /// Specifies that a parameter or property should be bound using the request body.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class MapFromBodyAttribute : Attribute, IBindingSourceMetadata
    {
        public MapFromBodyAttribute(Type from)
        {
            SourceType = from;
        }

        public Type SourceType { get; private set; }

        /// <inheritdoc />
        public BindingSource BindingSource { get { return BindingSource.Body; } }
    }
}
