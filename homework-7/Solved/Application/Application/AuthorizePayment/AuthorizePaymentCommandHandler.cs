using MediatR;
using NanoPaymentSystem.Application.PaymentProvider;
using NanoPaymentSystem.Application.Repository;
using NanoPaymentSystem.Domain;
using NanoPaymentSystem.Domain.LifecycleData;

namespace NanoPaymentSystem.Application.Application.AuthorizePayment;

internal sealed class AuthorizePaymentCommandHandler : IRequestHandler<AuthorizePaymentCommand, AuthorizePaymentResult>
{
    private readonly IPaymentInfrastructure _paymentInfrastructure;
    private readonly IPaymentProvider _paymentProvider;

    public AuthorizePaymentCommandHandler(IPaymentInfrastructure paymentInfrastructure, IPaymentProvider paymentProvider)
    {
        _paymentInfrastructure = paymentInfrastructure;
        _paymentProvider = paymentProvider;
    }

    /// <inheritdoc />
    public async Task<AuthorizePaymentResult> Handle(AuthorizePaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = await _paymentInfrastructure.GetPaymentById(request.Id, cancellationToken);

        payment.StartProcessing(new PaymentProcessingStartedData(
            new CardInfo(
                first6: request.CardNumber.Substring(0, 6),
                last4: request.CardNumber.Substring(request.CardNumber.Length - 4, 4),
                expirationMonth: request.CardExpirationMonth,
                expirationYear: request.CardExpirationYear)));

        var authResult = await _paymentProvider.Authorize(new AuthorizePaymentRequest(
                payment.Id.ToString(),
                payment.Price.Amount,
                payment.Price.CurrencyCode,
                new CardData(
                    cardNumber: request.CardNumber,
                    expirationYear: request.CardExpirationYear,
                    expirationMonth: request.CardExpirationMonth)),
            cancellationToken);

        if (authResult.IsSuccess)
        {
            payment.Authorize(new PaymentAuthorizedData(authResult.ProviderPaymentId!));
        }
        else
        {
            payment.Reject(new PaymentRejectedData(authResult.Message));
        }

        await _paymentInfrastructure.SavePayment(payment, cancellationToken);

        return new AuthorizePaymentResult(authResult.IsSuccess, authResult.Message);

    }
}
