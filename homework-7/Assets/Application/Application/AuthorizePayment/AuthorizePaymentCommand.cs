using MediatR;

namespace NanoPaymentSystem.Application.Application.AuthorizePayment;

public sealed class AuthorizePaymentCommand : IRequest<AuthorizePaymentResult>
{
    public AuthorizePaymentCommand(Guid id, string cardNumber, int cardExpirationMonth, int cardExpirationYear)
    {
        Id = id;
        CardNumber = cardNumber;
        CardExpirationMonth = cardExpirationMonth;
        CardExpirationYear = cardExpirationYear;
    }

    public Guid Id { get; }

    public string CardNumber { get; }

    public int CardExpirationMonth { get; }

    public int CardExpirationYear { get; }
}
