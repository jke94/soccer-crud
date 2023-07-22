namespace SoccerCrud.WebApi.IntegrationTests
{
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    #region using

    using Xunit;

    #endregion

    public class BasicTests : 
        IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private CustomWebApplicationFactory<Program> _program;

        public BasicTests(CustomWebApplicationFactory<Program> program)
        {
            _program = program;
        }


        [Fact]
        public async Task TestSayHello()
        {
            // Arrange
            var application = _program.CreateClient();

            // Act
            var response = await application.GetStringAsync("/api/SayHello");

            // Assert

            Assert.Equal("Hello world!", response);
        }

        [Fact]
        public async Task TestHealthCheck()
        {
            // Arrange
            var application = _program.CreateClient();

            // Act
            var response = await application.GetStringAsync("/_health");

            // Assert

            Assert.Equal(HealthCheckResult.Healthy().Status.ToString(), response);
        }
    }
}