
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SF.Core.Web.Base.Datatypes
{
    /// <summary>
    /// 模型基类
    /// </summary>
    public abstract partial class ModelBase
    {
        public ModelBase()
        {

        }

        public virtual void BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
        }


    }
    /// <summary>
    /// 模型基类
    /// </summary>
    public abstract partial class EntityModelBase : ModelBase
    {
        public virtual long Id { get; set; }

        [Required(ErrorMessage = ErrorMessages.RequiredField)]
        public int SortIndex { get; set; }
    }
    public abstract partial class TabbableModel : EntityModelBase
    {

        public virtual string[] LoadedTabs { get; set; }
    }

}
