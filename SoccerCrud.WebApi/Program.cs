using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SoccerCrud.WebApi;
using SoccerCrud.WebApi.Auth;
using SoccerCrud.WebApi.Database;
using SoccerCrud.WebApi.Database.Seeds;
using SoccerCrud.WebApi.Health;
using SoccerCrud.WebApi.Repositories;
using SoccerCrud.WebApi.Services;
using SoccerCrud.WebApi.Services.Auth;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    //c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyProject", Version = "v1.0.0" });

    var securitySchema = new OpenApiSecurityScheme
    {
        Description = "Using the Authorization header with the Bearer scheme.",
        Name = "Authorization",
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
builder.Services.AddScoped<ITokenClaimsService, TokenClaimsService>();
builder.Services.AddSingleton<IDummyUserManager, DummyUserManager>();

// Custom Services.
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();

// Repositories.
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<SoccerCrudDataContext>();
    var dataSeed = scope.ServiceProvider.GetRequiredService<IDataSeed>();

    if (app.Environment.IsDevelopment())
    {
        app.UseHttpsRedirection();

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

app.MapHealthChecks("/_health");

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();