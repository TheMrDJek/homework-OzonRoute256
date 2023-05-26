using MatchActors.Domain.Models;

namespace MatchActors.Infrastructure.OpenExchangeActorsClient;

public sealed class OpenExchangeSearchActorsDto
{
    public Actor[]? Results { get; init; }
}