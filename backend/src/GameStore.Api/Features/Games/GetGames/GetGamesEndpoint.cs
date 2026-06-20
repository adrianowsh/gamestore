using GameStore.Api.Data;

namespace GameStore.Api.Features.Games.GetGames;

public static class GetGamesEndpoint
{
    public static void MapGetGames(this IEndpointRouteBuilder app, GameStoreData data)
    {
        app.MapGet("/games", () => Results.Ok(
            data.GetGames().Select(g =>
                new GetGamesDto(
                    g.Id,
                    g.Name,
                    g.ReleaseDate)
            )
        ))
        .WithName(EndpointNames.GetGames);
    }
}