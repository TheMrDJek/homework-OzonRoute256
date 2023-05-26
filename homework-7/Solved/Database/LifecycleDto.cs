namespace NanoPaymentSystem.Database;

internal class LifecycleDto
{
    public long Id { get; init; }

    public Guid PaymentId { get; init; }

    public int Version { get; init; }

    public int IterationType { get; init; }

    public string Data { get; init; } = string.Empty;
}
