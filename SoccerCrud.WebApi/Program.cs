using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SoccerCrud.WebApi;
using SoccerCrud.WebApi.Auth;
using SoccerCrud.WebApi.Auth.Context;
using SoccerCrud.WebApi.Auth.Model;
using SoccerCrud.WebApi.Auth.Seeds;
using SoccerCrud.WebApi.Database;
using SoccerCrud.WebApi.Database.Seeds;
using SoccerCrud.WebApi.Health;
using SoccerCrud.WebApi.Repositories;
using SoccerCrud.WebApi.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SoccerCrud.WebApi", Version = "v1.0.0" });

    var securitySchema = new OpenApiSecurityScheme
    {
        Description = "SoccerCrud Web Api with authentication",
        Name = "soccer-crud",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    c.AddSecurityDefinition("Bearer", securitySchema);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securitySchema, new[] { "Bearer" } }
    });
});

builder.Services.AddMvc();

// Dabatase: Add services.
builder.Services.AddDatabaseServices();

// Database: Idenity.
builder.Services.AddDbContext<AppIdentityDbContext>(
    options => options.UseInMemoryDatabase("TestDatabase"))
.AddIdentityCore<ApplicationUser>( options =>
{
    options.Password.RequiredLength = 7;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<AppIdentityDbContext>();

// Database Identity: Seed data service.
builder.Services.AddScoped<IIdentityDataSeed, IdentityDataSeed>();

// Database: Seed data.
builder.Services.AddScoped<IDataSeed, DataSeed>();

// HealthChecks
builder.Services.AddHealthChecks()
    .AddCheck<DatabaseHealthCheck>("Database");

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Authentication and authorization.
builder.Services.AddAuthenticationLayer();
builder.Services.AddAuthorizationLayer();
builder.Services.AddScoped<IAuthService, AuthService>();

// Custom Services.
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();

// Repositories.
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    // Database contexts.
    var context = scope.ServiceProvider.GetRequiredService<SoccerCrudDataContext>();
    var identityContext = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();

    // Services to seed data.
    var dataSeed = scope.ServiceProvider.GetRequiredService<IDataSeed>();
    var dataSeedIdentity = scope.ServiceProvider.GetRequiredService<IIdentityDataSeed>();

    if (app.Environment.IsDevelopment())
    {
        app.UseHttpsRedirection();

        await dataSeedIdentity.SeedDataIdentity(scope);

        context.Database.EnsureDeletedAsync().Wait();
        context.Database.Migrate();
    }

    if (app.Environment.IsStaging())
    {
        await context.Database.EnsureCreatedAsync();
    }

    await dataSeed.SeedData(context);
}

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/api/SayHello", () => "Hello world!");

app.MapHealthChecks("/_health");

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();

public partial class Program { }