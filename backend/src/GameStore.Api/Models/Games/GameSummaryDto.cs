namespace GameStore.Api.Models.Games;

public readonly record struct GameSummaryDto
(
    string Id,
    string Name,
    DateOnly ReleaseDate
);

