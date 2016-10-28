using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleFramework.Core.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleFramework.Core.Data
{
    public static class CoreEFStartup
    {
        public static async Task InitializeDatabaseAsync(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
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

                //try
                //{
                await db.Database.MigrateAsync();
                //}
                //catch { }

                await EnsureData(db);

            }

        }
        private static async Task EnsureData(CoreDbContext db)
        {
            int rowsAffected = 0;

            // ensure roles
            int count = await db.Roles.CountAsync();
            if (count == 0)
            {

                var adminRole = InitialData.BuildAdminRole();
                db.Roles.Add(adminRole);

                rowsAffected = await db.SaveChangesAsync();


            }
        }

    }
}
