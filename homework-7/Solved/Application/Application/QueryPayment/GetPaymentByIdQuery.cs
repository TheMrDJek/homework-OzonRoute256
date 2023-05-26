using MediatR;
using NanoPaymentSystem.Domain;

namespace NanoPaymentSystem.Application.Application.QueryPayment;

public sealed class GetPaymentByIdQuery : IRequest<Payment>
{
    public GetPaymentByIdQuery(Guid paymentId) => PaymentId = paymentId;

    public Guid PaymentId { get; }
}
