using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace SF.Web.Security
{
    public interface IPermissionScopeService
    {
        /// <summary>
        /// Return scopes list for concrete permission used in future for permission bound
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        IEnumerable<PermissionScope> GetAvailablePermissionScopes(long permission);
        /// <summary>
        /// Factory method for scope
        /// </summary>
        /// <param name="scopeTypeName"></param>
        /// <returns></returns>
        PermissionScope GetScopeByTypeName(string scopeTypeName);
        /// <summary>
        /// Gets concrete entity scope resulting strings representation for using in future permission checks 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        IEnumerable<string> GetObjectPermissionScopeStrings(object obj);

        void RegisterSope(Func<PermissionScope> scopeFactory);
    }
}
