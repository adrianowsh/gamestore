using GameStore.Api.Data;
using GameStore.Api.Features.Games;
using GameStore.Api.Features.Genres;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddSqlite<GameStoreContext>(builder.Configuration.GetConnectionString("Database"));
builder.Services.AddValidation();
builder.Services.AddSingleton<GameStoreData>();

WebApplication app = builder.Build();

app.MapGet("/", () => "Welcome to the Game Store API!");

app.MapGames();
app.MapGenres();

if (app.Environment.IsDevelopment())
{
    app.MigrationDb();
    app.SeedData();
}

await app.RunAsync();
