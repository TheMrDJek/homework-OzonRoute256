namespace MatchActors.Domain.Exceptions;

public class ActorsMatchNotFoundException : Exception
{
    public ActorsMatchNotFoundException()
    {
    }

    public ActorsMatchNotFoundException(string message)
        : base(message)
    {
    }

    public ActorsMatchNotFoundException(string message, Exception innerException) :
        base(message, innerException)
    {
    }
}