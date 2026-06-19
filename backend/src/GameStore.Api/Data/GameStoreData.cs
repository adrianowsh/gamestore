using GameStore.Api.Models.Games;

namespace GameStore.Api.Data;

public sealed class GameStoreData
{
    private readonly List<Genre> _genres;
    private readonly List<Game> _games;

    public GameStoreData()
    {
        _genres =
            [
                Genre.Create("Action-adventure"),
                Genre.Create("Fighting"),
                Genre.Create("Kids and Family"),
                Genre.Create("Platform"),
                Genre.Create("Role-playing"),
                Genre.Create("Sports"),
            ];

        _games =
        [
            Game.Create("The Legend of Zelda: Breath of the Wild", "An open-world adventure game",
                _genres.FirstOrDefault(g => g.Name == "Action-adventure")?.Id ?? string.Empty, 99.99m,
                new DateOnly(2017, 3, 3)),
            Game.Create("Super Mario Odyssey", "A 3D platformer game",
                _genres.FirstOrDefault(g => g.Name == "Platform")?.Id ?? string.Empty, 120.99m,
                new DateOnly(2017, 10, 27)),
            Game.Create("Red Dead Redemption 2", "An epic Western action-adventure game",
                _genres.FirstOrDefault(g => g.Name == "Action-adventure")?.Id ?? string.Empty, 150.99m,
                new DateOnly(2018, 10, 26)),
            Game.Create("Grand Theft Auto V", "An open-world action-adventure game",
                _genres.FirstOrDefault(g => g.Name == "Action-adventure")?.Id ?? string.Empty, 59.99m,
                new DateOnly(2013, 9, 17))
        ];
    }


    /// <summary>
    /// In a real application, these methods would interact with a database or other data storage mechanism to retrieve and manipulate game and genre data. For simplicity, this example uses in-memory lists to store the data.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Game> GetGames() => _games;

    /// <summary>
    /// In a real application, these methods would interact with a database or other data storage mechanism to retrieve and manipulate game and genre data. For simplicity, this example uses in-memory lists to store the data.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Genre> GetGenres() => _genres;

    /// <summary>
    /// In a real application, these methods would interact with a database or other data storage mechanism to retrieve and manipulate game and genre data. For simplicity, this example uses in-memory lists to store the data.
    /// </summary>
    /// <param name="gameId"></param>
    /// <returns></returns>
    public Game? GetGame(string gameId) => _games.FirstOrDefault(g => g.Id == gameId);

    /// <summary>
    /// In a real application, these methods would interact with a database or other data storage mechanism to retrieve and manipulate game and genre data. For simplicity, this example uses in-memory lists to store the data.
    /// </summary>
    /// <param name="game"></param>
    public void AddGame(Game game) => _games.Add(game);

    /// <summary>
    /// In a real application, these methods would interact with a database or other data storage mechanism to retrieve and manipulate game and genre data. For simplicity, this example uses in-memory lists to store the data.
    /// </summary>
    /// <param name="game"></param>
    public void RemoveGame(Game game)
    {
        Game? existingGame = GetGame(game.Id);
        if (existingGame is not null)
        {
            _games.Remove(existingGame);
        }
    }
}