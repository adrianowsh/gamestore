using GameStore.Api.Models.Games;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{
    public static void MigrationDb(this WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();

        dbContext.Database.Migrate();
    }

    public static void SeedData(this WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();

        if (dbContext.Genres.Any()) return;

        dbContext.Genres.AddRange(
            Genre.Create("Action"),
            Genre.Create("Adventure"),
            Genre.Create("RPG"),
            Genre.Create("Strategy"),
            Genre.Create("Simulation"),
            Genre.Create("Fighting")
        );

        dbContext.SaveChanges();
    }
}