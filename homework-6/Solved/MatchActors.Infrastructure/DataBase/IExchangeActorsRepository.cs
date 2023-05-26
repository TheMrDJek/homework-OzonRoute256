namespace MatchActors.Infrastructure.DataBase;

public interface IExchangeActorsRepository
{
    public Task<ExchangeActorInfo?> GetExchangeActor(string actorName, CancellationToken cancellationToken);
}