
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleFramework.Core.Web.Base.Datatypes
{
    public sealed class CustomPropertiesDictionary : Dictionary<string, object>
    {
    }
    public abstract partial class ModelBase
    {
        public ModelBase()
        {

        }

        public virtual void BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
        }


    }

    public abstract partial class EntityModelBase : ModelBase
    {
        public virtual long Id { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredField)]
        public int Sortindex { get; set; }
    }
    public abstract partial class TabbableModel : EntityModelBase
    {

        public virtual string[] LoadedTabs { get; set; }
    }

}
