namespace SoccerCrud.WebApi.Auth.Seeds
{
    #region

    using Microsoft.AspNetCore.Identity;
    using SoccerCrud.WebApi.Auth.Model;

    #endregion

    public interface IIdentityDataSeed
    {
        #region Methods

        public Task SeedDataIdentity(IServiceScope serviceScope);

        #endregion
    }

    public class IdentityDataSeed : IIdentityDataSeed
    {
        #region Public Methods

        public async Task SeedDataIdentity(IServiceScope serviceScope)
        {
            var scopedProvider = serviceScope.ServiceProvider;

            var logger = scopedProvider.GetRequiredService<ILogger<IdentityDataSeed>>();
            var userManager = scopedProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scopedProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roles = GetListOfRolesToSeed();
            var users = GetListOfUsersToSeed();

            try
            {
                // 1. ASP.NET Core Identity: Add roles.
                foreach (var role in roles)
                {
                    // Asp Net Identity: Create roles to assign user after.
                    var createdRole = await roleManager.CreateAsync(new IdentityRole(role.RoleName));

                    if (!createdRole.Succeeded)
                    {
                        throw new Exception($"role {role.RoleName} not created.");
                    }
                }

                // 2. ASP.NET Core Identity: Add users and assigning roles.
                foreach (var user in users)
                {
                    var applicationUser = new ApplicationUser()
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                    };

                    // ASP.NET Core Identity: Create user.
                    var createUserTaskResult = await userManager.CreateAsync(applicationUser, user.Password);

                    if (!createUserTaskResult.Succeeded)
                    {
                        throw new Exception($"User {applicationUser.UserName} not created.");
                    }

                    // ASP.NET Core Identity: Finding user to assign role.
                    var foundUser = await userManager.FindByNameAsync(user.UserName);

                    if (foundUser == null)
                    {
                        throw new Exception($"User not created.");
                    }

                    // ASP.NET Core Identity: Assigning role to user.
                    var rolledUser = await userManager.AddToRoleAsync(applicationUser, user.Role);

                    if (!rolledUser.Succeeded)
                    {
                        throw new Exception($"{user.UserName} is not enrolled.");
                    }
                }
            }
            catch (Exception exception)
            {
                logger?.LogError($"An error occurred seeding the DB: {exception.Message}");
            }
        }

        #endregion

        #region Private Methods

        private IList<UserToSeed> GetListOfUsersToSeed()
        {
            return new List<UserToSeed>()
            {
                new UserToSeed()
                {
                    UserName = "javi.karra",
                    Password = "javikarrapwd",
                    Email = "javi.karra@mycompany.com",
                    FirstName = "Javi",
                    LastName = "Karra",
                    PhoneNumber = "1234567890",
                    Role = RoleDefinition.USER_ROLE,
                },
                new UserToSeed()
                {
                    UserName = "lucas.perez",
                    Password = "lucasperezpwd",
                    Email = "lucas.perez@mycompany.com",
                    FirstName = "Lucas",
                    LastName = "Perez",
                    PhoneNumber = "6234567890",
                    Role = RoleDefinition.USER_ROLE,
                },
                new UserToSeed()
                {
                    UserName = "admin",
                    Password = "adminpwd",
                    Email = "admin@test.com",
                    FirstName = "Tony",
                    LastName = "Tuz",
                    PhoneNumber = "623455699",
                    Role = RoleDefinition.ADMINISTRATOR_ROLE,
                }
            };
        }

        private IList<RolesToSeed> GetListOfRolesToSeed()
        {
            return new List<RolesToSeed>()
            {
                new RolesToSeed() { RoleName = RoleDefinition.ADMINISTRATOR_ROLE },
                new RolesToSeed() { RoleName = RoleDefinition.USER_ROLE }
            };
        }

        #endregion
    }
}
