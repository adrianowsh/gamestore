namespace GameStore.Api.Features.Games.GetGames;

public readonly record struct GetGamesDto
(
    string Id,
    string Name,
    DateOnly ReleaseDate
);