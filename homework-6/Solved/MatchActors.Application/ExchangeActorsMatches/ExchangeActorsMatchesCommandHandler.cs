using MatchActors.Domain;
using MediatR;

namespace MatchActors.Application.ExchangeActorsMatches;

public sealed class ExchangeActorsMatchesCommandHandler : IRequestHandler<ExchangeActorsMatchesCommand, ExchangeActorsMatchResult>
{
    private readonly IMatchesResolver _matchesResolver;
    private readonly IMediator _mediator;

    public ExchangeActorsMatchesCommandHandler(
        IMatchesResolver matchesResolver, IMediator mediator)
    {
        _matchesResolver = matchesResolver;
        _mediator = mediator;

    }

    public async Task<ExchangeActorsMatchResult> Handle(ExchangeActorsMatchesCommand request, CancellationToken cancellationToken)
    {
        var firstActorMovies = await _matchesResolver.ResolveActorsMatch(request.FirstNameActor, cancellationToken);
        var secondActorMovies = await _matchesResolver.ResolveActorsMatch(request.SecondNameActor, cancellationToken);

        var exchangeResult = new ExchangeActorsMatchResult
        {
            TitleMovies = _matchesResolver.Match(new MatchParameters
            {
                FirstActorCastMovies = firstActorMovies,
                SecondActorCastMovies = secondActorMovies
            })
        };
        
        await _mediator.Publish(new ExchangeNotification
        {
            Command = request,
            Result = exchangeResult,
        }, cancellationToken);
        
        return exchangeResult;
    }
}