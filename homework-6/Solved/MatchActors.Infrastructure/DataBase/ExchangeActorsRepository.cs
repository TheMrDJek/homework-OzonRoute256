using Dapper;
using Npgsql;

namespace MatchActors.Infrastructure.DataBase;

public sealed class ExchangeActorsRepository : IExchangeActorsRepository
{
    private readonly ExchangeActorConnectionString _connectionString;
    private readonly ICommandBuilder _commandBuilder;

    public ExchangeActorsRepository(ExchangeActorConnectionString connectionString, ICommandBuilder commandBuilder)
    {
        _connectionString = connectionString;
        _commandBuilder = commandBuilder;
    }

    public async Task<ExchangeActorInfo?> GetExchangeActor(string actorName, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(_connectionString.DatabaseConnectionString);

        await connection.OpenAsync(cancellationToken);

        return await connection.QueryFirstOrDefaultAsync<ExchangeActorInfo?>(_commandBuilder.BuildCommand(actorName, cancellationToken));
    }
}