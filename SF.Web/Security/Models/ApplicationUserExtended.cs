using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SF.Entitys;

namespace SF.Web.Security
{
    public class ApplicationUserExtended
    {
        public ApplicationUserExtended()
        {
             
        }

        public long Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Tenant id
        /// </summary>
        public string Icon { get; set; }

        public bool IsAdministrator { get; set; }

        public string UserType { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public AccountState UserState { get; set; }

        public string Password { get; set; }

        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }

        /// <summary>
        /// External provider logins.
        /// </summary>
        public ApplicationUserLogin[] Logins { get; set; }

        /// <summary>
        /// Assigned roles.
        /// </summary>
        public Role[] Roles { get; set; }
        /// <summary>
        /// All permissions from assigned roles.
        /// </summary>
        public string[] Modules { get; set; }
        /// <summary>
        /// All permissions from assigned roles.
        /// </summary>
        public string[] Permissions { get; set; }

    }
}
