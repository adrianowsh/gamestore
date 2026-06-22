using GameStore.Api.Data;

namespace GameStore.Api.Features.Games.GetGame;

public static class GetGameEndpoint
{
    public static void MapGetGame(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", (string id, GameStoreData data) =>
            {
                var game = data.GetGame(id);
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