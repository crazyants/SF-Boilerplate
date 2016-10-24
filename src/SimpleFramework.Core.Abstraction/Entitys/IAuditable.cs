using System;

namespace SimpleFramework.Core.Abstraction.Entitys
{
    public interface IAuditable
    {
        DateTimeOffset CreatedDate { get; set; }
        DateTimeOffset? ModifiedDate { get; set; }
        string CreatedBy { get; set; }
        string ModifiedBy { get; set; }
    }
}
