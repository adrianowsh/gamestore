using GameStore.Api.Data;
using GameStore.Api.Models.Games;

namespace GameStore.Api.Features.Games.UpdateGame;

public static class UpdateGameEndpoint
{
    public static void MapPutGame(this IEndpointRouteBuilder app, GameStoreData gameStoreData)
    {
        app.MapPut("/games/{id}", (string id, UpdateGameDto updatedGame) =>
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
            .WithName(EndpointNames.UpdateGame)
            .WithParameterValidation();
    }
}