namespace SoccerCrud.WebApi.IntegrationTests.Controllers
{
    #region using

    using Newtonsoft.Json;
    using System.Net;
    using System.Net.Http.Headers;
    using System.Text;
    using Xunit;

    #endregion

    [Collection("Sequential")]
    public class PlayerControllerIntegrationTests :
    IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private CustomWebApplicationFactory<Program> _program;

        public PlayerControllerIntegrationTests(
            CustomWebApplicationFactory<Program> program)
        {
            _program = program;
        }

        [Fact]
        public async Task WhenPlayerControllerGetAllWithoutAuthorization_401()
        {
            // Arrange
            var application = _program.CreateClient();

            // Act
            var response = await application.GetAsync("/api/Player/all");

            // Assert

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task WhenPlayerControllerGetAllWithAuthorization_200()
        {
            // Arrange
            var client = _program.CreateClient();

            // Act

            var authData = JsonConvert.SerializeObject( 
                new { 
                    UserName = "javi.karra", 
                    Password = "javikarrapwd" 
                });

            var content = new StringContent(authData, Encoding.UTF8, "application/json");

            var authResponse = await client.PostAsync("/api/Auth/authenticate", content);

            var authResponseContent = await authResponse.Content.ReadAsStringAsync();

            var authenticateResponse = JsonConvert.DeserializeObject<AuthenticateResponse>(authResponseContent);

            var authenticationHeaderValue = new AuthenticationHeaderValue("Bearer", authenticateResponse?.Token);

            client.DefaultRequestHeaders.Authorization = authenticationHeaderValue;

            var playerResponse = await client.GetAsync("/api/Player/all");

            var playerResponseContent = await playerResponse.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject(playerResponseContent);

            // Assert

            Assert.Equal(HttpStatusCode.OK, authResponse.StatusCode);
            Assert.Equal(HttpStatusCode.OK, playerResponse.StatusCode);
            Assert.True(data is not null);
        }
    }
}
