using MediatR;

namespace NanoPaymentSystem.Application.Application.CancelPayment;

public sealed class CancelPaymentCommand : IRequest<CancelPaymentResult>
{
    public CancelPaymentCommand(Guid id)
        => Id = id;

    public Guid Id { get; }
}
