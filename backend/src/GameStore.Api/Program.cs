using GameStore.Api.Data;
using GameStore.Api.Features.Games;
using GameStore.Api.Features.Genres;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GameStoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    )
);

builder.Services.AddValidation();

WebApplication app = builder.Build();

app.MapGet("/", () => "Welcome to the Game Store API!");

app.MapGames();
app.MapGenres();

if (app.Environment.IsDevelopment())
{
    await app.InitializeDbAsync();
}

await app.RunAsync();
