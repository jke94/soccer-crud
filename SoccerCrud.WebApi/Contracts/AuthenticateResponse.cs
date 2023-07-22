namespace SoccerCrud.WebApi.Contracts
{
    public class AuthenticateResponse
    {
        public bool Succeeded { get; set; }
        public string Token { get; set; } = string.Empty;

        public string Message { get; set; } =string.Empty;
    }
}
