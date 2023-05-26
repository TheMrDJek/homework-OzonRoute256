namespace NanoPaymentSystem.Application.PaymentProvider;

public sealed class CardData
{
    public CardData(string cardNumber, int expirationYear, int expirationMonth)
    {
        CardNumber = cardNumber;
        ExpirationYear = expirationYear;
        ExpirationMonth = expirationMonth;
    }

    public string CardNumber { get; }

    public int ExpirationYear { get; }

    public int ExpirationMonth { get; }
}
