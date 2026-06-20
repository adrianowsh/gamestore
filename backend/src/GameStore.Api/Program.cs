using GameStore.Api.Data;
using GameStore.Api.Features.Games.CreateGame;
using GameStore.Api.Features.Games.GetGame;
using GameStore.Api.Features.Games.GetGames;
using GameStore.Api.Features.Games.RemoveGame;
using GameStore.Api.Features.Games.UpdateGame;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidation();

WebApplication app = builder.Build();

//fake database
GameStoreData gameStoreData = new();

app.MapGet("/", () => "Welcome to the Game Store API!");

//GEt /genres
app.MapGetGames(gameStoreData);

// GET /games
app.MapGetGames(gameStoreData);

// GET /games/{id}
app.MapGetGame(gameStoreData);

// POST /games
app.MapCreateGame(gameStoreData);

//PUT /games/{id}
app.MapPutGame(gameStoreData);

//DELETE /games/{id}
app.MapDeleteGame(gameStoreData);

await app.RunAsync();
