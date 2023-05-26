using MediatR;
using NanoPaymentSystem.Domain;

namespace NanoPaymentSystem.Application.Application.QueryPayment;

public sealed class FindPaymentsByClientIdQuery : IRequest<List<Payment>>
{
    public FindPaymentsByClientIdQuery(string clientId)
        => ClientId = clientId;

    public string ClientId { get; }
}
