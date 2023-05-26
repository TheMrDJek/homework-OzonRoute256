namespace NanoPaymentSystem.Contracts;

public sealed class CardInfoResponse
{
    public CardInfoResponse(string first6, string last4, int expirationMonth, int expirationYear)
    {
        First6 = first6;
        Last4 = last4;
        ExpirationMonth = expirationMonth;
        ExpirationYear = expirationYear;
    }

    public string First6 { get; }

    public string Last4 { get; }

    public int ExpirationMonth { get; }

    public int ExpirationYear { get; }
}