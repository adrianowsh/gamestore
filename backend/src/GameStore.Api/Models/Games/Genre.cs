using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Models.Games;

public sealed class Genre
{
  private Genre(string id, string name)
  {
    Id = id;
    Name = name;
  }

  [Required]
  public string Id { get; set; }
  [Required]
  [StringLength(20, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 20 characters.")]
  public string Name { get; set; } = null!;

  public static Genre Create(string name) =>
    new($"gen_{Guid.CreateVersion7()}", name);

}
