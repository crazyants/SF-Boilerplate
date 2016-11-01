using SimpleFramework.Core.Abstraction.Entitys;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using System;
using SimpleFramework.Core.Web.Base.Datatypes;

namespace SimpleFramework.Core.Security
{
    public class Permission : EntityModelBase
    {
        public const string ClaimType = "Permission";
        public Permission()
        {
        }
        public Permission(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
        }

        public Permission(string name, string description) : this(name)
        {
            Description = description;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// Id of the module which has registered this permission.
        /// </summary>
        public string ModuleId { get; set; }
        /// <summary>
        /// Display name of the group to which this permission belongs. The '|' character is used to separate Child and parent groups.
        /// </summary>
        public string GroupName { get; set; }

        public ICollection<PermissionScope> AssignedScopes { get; set; }

        public ICollection<PermissionScope> AvailableScopes { get; set; }
        /// <summary>
        /// Generate permissions string with scope combination
        /// </summary>
        public IEnumerable<string> GetPermissionWithScopeCombinationNames()
        {
            var retVal = new List<string>();
            if (AssignedScopes != null && AssignedScopes.Any())
            {
                retVal.AddRange(AssignedScopes.Select(x => Id + ":" + x.ToString()));
            }
            else
            {
                retVal.Add(Id.ToString());
            }
            return retVal;
        }

        public static implicit operator Claim(Permission p)
        {
            return new Claim(ClaimType, p.Name);
        }
    }
}
