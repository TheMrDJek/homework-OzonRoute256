using MediatR;

namespace MatchActors.Application.ExchangeActorsMatches;

public sealed class ExchangeActorsMatchesCommand : IRequest<ExchangeActorsMatchResult>
{
    public string FirstNameActor { get; init; }
    public string SecondNameActor { get; init; }
    public bool OnlyMovie { get; init; }
}