namespace SoccerCrud.WebApi.IntegrationTests.Health
{
    #region using

    using Xunit;

    #endregion

    [Collection("Sequential")]
    public class DatabaseHealthCheckIntegrationTests :
        IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private CustomWebApplicationFactory<Program> _program;

        public DatabaseHealthCheckIntegrationTests(
            CustomWebApplicationFactory<Program> program)
        {
            _program = program;
        }

        [Fact]
        public async Task TestHealthCheck()
        {
            // Arrange
            var application = _program.CreateClient();

            // Act
            var response = await application.GetStringAsync("/_health");

            // Assert

            Assert.Equal("Healthy", response);
        }
    }
}
