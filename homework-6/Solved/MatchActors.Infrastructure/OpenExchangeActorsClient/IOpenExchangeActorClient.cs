using MatchActors.Domain.Models;

namespace MatchActors.Infrastructure.OpenExchangeActorsClient;

public interface IOpenExchangeActorClient
{
    public Task<string?> GetExchangeActorId(string actorName, CancellationToken cancellationToken);

    public Task<Movie[]?> GetExchangeActorDetails(string actorId, CancellationToken cancellationToken);
}