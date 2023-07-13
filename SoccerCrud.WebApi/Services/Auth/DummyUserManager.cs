using Azure.Core;
using Microsoft.AspNetCore.Identity;

namespace SoccerCrud.WebApi.Services.Auth
{
    public class DummyUserManager : IDummyUserManager
    {
        private Dictionary<string, string> _userAndPassword = new Dictionary<string, string>()
        {
            { "javi", "karra" },
        };


        public Task<bool> CheckPasswordAsync(string userName, string password)
        {
            if (!_userAndPassword.ContainsKey(userName))
            {
                return Task.FromResult(false);
            }

            if (!_userAndPassword[userName].Equals(password))
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
    }

    public interface IDummyUserManager
    {
        public Task<bool> CheckPasswordAsync(string userName, string password);
    }
}
