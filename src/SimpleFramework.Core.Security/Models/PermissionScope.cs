using SimpleFramework.Infrastructure.Common;
using SimpleFramework.Infrastructure.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SimpleFramework.Core.Security
{
    /// <summary>
    /// Base class for all types repesents permision boundary scopes
    /// </summary>
    public class PermissionScope : ValueObject<PermissionScope>
    {
        public PermissionScope()
        {
            Type = this.GetType().Name;
        }
        /// <summary>
        /// Define scope type influences the choice of ui pattern in role definition 
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Display representation 
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Represent string scope value
        /// </summary>
        public string Scope { get; set; }


        /// <summary>
        /// Return all supported scope for given permission used in role permission configuration 
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        public virtual bool IsScopeAvailableForPermission(long permission) {
            return false;
        }

        /// <summary>
        /// Return resulting list of scope string for entity may be used for permissions check
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual IEnumerable<string> GetEntityScopeStrings(object entity) {
            return null;
        }
      
        public override string ToString()
        {
            return Type + ":" + Scope;
        }
    }
}
