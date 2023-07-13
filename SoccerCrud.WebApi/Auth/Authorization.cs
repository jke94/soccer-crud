namespace SoccerCrud.WebApi.Auth
{
    public static class Authorization
    {
        public static IServiceCollection AddAuthorizationLayer(this IServiceCollection services)
        {
            services.AddAuthorization();

            return services;
        }
    }
}
