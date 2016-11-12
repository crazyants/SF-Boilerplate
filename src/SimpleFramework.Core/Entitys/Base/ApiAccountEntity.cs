using SimpleFramework.Core.Abstraction.Entitys;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleFramework.Core.Entitys
{
    public class ApiAccountEntity : BaseEntity
    {
        [StringLength(128)]
        public string Name { get; set; }
        public ApiAccountType ApiAccountType { get; set; }
        public long AccountId { get; set; }

        [StringLength(128)]
        [Required]
        public string AppId { get; set; }
        public string SecretKey { get; set; }
        public bool IsActive { get; set; }

        public virtual UserEntity Account { get; set; }
    }
}
