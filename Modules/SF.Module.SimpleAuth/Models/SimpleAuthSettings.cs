 
using System.Collections.Generic;

namespace SF.Module.SimpleAuth.Models
{
    public class SimpleAuthSettings
    {
        //public List<SimpleAuthUser> Users { get; set; }

        /// <summary>
        /// if true the /Login/Hasher will be available to use for generating a hash, 
        /// that can then be stored in settings instead of clear textpassword
        /// </summary>
        public bool EnablePasswordHasherUi { get; set; } = false;

        public string RecaptchaPublicKey { get; set; }
        public string RecaptchaPrivateKey { get; set; }
        public string AuthenticationScheme { get; set; } = "application";
    }
}
