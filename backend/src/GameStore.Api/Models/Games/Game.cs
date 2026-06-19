using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Models.Games;

public sealed class Game
{
    public string Id { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [StringLength(20, MinimumLength = 3)]
    public string GenreId { get; set; } = string.Empty;

    [Range(0.01, 1000.00)]
    public decimal Price { get; set; }
    public DateOnly ReleaseDate { get; set; }

    public static Game Create(
        string name,
        string description,
        string genreId,
        decimal price,
        DateOnly releaseDate)
    {
        return new Game
        {
            Id = $"gam_{Guid.CreateVersion7()}",
            Name = name,
            Description = description,
            GenreId = genreId,
            Price = price,
            ReleaseDate = releaseDate
        };
    }
}
