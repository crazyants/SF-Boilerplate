using SF.Entitys.Abstraction;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SF.Web.Security
{
    public class Permission
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
        public long Id { get; set; }
        public string Name { get; set; }
        public string ActionAddress { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// Id of the module which has registered this permission.
        /// </summary>
        public long ModuleId { get; set; } = -1;
        /// <summary>
        /// Display name of the group to which this permission belongs. The '|' character is used to separate Child and parent groups.
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// Generate permissions string with scope combination
        /// </summary>
        public IEnumerable<string> GetPermissionWithScopeCombinationNames()
        {
            var retVal = new List<string>();

            retVal.Add(Name.ToString());

            return retVal;
        }

        public static implicit operator Claim(Permission p)
        {
            return new Claim(ClaimType, p.Name);
        }
    }
}
