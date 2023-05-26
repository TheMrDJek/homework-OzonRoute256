using MatchActors.Domain.Models;

namespace MatchActors.Infrastructure.OpenExchangeActorsClient;

public sealed class OpenExchangeSearchActorDetailsDto
{
    public Movie[]? CastMovies { get; set; }
}