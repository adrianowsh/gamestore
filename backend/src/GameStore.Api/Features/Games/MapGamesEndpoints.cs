using GameStore.Api.Data;
using GameStore.Api.Features.Games.CreateGame;
using GameStore.Api.Features.Games.GetGame;
using GameStore.Api.Features.Games.GetGames;
using GameStore.Api.Features.Games.RemoveGame;
using GameStore.Api.Features.Games.UpdateGame;

namespace GameStore.Api.Features.Games;

public static class MapGamesEndpoints
{
    public static void MapGames(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("/games");
        group.MapGetGames();
        group.MapGetGame();
        group.MapCreateGame();
        group.MapPutGame();
        group.MapDeleteGame();
    }
}
