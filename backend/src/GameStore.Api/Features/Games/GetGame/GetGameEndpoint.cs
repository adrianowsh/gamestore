using GameStore.Api.Data;
using GameStore.Api.Models.Games;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Features.Games.GetGame;

public static class GetGameEndpoint
{
    public static void MapGetGame(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", async (string id, GameStoreContext data) =>
            {
                Game? game = await data.Games
                                .AsNoTracking()
                                .FirstOrDefaultAsync(g => g.Id == id);

                if (game is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(new GetGameDto(
                    game.Id,
                    game.Name,
                    game.Description,
                    game.Price,
                    game.ReleaseDate,
                    game.GenreId));
            })
            .WithName(EndpointNames.GetGame);
    }
}