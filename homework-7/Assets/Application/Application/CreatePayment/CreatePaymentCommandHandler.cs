using MediatR;
using NanoPaymentSystem.Application.Repository;
using NanoPaymentSystem.Domain;
using NanoPaymentSystem.Domain.LifecycleData;

namespace NanoPaymentSystem.Application.Application.CreatePayment;

internal sealed class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, CreatePaymentResult>
{
    private readonly IPaymentInfrastructure _paymentInfrastructure;

    public CreatePaymentCommandHandler(IPaymentInfrastructure paymentInfrastructure)
    {
        _paymentInfrastructure = paymentInfrastructure;
    }

    /// <inheritdoc />
    public async Task<CreatePaymentResult> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = new Payment(new PaymentCreatedData(
            request.ClientId,
            request.OrderId,
            new Price(request.Amount, request.CurrencyCode)));

        await _paymentInfrastructure.SavePayment(payment, cancellationToken);

        return new CreatePaymentResult(payment.Id.ToString());
    }
}
