using GameStore.Api.Models.Games;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public sealed class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();
    public DbSet<Genre> Genres => Set<Genre>();
}
