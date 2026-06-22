using GameStore.Api.Data;
using GameStore.Api.Features.Genres.GetGenres;

namespace GameStore.Api.Features.Genres;

public static class MapGenresEndpoint
{
    public static void MapGenres(this IEndpointRouteBuilder app)
    {
        RouteGroupBuilder group = app.MapGroup("/genres");
        group.MapGetGenres();
    }
}
