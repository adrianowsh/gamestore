using GameStore.Api.Data;
using GameStore.Api.Features.Games;

namespace GameStore.Api.Features.Genres.GetGenres;

public static class GetGenresEndpoint
{
    public static void MapGetGenres(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", (GameStoreData data) => Results.Ok(
              data.GetGenres().Select(g =>
                  new GenreDto(g.Id, g.Name))
          ))
          .WithName(EndpointNames.GetGenres);
    }
}
