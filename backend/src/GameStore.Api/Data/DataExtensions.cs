using GameStore.Api.Models.Games;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public static class DataExtensions
{
    private static async Task MigrationDbAsync(this WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();

        await dbContext.Database.MigrateAsync();
    }

    private static async Task SeedDataAsync(this WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();

        if (await dbContext.Genres.AnyAsync()) return;

        dbContext.Genres.AddRange(
            Genre.Create("Action"),
            Genre.Create("Adventure"),
            Genre.Create("RPG"),
            Genre.Create("Strategy"),
            Genre.Create("Simulation"),
            Genre.Create("Fighting")
        );

        await dbContext.SaveChangesAsync();
    }

    public static async Task InitializeDbAsync(this WebApplication app)
    {
        await app.MigrationDbAsync();
        await app.SeedDataAsync();
    }
}