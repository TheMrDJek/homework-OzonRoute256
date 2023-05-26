using Dapper;

namespace MatchActors.Infrastructure.DataBase;

public interface ICommandBuilder
{
    public CommandDefinition BuildCommand(string actorName, CancellationToken cancellationToken);
}