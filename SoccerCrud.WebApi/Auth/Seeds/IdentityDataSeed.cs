namespace SoccerCrud.WebApi.Auth.Seeds
{
    #region

    using Microsoft.AspNetCore.Identity;
    using SoccerCrud.WebApi.Auth.Model;

    #endregion

    public interface IIdentityDataSeed
    {
        public Task SeedDataIdentity(IServiceScope serviceScope);
    }

    public class IdentityDataSeed : IIdentityDataSeed
    {
        public async Task SeedDataIdentity(IServiceScope serviceScope)
        {
            var scopedProvider = serviceScope.ServiceProvider;

            var logger = scopedProvider.GetRequiredService<ILogger<IdentityDataSeed>>();
            var userManager = scopedProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scopedProvider.GetRequiredService<RoleManager<IdentityRole>>();

            try
            {
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

                var adminUser = new ApplicationUser
                {
                    UserName = adminUserName,
                    Email = "admin@test.com",
                };

                var taskResultAdmin = await userManager.CreateAsync(adminUser, "adminpwd");

                if (!taskResultAdmin.Succeeded)
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
                logger?.LogError(ex, "An error occurred seeding the DB.");
            }
        }
    }
}
