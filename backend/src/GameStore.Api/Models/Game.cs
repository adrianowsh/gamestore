using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Models;

public sealed class Game
{
    public string Id { get; set; } = string.Empty;

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(20, MinimumLength = 3)]
    public string Genre { get; set; } = string.Empty;

    [Range(0.01, 1000.00)]
    public decimal Price { get; set; }
    public DateTime ReleaseDate { get; set; }

    public static Game Create(string name, string genre, decimal price, DateTime releaseDate)
    {
        return new Game
        {
            Id = $"gam_{Guid.CreateVersion7()}",
            Name = name,
            Genre = genre,
            Price = price,
            ReleaseDate = releaseDate
        };
    }
}
