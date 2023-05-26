namespace MatchActors.Application.ExchangeActorsMatches;

public class MatchActorsRequest
{
    public string? FirstActor { get; init; }
    public string? SecondActor { get; init; }
    public bool MoviesOnly { get; init; }
}