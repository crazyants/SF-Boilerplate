
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace SimpleFramework.Core.Models
{
    public sealed class CustomPropertiesDictionary : Dictionary<string, object>
    {
    }
    public abstract partial class ModelBase
    {
        public ModelBase()
        {
            this.CustomProperties = new CustomPropertiesDictionary();
        }

        public virtual void BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
        }

        /// <summary>
        /// Use this property to store any custom value for your models. 
        /// </summary>
        public CustomPropertiesDictionary CustomProperties { get; set; }
    }

    public abstract partial class EntityModelBase : ModelBase
    {
        public virtual long Id { get; set; }
    }
    public abstract partial class TabbableModel : EntityModelBase
    {
 
        public virtual string[] LoadedTabs { get; set; }
    }
   
}
