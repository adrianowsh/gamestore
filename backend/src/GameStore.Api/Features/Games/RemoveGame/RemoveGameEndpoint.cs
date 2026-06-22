using GameStore.Api.Data;
using GameStore.Api.Models.Games;

namespace GameStore.Api.Features.Games.RemoveGame;

public static class RemoveGameEndpoint
{
    public static void MapDeleteGame(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id}", (string id, GameStoreData gameStoreData) =>
        {
            Game? game = gameStoreData.GetGame(id);
            if (game is null)
            {
                return Results.NotFound();
            }

            gameStoreData.RemoveGame(game);
            return Results.NoContent();
        })
        .WithName(EndpointNames.DeleteGame)
        .WithParameterValidation();
    }
}