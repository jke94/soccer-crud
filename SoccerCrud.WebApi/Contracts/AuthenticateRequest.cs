namespace SoccerCrud.WebApi.Contracts
{
    public class AuthenticateRequest
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
