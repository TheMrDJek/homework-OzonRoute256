namespace NanoPaymentSystem.Application.PaymentProvider;

public sealed class AuthorizePaymentRequest
{
    public AuthorizePaymentRequest(string paymentId, decimal amount, string currencyCode, CardData bankCardData)
    {
        PaymentId = paymentId;
        Amount = amount;
        CurrencyCode = currencyCode;
        BankCardData = bankCardData;
    }

    public string PaymentId { get; }

    public decimal Amount { get; }

    public string CurrencyCode { get; }

    public CardData BankCardData { get; }
}