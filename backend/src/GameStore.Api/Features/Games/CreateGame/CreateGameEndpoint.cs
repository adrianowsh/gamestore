using GameStore.Api.Data;
using GameStore.Api.Models.Games;

namespace GameStore.Api.Features.Games.CreateGame;

public static class CreateGameEndpoint
{
    public static void MapCreateGame(this IEndpointRouteBuilder app, GameStoreData data)
    {
        app.MapPost("/games", (CreateGameDto gameDto) =>
            {
                Genre? genre = data.GetGenres().FirstOrDefault(g => g.Id == gameDto.GenreId);
                if (genre is null)
                {
                    return Results.BadRequest($"Genre with ID '{gameDto.GenreId}' does not exist.");
                }

                Game newGame = Game.Create(
                        gameDto.Name,
                        gameDto.Description,
                        gameDto.GenreId,
                        gameDto.Price,
                        gameDto.ReleaseDate);

                data.AddGame(newGame);

                return Results.CreatedAtRoute(
                    nameof(EndpointNames.GetGame),
                    new { id = newGame.Id },
                    newGame);
            })
            .WithName(EndpointNames.CreateGame)
            .WithParameterValidation();
    }
}