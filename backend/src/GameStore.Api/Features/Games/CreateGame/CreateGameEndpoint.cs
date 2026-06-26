using GameStore.Api.Data;
using GameStore.Api.Models.Games;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Features.Games.CreateGame;

public static class CreateGameEndpoint
{
    public static void MapCreateGame(this IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (CreateGameDto gameDto, GameStoreContext data) =>
            {
                var genre = await data.Genres
                                .AsNoTracking()
                                .FirstOrDefaultAsync(g => g.Id == gameDto.GenreId);

                if (genre is null)
                {
                    return Results.BadRequest($"Genre with ID '{gameDto.GenreId}' does not exist.");
                }

                Game newGame = Game.Create(
                        gameDto.Name,
                        gameDto.Description,
                        gameDto.GenreId,
                        gameDto.Price,
                        gameDto.ReleaseDate);

                await data.Games.AddAsync(newGame);

                await data.SaveChangesAsync();

                return Results.CreatedAtRoute(
                    nameof(EndpointNames.GetGame),
                    new { id = newGame.Id },
                    newGame);
            })
            .WithName(EndpointNames.CreateGame)
            .WithParameterValidation();
    }
}