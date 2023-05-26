using MediatR;

namespace MatchActors.Application.ExchangeActorsMatches;

public sealed class ExchangeNotification : INotification
{
    public ExchangeActorsMatchesCommand Command { get; init; } = new();

    public ExchangeActorsMatchResult Result { get; init; } = new();
}