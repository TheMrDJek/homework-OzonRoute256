using Dapper;
using MatchActors.Application;
using MatchActors.Application.ExchangeActorsMatches;
using MatchActors.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Npgsql;

namespace MatchActors.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActorsMatchController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <inheritdoc />
        public ActorsMatchController(IMediator mediator)
            => _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<MatchActorsResponse>> Post([FromBody] MatchActorsRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new ExchangeActorsMatchesCommand()
            {
                FirstNameActor = request.FirstActor,
                SecondNameActor = request.SecondActor,
                OnlyMovie = request.MoviesOnly
            }, cancellationToken);

            return new ActionResult<MatchActorsResponse>(new MatchActorsResponse
            {
                TitleMovies = result.TitleMovies,
            });
        }
    }
}
