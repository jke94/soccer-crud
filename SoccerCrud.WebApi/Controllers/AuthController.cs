namespace SoccerCrud.WebApi.Controllers
{
    #region using

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SoccerCrud.WebApi.Contracts;
    using SoccerCrud.WebApi.Services;
    using System.Security.Claims;

    #endregion

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        #region Fields

        private readonly IAuthService _authService;

        #endregion

        #region Constructor

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        #endregion

        #region HTTP GET Methods

        [Authorize]
        [HttpGet("LoguedUser")]
        public IActionResult LoguedUser()
        {
            var id = User.FindFirstValue(ClaimTypes.Sid);
            var givenName = User.FindFirstValue(ClaimTypes.GivenName);
            var mail = User.FindFirstValue(ClaimTypes.Email);
            var phone = User.FindFirstValue(ClaimTypes.MobilePhone);
            var role = User.FindFirstValue(ClaimTypes.Role);
            var firstName = User.FindFirstValue("firstname");
            var lastName = User.FindFirstValue("lastname");

            return Ok(new
            {
                id,
                givenName,
                mail,
                phone,
                role,
                firstName,
                lastName
            });
        }

        #endregion

        #region HTTP POST Methods

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(
            [FromBody] AuthenticateRequest request)
        {
            var response = await _authService.AuthenticatenticateAsync(request);

            if (!response.Succeeded)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser(
            [FromBody] CreateUserDto createUserDto)
        {
            if (createUserDto == null)
            {
                return BadRequest($"{nameof(createUserDto)} is null.");
            }

            var response = await _authService.CreateUserDto(createUserDto);

            if (!response.Succeeded)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }

        #endregion
    }
}
