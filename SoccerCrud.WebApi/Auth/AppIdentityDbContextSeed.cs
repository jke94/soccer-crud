namespace SoccerCrud.WebApi.Auth
{
    #region using

    using Microsoft.AspNetCore.Identity;

    #endregion

    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var scopedProvider = scope.ServiceProvider;

            try
            {
                var userManager = scopedProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scopedProvider.GetRequiredService<RoleManager<IdentityRole>>();

                await roleManager.CreateAsync(new IdentityRole("Administrator"));

                // Create basic users

                var userA = new ApplicationUser 
                { 
                    UserName = "javi.karra",
                    Email = "javi.karra@mycompany.com",
                };

                var tasResultA = await userManager.CreateAsync(userA, "javikarrapwd");

                if (!tasResultA.Succeeded)
                {
                    throw new Exception($"User {userA.UserName} not created.");
                }

                var userB = new ApplicationUser
                {
                    UserName = "lucas",
                    Email = "lucas@mycompany.com",
                };

                var tasResultB = await userManager.CreateAsync(userB, "lucaspwd");

                if (!tasResultB.Succeeded)
                {
                    throw new Exception($"User {userB.UserName} not created.");
                }

                // Create Admin user and assign administrator role to user.

                string adminUserName = "admin";

                var adminUser = new ApplicationUser { 
                    UserName = adminUserName, 
                    Email = "admin@test.com",
                };

               var taskResultAdmin = await userManager.CreateAsync(adminUser, "adminpwd");

                if(!taskResultAdmin.Succeeded)
                {
                    throw new Exception($"User {adminUser.UserName} not created.");
                }

                adminUser = await userManager.FindByNameAsync(adminUserName);

                if (adminUser == null)
                {
                    throw new Exception($"User not created.");
                }

                await userManager.AddToRoleAsync(adminUser, "Administrator");
            }
            catch (Exception ex)
            {
                app.Logger.LogError(ex, "An error occurred seeding the DB.");
            }
        }

    }
}
