using GameStore.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Features.Games.GetGames;

public static class GetGamesEndpoint
{
    public static void MapGetGames(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (GameStoreContext data) => Results.Ok(
            await data.Games.AsNoTracking()
                            .Select(g =>
                                new GetGamesDto(
                                    g.Id,
                                    g.Name,
                                    g.ReleaseDate)
                            ).ToListAsync()
        ))
        .WithName(EndpointNames.GetGames);
    }
}