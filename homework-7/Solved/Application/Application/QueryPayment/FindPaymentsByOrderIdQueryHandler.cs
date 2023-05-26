using MediatR;
using NanoPaymentSystem.Application.Repository;
using NanoPaymentSystem.Domain;

namespace NanoPaymentSystem.Application.Application.QueryPayment;

internal class FindPaymentsByOrderIdQueryHandler : IRequestHandler<FindPaymentsByOrderIdQuery, List<Payment>>
{
    private readonly IPaymentRepository _paymentRepository;

    public FindPaymentsByOrderIdQueryHandler(IPaymentRepository paymentRepository)
        => _paymentRepository = paymentRepository;

    /// <inheritdoc />
    public Task<List<Payment>> Handle(FindPaymentsByOrderIdQuery request, CancellationToken cancellationToken)
        => _paymentRepository.FindPaymentsByOrderId(request.OrderId, cancellationToken);
}
