namespace SoccerCrud.WebApi.Controllers
{
    #region using

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SoccerCrud.WebApi.Auth;
    using SoccerCrud.WebApi.Services.Auth;
    using System.Security.Claims;

    #endregion

    public class AuthController : ControllerBase
    {
        #region Fields

        private readonly ITokenClaimsService _tokenClaimsService;
        private readonly UserManager<ApplicationUser> _userManager;

        #endregion

        #region Constructor

        public AuthController(
            ITokenClaimsService tokenClaimsService,
            UserManager<ApplicationUser> userManager)
        {
            _tokenClaimsService = tokenClaimsService;
            _userManager = userManager;
        }

        #endregion

        #region HTTP GET Methods

        [HttpGet("currentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var nameIdentifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var role = User.FindFirstValue(ClaimTypes.Role);
            var firstName = User.FindFirstValue("firstname");
            var lastName = User.FindFirstValue("lastname");

            return Ok(new { nameIdentifier, userName, role, firstName, lastName });
        }

        #endregion

        #region HTTP POST Methods

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(
            [FromBody] AuthenticateRequest request)
        {
            var response = new AuthenticateResponse();

            if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("User or password it´s empty.");
            }

            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var succeeded = await _userManager.CheckPasswordAsync(user, request.Password);

            if (succeeded)
            {
                response.Succeeded = succeeded;
                response.Token = await _tokenClaimsService.GetTokenAsync(request.UserName);

                return Ok(response);
            }

            return Unauthorized(response);
        }

        #endregion
    }

    public class AuthenticateRequest
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }

    public class AuthenticateResponse
    {
        public bool Succeeded { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
