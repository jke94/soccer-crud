namespace SoccerCrud.WebApi
{
    #region using

    using SoccerCrud.WebApi.Database;
    using Microsoft.EntityFrameworkCore;

    #endregion

    public static class DependencyInjection
    {
        #region Fiedls

        private const string SqLiteConnectionString = "Data Source=SoccerCrud.db";
        private const string SqlServerConnectionString = "Server=sqlserver-db;Database=SoccerCrud;User=sa;Password=S3cur3P@ssW0rd!;MultipleActiveResultSets=true;TrustServerCertificate=true";

        #endregion

        public static IServiceCollection AddDatabaseServices(this IServiceCollection services)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            switch (environment)
            {
                case "Development":

                    services.AddDbContext<SoccerCrudDataContext>(options =>
                    {
                        options.UseSqlite(SqLiteConnectionString);
                    });

                    break;

                case "Staging":

                    services.AddDbContext<SoccerCrudDataContext>(options =>
                    {
                        options.UseSqlServer(SqlServerConnectionString);
                    });

                    break;

                default:
                    throw new Exception("Environment not allowed.");
            }

            return services;
        }
    }
}
