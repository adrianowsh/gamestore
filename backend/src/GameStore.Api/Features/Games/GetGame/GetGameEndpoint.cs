using GameStore.Api.Data;

namespace GameStore.Api.Features.Games.GetGame;

public static class GetGameEndpoint
{
    public static void MapGetGame(this IEndpointRouteBuilder app, GameStoreData data)
    {
        app.MapGet("/games/{id}", (string id) =>
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