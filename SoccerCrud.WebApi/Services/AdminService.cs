namespace SoccerCrud.WebApi.Services
{
    #region using

    using Microsoft.AspNetCore.Identity;
    using SoccerCrud.WebApi.Auth.Model;

    #endregion

    public interface IAdminService
    {
        public Task<IList<ApplicationUser>> GetUsersInRoleAsync(string role);
    }

    public class AdminService : IAdminService
    {
        #region Fiedls

        private readonly UserManager<ApplicationUser> _userManager;

        #endregion

        #region Constructor

        public AdminService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        #endregion

        #region Public Methods

        public async Task<IList<ApplicationUser>> GetUsersInRoleAsync(string role)
        {
            var roles = await _userManager.GetUsersInRoleAsync(role);

            if (!roles.Any())
            {
                return new List<ApplicationUser>();
            }

            return roles;
        }

        #endregion
    }
}
