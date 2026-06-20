using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Features.Games.UpdateGame;

public sealed record UpdateGameDto
(
    [Required]
    [StringLength(100, MinimumLength = 3)]
    string Name,

    [StringLength(500)]
    string Description,

    [Range(0.01, 1000.00)]
    decimal Price,
    [Required]
    DateOnly ReleaseDate,

    [Required]
    string GenreId
);
