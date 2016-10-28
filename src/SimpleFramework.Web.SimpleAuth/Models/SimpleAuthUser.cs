 
using System.Collections.Generic;

namespace SimpleFramework.Web.SimpleAuth.Models
{
    public class SimpleAuthUser
    {

       
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool PasswordIsHashed { get; set; }
        public List<SimpleAuthClaim> Claims { get; set; }
    }
}
