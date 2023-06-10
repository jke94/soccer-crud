using Microsoft.EntityFrameworkCore;
using SoccerCrud.WebApi.Database;
using SoccerCrud.WebApi.Database.Seeds;
using SoccerCrud.WebApi.Repositories;
using SoccerCrud.WebApi.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc();
builder.Services.AddDbContext<SoccerCrudDataContext>(options =>
{
    options.UseSqlite("Data Source=SoccerCrud.db");
});

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

// Seed data.
builder.Services.AddScoped<IDataSeed, DataSeed>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<SoccerCrudDataContext>();
    var dataSeed = scope.ServiceProvider.GetRequiredService<IDataSeed>();

    context.Database.EnsureDeletedAsync().Wait();
    context.Database.Migrate();
    
    await dataSeed.SeedData(context);
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseHttpsRedirection();

app.Run();

// https://dev.to/renukapatil/supercharging-aspnet-60-with-odata-crud-batching-pagination-12np