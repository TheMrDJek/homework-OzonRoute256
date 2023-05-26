using MatchActors.Domain;
using MatchActors.Domain.Exceptions;
using MatchActors.Domain.Models;
using MatchActors.Infrastructure.DataBase;
using MatchActors.Infrastructure.OpenExchangeActorsClient;

namespace MatchActors.Application;

public class CachedMatchesResolver : IMatchesResolver
{
    private readonly IExchangeActorsRepository _actorsRepository;
    private readonly IOpenExchangeActorClient _exchangeActorClient;

    public CachedMatchesResolver(IExchangeActorsRepository actorsRepository, IOpenExchangeActorClient exchangeActorClient)
    {
        _actorsRepository = actorsRepository;
        _exchangeActorClient = exchangeActorClient;

    }

    public async Task<Movie[]?> ResolveActorsMatch(string actorName, CancellationToken cancellationToken)
    {
        var resultFromDb = await _actorsRepository.GetExchangeActor(actorName, cancellationToken);

        if (resultFromDb == null)
        {
            return Array.Empty<Movie>();
        }

        var resultFromExchangeActorId = await _exchangeActorClient.GetExchangeActorId(actorName, cancellationToken);

        if (resultFromExchangeActorId == null)
        {
            throw new ActorsMatchNotFoundException($"Actor '{actorName}' wa not found");
        }

        return await _exchangeActorClient.GetExchangeActorDetails(resultFromExchangeActorId, cancellationToken);

    }
    
    public List<string>? Match(MatchParameters matchParameters)
    {
        if (!matchParameters.MoviesOnly)
        {
            return (from firstActorCastMovie in matchParameters.FirstActorCastMovies
                from secondActorCastMovie in matchParameters.SecondActorCastMovies
                where firstActorCastMovie.Id == secondActorCastMovie.Id
                select firstActorCastMovie.Title).ToList();
        }

        var firstActorCastMovies = matchParameters.FirstActorCastMovies
            .Where(v => v.Role is "Actress" or "Actor");
        
        var secondActorCastMovies = matchParameters.SecondActorCastMovies
            .Where(v => v.Role is "Actress" or "Actor");
            
        return (from firstActorCastMovie in firstActorCastMovies 
            from secondActorCastMovie in secondActorCastMovies 
            where firstActorCastMovie.Id == secondActorCastMovie.Id 
            select firstActorCastMovie.Title).ToList();

    }
}