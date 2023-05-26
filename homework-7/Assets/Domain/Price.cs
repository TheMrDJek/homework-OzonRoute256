namespace NanoPaymentSystem.Domain;

public sealed class Price
{
    public Price(decimal amount, string currencyCode)
    {
        Amount = amount;
        CurrencyCode = currencyCode;
    }

    public decimal Amount { get; }

    public string CurrencyCode { get; }
}
