using MatchActors.Domain.Models;

namespace MatchActors.Domain;

public class MatchParameters
{
    public Movie[]? FirstActorCastMovies { get; init; }
    
    public Movie[]? SecondActorCastMovies { get; init; }
    
    public bool MoviesOnly { get; init; }
}