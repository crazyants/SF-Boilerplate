using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SF.Entitys;
using SF.Core.StartupTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Data
{
    public class CoreEFStartupTask : IStartupTask
    {
        private readonly IServiceProvider _serviceProvider;

        public CoreEFStartupTask(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

        }
        public int Priority { get { return 1; } }

        public async Task Run()
        {
            using (var serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<CoreDbContext>();

                // ran into an error when I made a change to the model and tried to apply the migration
                // found the reason was this line:
                //bool didCreatedDb = await db.Database.EnsureCreatedAsync();
                // according to https://github.com/aspnet/EntityFramework/issues/3160
                // EnsureCreatedAsync totally bypasses migrations and just creates the schema for you, you can't mix this with migrations. 
                // EnsureCreated is designed for testing or rapid prototyping where you are ok with dropping and re-creating 
                // the database each time. If you are using migrations and want to have them automatically applied on app start, 
                // then you can use context.Database.Migrate() instead.
                try
                {
                    await db.Database.EnsureCreatedAsync();
                }
                catch (System.NotImplementedException)
                {
                    db.Database.EnsureCreated();
                }

                await EnsureData(db);

            }

        }

        private async Task EnsureData(CoreDbContext db)
        {
            int rowsAffected = 0;

            int count = await db.Set<SiteSettings>().CountAsync<SiteSettings>();
            SiteSettings newSite = null;
            if (count == 0)
            {
                // create first site
                newSite = InitialData.BuildInitialSite();

                db.Set<SiteSettings>().Add(newSite);

                rowsAffected = await db.SaveChangesAsync();

            }
            // ensure roles
            count = await db.Roles.CountAsync();
            if (count == 0)
            {
                var site = newSite;
                if (site == null)
                {
                    site = await db.Set<SiteSettings>().SingleOrDefaultAsync<SiteSettings>(
                        s => s.Id != 0 && s.IsServerAdminSite == true);
                }

                if (site != null)
                {

                    var roleAdminRole = InitialData.BuildAdminRole();
                    roleAdminRole.SiteId = site.Id;
                    db.Roles.Add(roleAdminRole);

                    var contentAdminRole = InitialData.BuildContentAdminsRole();
                    contentAdminRole.SiteId = site.Id;
                    db.Roles.Add(contentAdminRole);

                    var authenticatedUserRole = InitialData.BuildAuthenticatedRole();
                    authenticatedUserRole.SiteId = site.Id;
                    db.Roles.Add(authenticatedUserRole);

                    rowsAffected = await db.SaveChangesAsync();

                }
            }
            // ensure admin user
            count = await db.Users.CountAsync();
            if (count == 0)
            {
                SiteSettings site = await db.Set<SiteSettings>().FirstOrDefaultAsync<SiteSettings>(
                    s => s.Id != 0 && s.IsServerAdminSite == true);

                if (site != null)
                {
                    var role = await db.Roles.FirstOrDefaultAsync(
                            x => x.SiteId == site.Id && x.NormalizedName == "ADMINISTRATORS");

                    if (role != null)
                    {
                        var adminUser = InitialData.BuildInitialAdmin();
                        adminUser.SiteId = site.Id;
                        db.Users.Add(adminUser);

                        rowsAffected = await db.SaveChangesAsync();

                        if (rowsAffected > 0 && adminUser.Id != 0)
                        {
                            var ur = new UserRoleEntity();
                            ur.RoleId = role.Id;
                            ur.UserId = adminUser.Id;
                            db.UserRoles.Add(ur);
                            await db.SaveChangesAsync();

                            role = await db.Roles.SingleOrDefaultAsync(
                                 x => x.SiteId == site.Id && x.NormalizedName == "AuthenticatedUsers".ToUpperInvariant());

                            if (role != null)
                            {
                                ur = new UserRoleEntity();
                                ur.RoleId = role.Id;
                                ur.UserId = adminUser.Id;
                                db.UserRoles.Add(ur);
                                await db.SaveChangesAsync();
                            }


                        }
                    }

                }

            }
        }

    }
}
