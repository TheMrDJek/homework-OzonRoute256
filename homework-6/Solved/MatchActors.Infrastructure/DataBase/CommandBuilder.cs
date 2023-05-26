using Dapper;

namespace MatchActors.Infrastructure.DataBase;

public sealed class CommandBuilder : ICommandBuilder
{
    private const string BaseQuery = "select actor_id from actors where name = @actorName";

    public CommandDefinition BuildCommand(string actorName, CancellationToken cancellationToken)
    {
        return new CommandDefinition(BaseQuery, new {actorName}, cancellationToken: cancellationToken);
    }
}