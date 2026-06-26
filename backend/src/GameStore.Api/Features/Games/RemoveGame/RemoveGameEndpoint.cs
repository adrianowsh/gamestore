using GameStore.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Features.Games.RemoveGame;

public static class RemoveGameEndpoint
{
    public static void MapDeleteGame(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id}", async (string id, GameStoreContext gameStoreContext) =>
            {
                await gameStoreContext.Games
                    .Where(g => g.Id == id)
                    .ExecuteDeleteAsync();

                return Results.NoContent();
            })
        .WithName(EndpointNames.DeleteGame)
        .WithParameterValidation();
    }
}