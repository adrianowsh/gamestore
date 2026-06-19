using GameStore.Api.Models.Games;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidation();

WebApplication app = builder.Build();

const string GetGameEndpointName = "GetGame";

List<Genre> genres =
[
  Genre.Create("Action-adventure"),
  Genre.Create("Fighting"),
  Genre.Create("Kids and Family"),
  Genre.Create("Rolesplaying"),
  Genre.Create("Sports"),
];

List<Game> games =
[
   Game.Create("The Legend of Zelda: Breath of the Wild", "An open-world adventure game", genres.FirstOrDefault(g => g.Name == "Action-adventure")?.Id ?? string.Empty, 99.99m, new DateOnly(2017, 3, 3)),
   Game.Create("Super Mario Odyssey", "A 3D platformer game", genres.FirstOrDefault(g => g.Name == "Platform")?.Id ?? string.Empty, 120.99m, new DateOnly(2017, 10, 27)),
   Game.Create("Red Dead Redemption 2", "An epic Western action-adventure game", genres.FirstOrDefault(g => g.Name == "Action-adventure")?.Id ?? string.Empty, 150.99m, new DateOnly(2018, 10, 26))
];

app.MapGet("/", () => "Welcome to the Game Store API!");

//GEt /genres
app.MapGet("/genres", () => Results.Ok(
  genres.Select(g =>
  new GenreDto(
    g.Id,
    g.Name))));

// GET /games
app.MapGet("/games", () => Results.Ok(
    games.Select(g =>
        new GameSummaryDto(
          g.Id,
          g.Name,
          g.ReleaseDate)
        )
));

// GET /games/{id}
app.MapGet("/games/{id}", (string id) =>
{
  Game? game = games.FirstOrDefault(g => g.Id == id);

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
  Genre? genre = genres.FirstOrDefault(g => g.Id == game.GenreId);
  if (genre is null)
  {
    return Results.BadRequest($"Genre with ID '{game.GenreId}' does not exist.");
  }

  Game newGame = Game.Create(game.Name, game.Description, game.GenreId, game.Price, game.ReleaseDate);
  games.Add(newGame);

  return Results.CreatedAtRoute(
    GetGameEndpointName,
    new { id = newGame.Id },
    newGame);
});

//PUT /games/{id}
app.MapPut("/games/{id}", (string id, Game updatedGame) =>
{
  Game? existingGame = games.FirstOrDefault(g => g.Id == id);

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
  Game? game = games.FirstOrDefault(g => g.Id == id);
  if (game is null)
  {
    return Results.NotFound();
  }

  games.Remove(game);
  return Results.NoContent();
})
.WithParameterValidation();

await app.RunAsync();
