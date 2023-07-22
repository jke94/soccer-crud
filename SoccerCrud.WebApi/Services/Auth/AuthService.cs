namespace SoccerCrud.WebApi.Services.Auth
{
    #region using

    using Microsoft.AspNetCore.Identity;
    using Microsoft.IdentityModel.Tokens;
    using SoccerCrud.WebApi.Auth.Model;
    using SoccerCrud.WebApi.Contracts;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Text;

    #endregion

    public interface IAuthService
    {
        public Task<AuthenticateResponse> AuthenticatenticateAsync(AuthenticateRequest request);
    }

    public class AuthService : IAuthService
    {
        #region Fiedls

        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        #endregion

        #region Constructor

        public AuthService(
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        #endregion 

        #region Public Methods

        public async Task<AuthenticateResponse> AuthenticatenticateAsync(AuthenticateRequest request)
        {
            var response = new AuthenticateResponse();

            if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
            {
                response.Succeeded = false;
                response.Message = "User or password it´s empty.";

                return response;
            }

            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                response.Succeeded = false;
                response.Message = "User not found.";

                return response;
            }

            var succeeded = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!succeeded)
            {
                response.Succeeded = false;
                response.Message = "Password is wrong";

                return response;
            }

            response.Succeeded = succeeded;
            response.Token = await GetTokenAsync(request.UserName);
            response.Message = "OK";

            return response;
        }

        #endregion

        #region Private Methods

        private async Task<string> GetTokenAsync(string userName)
        {
            var jwtSettingsKey = _configuration["JwtSettings:Key"];

            if (jwtSettingsKey == null)
            {
                throw new ArgumentNullException(nameof(jwtSettingsKey));
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSettingsKey);
            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];
            var user = await _userManager.FindByNameAsync(userName);

            if (user is null)
            {
                throw new ArgumentNullException($"{nameof(user)} is null.");
            }

            var roles = await _userManager.GetRolesAsync(user);

            var rolesAsStr = string.Join(",", roles);

            var claims = new List<Claim>
            { 
                new Claim(ClaimTypes.Sid, user.Id),
                new Claim(ClaimTypes.Name, user.UserName ?? "NAME_NOT_FOUND"),
                new Claim(ClaimTypes.Email, user.Email ?? "EMAIL_NOT_FOUND"),
                new Claim(ClaimTypes.GivenName, $"{user.LastName}, {user.FirstName}"),
                new Claim("firstname", user.FirstName),
                new Claim("lastname", user.LastName),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber?? "PHONENUMBER_NOT_FOUND"),
                new Claim(ClaimTypes.Role, rolesAsStr)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = audience,
                Subject = new ClaimsIdentity(claims.ToArray()),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = signingCredentials
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        #endregion
    }
}
