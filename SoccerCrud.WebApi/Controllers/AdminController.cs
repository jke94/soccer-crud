namespace SoccerCrud.WebApi.Controllers
{
    #region using

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SoccerCrud.WebApi.Services;

    #endregion

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        #region Fields

        #region Fiedls

        private readonly IAdminService _adminService;

        #endregion

        #endregion

        #region Constructor

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        #endregion

        #region HTTP GET Methods

        [HttpGet("SayHello")]
        public async Task<IActionResult> SayHello()
        {
            var message = await Task.FromResult("Hello! I am logued as administrator");
            
            return Ok(message);
        }

        [HttpGet("GetUsersByRole")]
        public async Task<IActionResult> GetUsersByRole(string role)
        {
            if(string.IsNullOrEmpty(role))
            {
                return BadRequest("Role is null or empty");
            }

            var roles = await _adminService.GetUsersInRoleAsync(role);

            if (!roles.Any())
            {
                return NotFound();
            }

            return Ok(roles);
        }

        #endregion
    }
}
