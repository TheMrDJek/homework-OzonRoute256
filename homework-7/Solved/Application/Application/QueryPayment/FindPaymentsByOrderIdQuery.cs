using MediatR;
using NanoPaymentSystem.Domain;

namespace NanoPaymentSystem.Application.Application.QueryPayment;

public sealed class FindPaymentsByOrderIdQuery : IRequest<List<Payment>>
{
    public FindPaymentsByOrderIdQuery(string orderId)
        => OrderId = orderId;

    public string OrderId { get; }
}
