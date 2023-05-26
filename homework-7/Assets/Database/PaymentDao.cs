namespace NanoPaymentSystem.Database;

internal sealed class PaymentDao
{
    public Guid Id { get; set; }

    public string ClientId { get; set; } = string.Empty;

    public string OrderId { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public string CurrencyCode { get; set; } = string.Empty;

    public int Status { get; set; }

    public string? Message { get; set; }

    public string? BankCardFirst6 { get; set; }

    public string? BankCardLast4 { get; set; }

    public int BankCardExpirationMonth { get; set; }

    public int BankCardExpirationYear { get; set; }

    public string? ProviderPaymentId { get; set; }
}