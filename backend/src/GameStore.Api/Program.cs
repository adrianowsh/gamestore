using GameStore.Api.Data;
using GameStore.Api.Models.Games;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidation();

WebApplication app = builder.Build();

const string GetGameEndpointName = "GetGame";

//fake database
GameStoreData gameStoreData = new();

app.MapGet("/", () => "Welcome to the Game Store API!");

//GEt /genres
app.MapGet("/genres", () => Results.Ok(
  gameStoreData.GetGenres().Select(g =>
  new GenreDto(
    g.Id,
    g.Name))));

// GET /games
app.MapGet("/games", () => Results.Ok(
    gameStoreData.GetGames().Select(g =>
        new GameSummaryDto(
          g.Id,
          g.Name,
          g.ReleaseDate)
        )
));

// GET /games/{id}
app.MapGet("/games/{id}", (string id) =>
{
    Game? game = gameStoreData.GetGame(id);

    GameDetailDto result = GameDetailDto.Create(
      game?.Id ?? string.Empty,
      game?.Name ?? string.Empty,
      game?.Description ?? string.Empty,
      game?.Price ?? 0m,
      game?.ReleaseDate ?? DateOnly.MinValue,
      game?.GenreId ?? string.Empty
    );

    return game is not null ? Results.Ok(result) : Results.NotFound();
})
.WithName(GetGameEndpointName);

// POST /games
app.MapPost("/games", (CreateGame game) =>
{
    Genre? genre = gameStoreData.GetGenres().FirstOrDefault(g => g.Id == game.GenreId);
    if (genre is null)
    {
        return Results.BadRequest($"Genre with ID '{game.GenreId}' does not exist.");
    }

    Game newGame = Game.Create(game.Name, game.Description, game.GenreId, game.Price, game.ReleaseDate);
    gameStoreData.AddGame(newGame);

    return Results.CreatedAtRoute(
      GetGameEndpointName,
      new { id = newGame.Id },
      newGame);
});

//PUT /games/{id}
app.MapPut("/games/{id}", (string id, Game updatedGame) =>
{
    Game? existingGame = gameStoreData.GetGame(id);

    if (existingGame is null)
        return Results.NotFound();

    existingGame.Name = updatedGame.Name;
    existingGame.Description = updatedGame.Description;
    existingGame.GenreId = updatedGame.GenreId;
    existingGame.Price = updatedGame.Price;
    existingGame.ReleaseDate = updatedGame.ReleaseDate;

    return Results.NoContent();
})
.WithParameterValidation();

//DELETE /games/{id}
app.MapDelete("/games/{id}", (string id) =>
{
    Game? game = gameStoreData.GetGame(id);
    if (game is null)
    {
        return Results.NotFound();
    }

    gameStoreData.RemoveGame(game);
    return Results.NoContent();
})
.WithParameterValidation();

await app.RunAsync();
