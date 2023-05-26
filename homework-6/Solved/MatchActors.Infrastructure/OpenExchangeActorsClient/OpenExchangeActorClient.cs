using System.Net.Http.Json;
using MatchActors.Domain.Models;

namespace MatchActors.Infrastructure.OpenExchangeActorsClient;

public sealed class OpenExchangeActorClient : IOpenExchangeActorClient
{
    private readonly HttpClient _httpClient;
    private readonly OpenExchangeActorsOptions _exchangeActorsOptions;

    public OpenExchangeActorClient(HttpClient httpClient, OpenExchangeActorsOptions exchangeActorsOptions)
    {
        _httpClient = httpClient;
        _exchangeActorsOptions = exchangeActorsOptions;
    }

    public async Task<string?> GetExchangeActorId(string actorName, CancellationToken cancellationToken)
    {
        var exchangeActorsResponse = await _httpClient.GetFromJsonAsync<OpenExchangeSearchActorsDto>(BuildActorQuery(actorName), cancellationToken);

        return exchangeActorsResponse?.Results?.FirstOrDefault()?.Id;
    }
    
    public async Task<Movie[]?> GetExchangeActorDetails(string actorId, CancellationToken cancellationToken)
    {
        var exchangeActorsDetailsResponse = await _httpClient.GetFromJsonAsync<OpenExchangeSearchActorDetailsDto>(BuildActorDetailsQuery(actorId), cancellationToken);

        return exchangeActorsDetailsResponse?.CastMovies;
    }

    private string BuildActorQuery(string actorName)
    {
        return $"{_exchangeActorsOptions.BaseUrl}/SearchName/{_exchangeActorsOptions.AppId}/{actorName}";
    }
    
    private string BuildActorDetailsQuery(string actorId)
    {
        return $"{_exchangeActorsOptions.BaseUrl}/Name/{_exchangeActorsOptions.AppId}/{actorId}";
    }
}