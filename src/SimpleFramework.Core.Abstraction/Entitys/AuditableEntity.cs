using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleFramework.Core.Abstraction.Entitys
{
    public abstract class AuditableEntity : BaseEntity, IAuditable
    {
	    #region IAuditable Members
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        [StringLength(64)]
        public string CreatedBy { get; set; }
        [StringLength(64)]
        public string ModifiedBy { get; set; }
        #endregion
    }
}
