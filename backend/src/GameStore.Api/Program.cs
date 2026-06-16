using GameStore.Api.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
WebApplication app = builder.Build();

const string GetGameEndpointName = "GetGame";

List<Game> games =
[
  new Game
  {
    Id = $"gam_{Guid.CreateVersion7()}",
    Name = "Street Fighter V",
    Genre = "Fighting",
    Price = 59.99m,
    ReleaseDate = new DateTime(2017, 3, 3)
  },
  new Game
  {
    Id = $"gam_{Guid.CreateVersion7()}",
    Name = "Super Mario Odyssey",
    Genre = "Platform",
    Price = 120.99m,
    ReleaseDate = new DateTime(2017, 10, 27)
  },
  new Game
  {
    Id = $"gam_{Guid.CreateVersion7()}",
    Name = "Red Dead Redemption 2",
    Genre = "Action-adventure",
    Price = 150.99m,
    ReleaseDate = new DateTime(2018, 10, 26)
  }
];

// GET /games
app.MapGet("/games", () => Results.Ok(games));

// GET /games/{id}
app.MapGet("/games/{id}", (string id) =>
{
  Game? game = games.FirstOrDefault(g => g.Id == id);
  return game is not null ? Results.Ok(game) : Results.NotFound();
})
.WithName(GetGameEndpointName);

// POST /games
app.MapPost("/games", (Game game) =>
{
  var newGame = Game.Create(game.Name, game.Genre, game.Price, game.ReleaseDate);

  games.Add(newGame);

  return Results.CreatedAtRoute(
    GetGameEndpointName,
    new { id = newGame.Id },
    newGame);
})
.WithParameterValidation();

//PUT /games/{id}
app.MapPut("/games/{id}", (string id, Game updatedGame) =>
{
  Game? existingGame = games.FirstOrDefault(g => g.Id == id);
  if (existingGame is null)
  {
    return Results.NotFound();
  }

  existingGame.Name = updatedGame.Name;
  existingGame.Genre = updatedGame.Genre;
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
