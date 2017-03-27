using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CacheManager.Core;
using SF.Web.Security.Converters;
using SF.Entitys;
using SF.Core.Extensions;
using Microsoft.AspNetCore.Identity;
using SF.Core.Abstraction.Data;
using SF.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace SF.Web.Security
{
    public class SecurityService : ISecurityService
    {
        private readonly IBaseUnitOfWork _baseUnitOfWork;
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<RoleEntity> _roleManager;
        private readonly ICacheManager<object> _cacheManager;
        private readonly IEnumerable<IPermissionProvider> _permissionProviders;
        private readonly IHttpContextAccessor _contextAccessor;
        public SecurityService(IBaseUnitOfWork baseUnitOfWork,
                               UserManager<UserEntity> userManager,
                               RoleManager<RoleEntity> roleManage,
                               ICacheManager<object> cacheManager,
                               IEnumerable<IPermissionProvider> permissionProviders,
                                IHttpContextAccessor contextAccessor)
        {
            _baseUnitOfWork = baseUnitOfWork;
            _userManager = userManager;
            _roleManager = roleManage;
            _cacheManager = cacheManager;
            _permissionProviders = permissionProviders;
            _contextAccessor = contextAccessor;
        }

        #region ISecurityService Members
        public async Task<ApplicationUserExtended> GetCurrentUser(UserDetails detailsLevel)
        {
            var context = _contextAccessor.HttpContext;
            if (context.User.Identity.IsAuthenticated)
            {
                var user = await GetApplicationUserByNameAsync(context.User.GetUserName());
                return GetUserExtended(user, detailsLevel);
            }
            return new ApplicationUserExtended();
        }
        public async Task<ApplicationUserExtended> FindByNameAsync(string userName, UserDetails detailsLevel)
        {
            var user = await GetApplicationUserByNameAsync(userName);
            return GetUserExtended(user, detailsLevel);
        }

        public async Task<ApplicationUserExtended> FindByIdAsync(string userId, UserDetails detailsLevel)
        {
            var user = await GetApplicationUserByIdAsync(userId);
            return GetUserExtended(user, detailsLevel);
        }

        public async Task<ApplicationUserExtended> FindByEmailAsync(string email, UserDetails detailsLevel)
        {
            using (var userManager = _userManager)
            {
                var user = await userManager.FindByEmailAsync(email);
                return GetUserExtended(user, detailsLevel);
            }
        }

        public async Task<ApplicationUserExtended> FindByLoginAsync(string loginProvider, string providerKey, UserDetails detailsLevel)
        {
            using (var userManager = _userManager)
            {
                var user = await userManager.FindByLoginAsync(loginProvider, providerKey);
                return GetUserExtended(user, detailsLevel);
            }
        }

        public async Task<SecurityResult> CreateAsync(ApplicationUserExtended user)
        {
            IdentityResult result;

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            NormalizeUser(user);

            //Update ASP.NET indentity user
            using (var userManager = _userManager)
            {
                var dbUser = user.ToIdentityModel();
                user.Id = dbUser.Id;

                if (string.IsNullOrEmpty(user.Password))
                {
                    result = await userManager.CreateAsync(dbUser);
                }
                else
                {
                    result = await userManager.CreateAsync(dbUser, user.Password);
                }
            }

            if (result.Succeeded)
            {
                //using (var repository = _platformRepository())
                //{
                //    var dbAcount = user.ToDataModel();
                //    if(string.IsNullOrEmpty(user.MemberId))
                //    {
                //        //Use for memberId same account id if its not set (Our current case Contact member 1 - 1 Account workaround). But client may use memberId as for any outer id.
                //        dbAcount.MemberId = dbAcount.Id;
                //    }
                //    dbAcount.AccountState = AccountState.Approved.ToString();

                //    repository.Add(dbAcount);
                //    repository.UnitOfWork.Commit();
                //}
            }

            return result.ToCoreModel();
        }

        public async Task<SecurityResult> UpdateAsync(ApplicationUserExtended user)
        {
            SecurityResult result;

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            NormalizeUser(user);

            //Update ASP.NET indentity user
            using (var userManager = _userManager)
            {
                var dbUser = await userManager.FindByIdAsync(user.Id.ToString());
                result = ValidateUser(dbUser);

                if (result.Succeeded)
                {
                    var userName = dbUser.UserName;

                    //Update ASP.NET indentity user
                    user.Patch(dbUser);
                    var identityResult = await userManager.UpdateAsync(dbUser);
                    result = identityResult.ToCoreModel();

                    //clear cache
                    RemoveUserFromCache(user.Id, userName);
                }
            }

            if (result.Succeeded)
            {
                //Update platform security user
                //using (var repository = _platformRepository())
                //{
                //    var targetDbAcount = repository.GetAccountByName(user.UserName, UserDetails.Full);

                //    if (targetDbAcount == null)
                //    {
                //        result = new SecurityResult { Errors = new[] { "Account not found." } };
                //    }
                //    else
                //    {
                //        var changedDbAccount = user.ToDataModel();
                //        using (var changeTracker = GetChangeTracker(repository))
                //        {
                //            changeTracker.Attach(targetDbAcount);

                //            changedDbAccount.Patch(targetDbAcount);
                //            repository.UnitOfWork.Commit();
                //        }
                //    }
                //}
            }

            return result;
        }

        public async Task DeleteAsync(string[] names)
        {
            using (var userManager = _userManager)
            {
                foreach (var name in names)
                {
                    var dbUser = await userManager.FindByNameAsync(name);

                    if (dbUser != null)
                    {
                        await userManager.DeleteAsync(dbUser);

                        //using (var repository = _platformRepository())
                        //{
                        //    var account = repository.GetAccountByName(name, UserDetails.Reduced);
                        //    if (account != null)
                        //    {
                        //        repository.Remove(account);
                        //        repository.UnitOfWork.Commit();
                        //    }
                        //}

                        //clear cache
                        RemoveUserFromCache(dbUser.Id, name);
                    }
                }
            }
        }


        public async Task<SecurityResult> ChangePasswordAsync(string name, string oldPassword, string newPassword)
        {
            using (var userManager = _userManager)
            {
                var dbUser = await GetApplicationUserByNameAsync(name);
                var result = ValidateUser(dbUser);

                if (result.Succeeded)
                {
                    var identityResult = await userManager.ChangePasswordAsync(dbUser, oldPassword, newPassword);
                    result = identityResult.ToCoreModel();
                }

                return result;
            }
        }

        public async Task<SecurityResult> ResetPasswordAsync(string name, string newPassword)
        {
            using (var userManager = _userManager)
            {
                var dbUser = await GetApplicationUserByNameAsync(name);
                var result = ValidateUser(dbUser);

                if (result.Succeeded)
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(dbUser);
                    var identityResult = await userManager.ResetPasswordAsync(dbUser, token, newPassword);
                    result = identityResult.ToCoreModel();
                }

                return result;
            }
        }

        public async Task<SecurityResult> ResetPasswordAsync(string userId, string token, string newPassword)
        {
            using (var userManager = _userManager)
            {
                var dbUser = await GetApplicationUserByIdAsync(userId);
                var result = ValidateUser(dbUser);

                if (result.Succeeded)
                {
                    var identityResult = await userManager.ResetPasswordAsync(dbUser, token, newPassword);
                    result = identityResult.ToCoreModel();
                }

                return result;
            }
        }

        public async Task<UserSearchResponse> SearchUsersAsync(UserSearchRequest request)
        {
            request = request ?? new UserSearchRequest();
            var result = new UserSearchResponse();


            var query = _baseUnitOfWork.BaseWorkArea.User.Query();

            if (request.Keyword != null)
            {
                query = query.Where(u => u.UserName.Contains(request.Keyword));
            }

            if (!string.IsNullOrEmpty(request.MemberId))
            {
                //Find all accounts with specified memberId
                query = query.Where(u => u.Id.ToString() == request.MemberId);
            }

            if (request.AccountTypes != null && request.AccountTypes.Any())
            {
                //  query = query.Where(x => request.AccountTypes.Contains(x.UserType));
            }
            result.TotalCount = query.Count();

            var users = query.OrderBy(x => x.UserName)
                             .Skip(request.SkipCount)
                             .Take(request.TakeCount)
                             .ToArray();

            var extendedUsers = new List<ApplicationUserExtended>();

            foreach (var user in users)
            {
                var extendedUser = await FindByNameAsync(user.UserName, UserDetails.Reduced);
                if (extendedUser != null)
                {
                    extendedUsers.Add(extendedUser);
                }
            }

            result.Users = extendedUsers.ToArray();

            return result;

        }

        public async Task<string> GeneratePasswordResetTokenAsync(string userId)
        {

            using (var userManager = _userManager)
            {
                var dbUser = await GetApplicationUserByIdAsync(userId);
                var result = ValidateUser(dbUser);

                if (result.Succeeded)
                {
                    return await userManager.GeneratePasswordResetTokenAsync(dbUser);
                }
            }
            return "";
        }

        //public Permission[] GetAllPermissions()
        //{
        //    return _cacheManager.Get("AllPermissions", "PlatformRegion", LoadAllPermissions);
        //}

        public bool UserHasAnyPermission(string userName, string[] scopes, params string[] permissionIds)
        {
            if (permissionIds == null)
            {
                throw new ArgumentNullException("permissionIds");
            }

            var user = FindByName(userName, UserDetails.Full);

            var result = user != null && user.UserState == AccountState.Approved;

            if (result && user.IsAdministrator)
            {
                return true;
            }

            //For managers always allow to call api
            if (result && permissionIds.Length == 1 && permissionIds.Contains(GobalPermissionProvider.SecurityCallApi.Name)
               && (string.Equals(user.UserType, AccountType.Manager.ToString(), StringComparison.CurrentCultureIgnoreCase) ||
                    string.Equals(user.UserType, AccountType.Administrator.ToString(), StringComparison.CurrentCultureIgnoreCase)))
            {
                return true;
            }

            if (result)
            {
                var fqUserPermissions = user.Roles.SelectMany(x => x.Permissions).SelectMany(x => x.GetPermissionWithScopeCombinationNames()).Distinct();
                var fqCheckPermissions = permissionIds.Concat(permissionIds.LeftJoin(scopes, ":"));
                result = fqUserPermissions.Intersect(fqCheckPermissions, StringComparer.OrdinalIgnoreCase).Any();
            }

            return result;
        }

        public Permission[] GetUserPermissions(string userName)
        {
            var user = FindByName(userName, UserDetails.Full);
            var result = user != null ? user.Roles.SelectMany(x => x.Permissions).Distinct().ToArray() : Enumerable.Empty<Permission>().ToArray();
            return result;
        }
        #endregion

        private ApplicationUserExtended FindByName(string userName, UserDetails detailsLevel)
        {
            var user = GetApplicationUserByName(userName);
            return GetUserExtended(user, detailsLevel);
        }

        private SecurityResult ValidateUser(UserEntity dbUser)
        {
            var result = new SecurityResult { Succeeded = true };

            if (dbUser == null)
            {
                result = new SecurityResult { Errors = new[] { "User not found." } };
            }

            return result;
        }

        private async Task<UserEntity> GetApplicationUserByIdAsync(string userId)
        {
            var cacheRegion = GetUserCacheRegion(userId);

            var result = await _cacheManager.GetAsync(cacheRegion, cacheRegion, async () =>
            {
                using (var userManager = _userManager)
                {
                    return await userManager.FindByIdAsync(userId);
                }
            });

            return result;
        }

        private UserEntity GetApplicationUserByName(string userName)
        {
            var cacheRegion = GetUserCacheRegion(userName);

            var result = _cacheManager.Get(cacheRegion, cacheRegion, () =>
            {
                using (var userManager = _userManager)
                {
                    return Task.Run(async () => await userManager.FindByNameAsync(userName)).Result;
                }
            });

            return result;
        }

        private async Task<UserEntity> GetApplicationUserByNameAsync(string userName)
        {
            var cacheRegion = GetUserCacheRegion(userName);

            var result = await _cacheManager.GetAsync(cacheRegion, cacheRegion, async () =>
            {
                using (var userManager = _userManager)
                {
                    return await userManager.FindByNameAsync(userName);
                }
            });

            return result;
        }

        private void RemoveUserFromCache(long userId, string userName)
        {
            _cacheManager.ClearRegion(GetUserCacheRegion(userId.ToString()));
            _cacheManager.ClearRegion(GetUserCacheRegion(userName));
        }

        private ApplicationUserExtended GetUserExtended(UserEntity applicationUser, UserDetails detailsLevel)
        {
            ApplicationUserExtended result = null;
            if (applicationUser != null)
            {
                var cacheRegion = GetUserCacheRegion(applicationUser.Id.ToString());
                result = _cacheManager.Get(cacheRegion + ":" + detailsLevel, cacheRegion, () =>
                {
                    ApplicationUserExtended retVal;

                    var user = _baseUnitOfWork.BaseWorkArea.User.Query().Include(x => x.Roles).First(x => x.UserName == applicationUser.UserName);
                    var roleIds = user.Roles.Select(x => x.RoleId);
                    //角色
                    var roles = _roleManager.Roles.Where(x => roleIds.Contains(x.Id)).Select(x => x.ToCoreModel()).ToArray();
                    //模块权限
                    var modules = _baseUnitOfWork.BaseWorkArea.RoleModule.QueryFilter(x => roleIds.Contains(x.RoleId)).Select(x => x.Module).Distinct();
                    //操作权限
                    var permissions = _baseUnitOfWork.BaseWorkArea.RolePermission.QueryFilter(x => roleIds.Contains(x.RoleId)).Select(x => x.Permission).Distinct();

                    retVal = user.ToCoreModel();
                    retVal.Roles = roles;
                    retVal.Permissions = permissions.Select(rp => rp.Name).Distinct().ToArray();
                    retVal.Modules = modules.Select(x => x.EnCode).Distinct().ToArray();
                    if (detailsLevel != UserDetails.Export)
                    {
                        retVal.PasswordHash = null;
                        retVal.SecurityStamp = null;
                    }
                    return retVal;
                });
            }
            return result;
        }

        private static string GetUserCacheRegion(string userId)
        {
            return "AppUserRegion:" + userId;
        }

        private static void NormalizeUser(ApplicationUserExtended user)
        {
            if (user.UserName != null)
                user.UserName = user.UserName.Trim();

            if (user.Email != null)
                user.Email = user.Email.Trim();

            if (user.PhoneNumber != null)
                user.PhoneNumber = user.PhoneNumber.Trim();
        }
    }
}
