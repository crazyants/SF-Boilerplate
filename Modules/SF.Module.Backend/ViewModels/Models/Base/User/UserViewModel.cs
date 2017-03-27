
using SF.Web.Models;
using System;

namespace SF.Module.Backend.ViewModels
{
    /// <summary>
    /// 用户试图模型
    /// </summary>
    public class UserViewModel : EntityModelBase
    {
        public Guid UserGuid { get; set; }

        public string FullName { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsAdministrator { get; set; }

        public string AccountState { get; set; }

        public string UserType { get; set; }

        public long? CurrentShippingAddressId { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }
    }
}
