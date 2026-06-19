namespace GameStore.Api.Models.Games;

public readonly record struct GameDetailDto
{
    public GameDetailDto(string id, string name, string description, decimal price, DateOnly releaseDate, string genreId)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        ReleaseDate = releaseDate;
        GenreId = genreId;
    }
    public string Id { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public DateOnly ReleaseDate { get; init; }
    public string GenreId { get; init; } = string.Empty;

    public static GameDetailDto Create(string Id, string Name, string Description, decimal Price, DateOnly ReleaseDate, string GenreId)
            => new(Id, Name, Description, Price, ReleaseDate, GenreId);
};


