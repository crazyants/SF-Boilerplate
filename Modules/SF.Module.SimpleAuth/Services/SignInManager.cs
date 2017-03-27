
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SF.Module.SimpleAuth.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace SF.Module.SimpleAuth.Services
{
    public class SignInManager
    {
        public SignInManager(
            IAuthSettingsResolver settingsResolver,
            IUserLookupProvider userLookupProvider,
            IPasswordHasher<SimpleAuthUser> passwordHasher,
            ILogger<SignInManager> logger)
        {
            authSettings = settingsResolver.GetCurrentAuthSettings();
            userRepo = userLookupProvider;
            this.passwordHasher = passwordHasher;
            log = logger;
        }

        private IUserLookupProvider userRepo;
        private SimpleAuthSettings authSettings;
        private IPasswordHasher<SimpleAuthUser> passwordHasher;
        //private List<SimpleAuthUser> allUsers;
        private ILogger log;

        public SimpleAuthSettings AuthSettings
        {
            get { return authSettings; }
        }

        public SimpleAuthUser GetUser(string userName)
        {
            return userRepo.GetUser(userName);
        }

        public bool ValidatePassword(SimpleAuthUser authUser, string providedPassword)
        {
            bool result = false;
            if (authUser.PasswordIsHashed)
            {
                var hashResult = passwordHasher.VerifyHashedPassword(authUser, authUser.Password, providedPassword);
                result = (hashResult == PasswordVerificationResult.Success);

            }
            else
            {
                result = authUser.Password == providedPassword;
            }

            return result;
        }

        public ClaimsPrincipal GetClaimsPrincipal(SimpleAuthUser authUser)
        {
            var identity = new ClaimsIdentity(authSettings.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, "1"));
            identity.AddClaim(new Claim(ClaimTypes.Name, authUser.UserName));

            foreach (SimpleAuthClaim c in authUser.Claims)
            {
                if (c.ClaimType == "Email")
                {
                    identity.AddClaim(new Claim(ClaimTypes.Email, c.ClaimValue));
                }
                else if (c.ClaimType == "Role")
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, c.ClaimValue));
                }
                else
                {
                    identity.AddClaim(new Claim(c.ClaimType, c.ClaimValue));
                }
            }

            var prince = new ClaimsPrincipal(identity);

            return prince;
        }

        public string HashPassword(string inputPassword)
        {
            var fakeUser = new SimpleAuthUser();
            return passwordHasher.HashPassword(fakeUser, inputPassword);
        }


    }
}
