using GameStore.Api.Data;

namespace GameStore.Api.Features.Genres.GetGenres;

public static class GetGenresEndpoint
{
    public static void MapGetGenres(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", (GameStoreContext data) => Results.Ok(
              data.Genres.Select(g =>
                  new GenreDto(g.Id, g.Name))
          ))
          .WithName(EndpointNames.GetGenres);
    }
}
