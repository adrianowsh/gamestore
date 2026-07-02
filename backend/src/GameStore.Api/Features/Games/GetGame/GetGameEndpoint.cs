using GameStore.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Features.Games.GetGame;

public static class GetGameEndpoint
{
    public static void MapGetGame(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", async (string id, GameStoreContext data, ILogger<Program> logger) =>
            {
                var game = await data.Games
                    .AsNoTracking()
                    .FirstOrDefaultAsync(g => g.Id == id);

                if (game is null)
                {
                    throw new Exception("Database is not available");
                }

                return Results.Ok(new GetGameDto(
                    game.Id,
                    game.Name,
                    game.Description,
                    game.Price,
                    game.ReleaseDate,
                    game.GenreId));

                //catch (Exception ex)
                //{
                //    var traceId = Activity.Current?.TraceId;

                //    logger.LogError(
                //        ex,
                //        "Could not process on machine {Machine} - TraceId: {TraceId}",
                //        Environment.MachineName,
                //        traceId);

                //    return Results.Problem(
                //        title: "An error occured while processing your request",
                //        statusCode: StatusCodes.Status500InternalServerError,
                //        extensions: new Dictionary<string, object?>()
                //        {
                //            { "traceId", traceId.ToString() }
                //        }
                // );
                //}
            })
            .WithName(EndpointNames.GetGame);
    }
}