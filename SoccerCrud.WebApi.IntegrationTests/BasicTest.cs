namespace SoccerCrud.WebApi.IntegrationTests
{
    #region using

    using Xunit;

    #endregion

    [Collection("Sequential")]
    public class BasicTest : 
        IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private CustomWebApplicationFactory<Program> _program;

        public BasicTest(CustomWebApplicationFactory<Program> program)
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
    }
}