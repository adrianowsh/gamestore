using GameStore.Api.Data;
using GameStore.Api.Features.Games;
using GameStore.Api.Features.Genres;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// builder.Services.AddDbContext<GameStoreContext>(options =>
//     options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSqlite<GameStoreContext>(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddValidation();
builder.Services.AddTransient<GameStoreData>();

WebApplication app = builder.Build();

app.MapGet("/", () => "Welcome to the Game Store API!");

app.MapGames();
app.MapGenres();

await app.RunAsync();
