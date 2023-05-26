namespace NanoPaymentSystem.Contracts;

public sealed class CreatePaymentRequest
{
    public string ClientId { get; init; } = string.Empty;

    public string OrderId { get; init; } = string.Empty;

    public decimal Amount { get; init; }

    public string CurrencyCode { get; init; } = string.Empty;
}
