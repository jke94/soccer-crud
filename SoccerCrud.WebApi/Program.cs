using Microsoft.EntityFrameworkCore;
using SoccerCrud.WebApi;
using SoccerCrud.WebApi.Database;
using SoccerCrud.WebApi.Database.Seeds;
using SoccerCrud.WebApi.Health;
using SoccerCrud.WebApi.Repositories;
using SoccerCrud.WebApi.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc();

// Dabatase: Add services.
builder.Services.AddDatabaseServices();

// Database: Seed data.
builder.Services.AddScoped<IDataSeed, DataSeed>();

builder.Services.AddHealthChecks()
    .AddCheck<DatabaseHealthCheck>("Database");

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

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

app.MapHealthChecks("/_health");

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();