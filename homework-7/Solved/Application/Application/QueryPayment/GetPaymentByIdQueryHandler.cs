using MediatR;
using NanoPaymentSystem.Application.Repository;
using NanoPaymentSystem.Domain;

namespace NanoPaymentSystem.Application.Application.QueryPayment;

internal sealed class GetPaymentByIdQueryHandler : IRequestHandler<GetPaymentByIdQuery, Payment>
{
    private readonly IPaymentInfrastructure _paymentInfrastructure;

    public GetPaymentByIdQueryHandler(IPaymentInfrastructure paymentInfrastructure)
    {
        _paymentInfrastructure = paymentInfrastructure;
    }

    /// <inheritdoc />
    public Task<Payment> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
        => _paymentInfrastructure.GetPaymentById(request.PaymentId, cancellationToken);
}
