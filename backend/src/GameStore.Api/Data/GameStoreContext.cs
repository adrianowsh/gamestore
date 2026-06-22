using GameStore.Api.Models.Games;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

public sealed class GameStoreContext : DbContext
{
    public GameStoreContext(DbContextOptions<GameStoreContext> options)
        : base(options) { }

    public DbSet<Game> Games => Set<Game>();
    public DbSet<Genre> Henres => Set<Genre>();
}
