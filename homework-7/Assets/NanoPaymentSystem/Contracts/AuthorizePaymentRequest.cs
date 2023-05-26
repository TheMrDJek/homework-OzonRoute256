namespace NanoPaymentSystem.Contracts;

public class AuthorizePaymentRequest
{
    public string CardNumber { get; init; } = string.Empty;

    public int ExpirationYear { get; init; }

    public int ExpirationMonth { get; init; }
}
