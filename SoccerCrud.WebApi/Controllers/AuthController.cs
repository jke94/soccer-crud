using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SoccerCrud.WebApi.Services.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SoccerCrud.WebApi.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly ITokenClaimsService _tokenClaimsService;
        private readonly IDummyUserManager _dummyUserManager;

        public AuthController(
            ITokenClaimsService tokenClaimsService,
            IDummyUserManager dummyUserManager
            )
        {
            _tokenClaimsService = tokenClaimsService;
            _dummyUserManager = dummyUserManager;
        }

        //[HttpGet("currentUser")]
        //[Authorize]
        //public async Task<IActionResult> GetCurrentUser() =>
        //    Ok(User.Identity.IsAuthenticated ?
        //        await UserInfo.CreateUserInfo(User, 
        //            await _tokenClaimsService.GetTokenAsync(User.Identity.Name))
        //        : UserInfo.Anonymous);

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(
            [FromBody] AuthenticateRequest request)
        {
            var response = new AuthenticateResponse();

            if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("User or password it´s empty.");
            }

            var succeeded = await _dummyUserManager.CheckPasswordAsync(
                request.UserName, 
                request.Password);

            if (succeeded)
            {
                response.Succeeded = succeeded;
                response.Token = await _tokenClaimsService.GetTokenAsync(request.UserName);

                return Ok(response);
            }

            return Unauthorized(response);
        }
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
