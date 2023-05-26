using MatchActors.Domain;
using MatchActors.Domain.Models;

namespace MatchActors.Application;

public interface IMatchesResolver
{
    public Task<Movie[]?> ResolveActorsMatch(string actorName, CancellationToken cancellationToken);

    public List<string>? Match(MatchParameters matchParameters);
}