using MediatR;
using NanoPaymentSystem.Application.Repository;
using NanoPaymentSystem.Domain;

namespace NanoPaymentSystem.Application.Application.QueryPayment;

internal class FindPaymentsByClientIdQueryHandler : IRequestHandler<FindPaymentsByClientIdQuery, List<Payment>>
{
    private readonly IPaymentRepository _paymentRepository;

    public FindPaymentsByClientIdQueryHandler(IPaymentRepository paymentRepository)
        => _paymentRepository = paymentRepository;

    /// <inheritdoc />
    public Task<List<Payment>> Handle(FindPaymentsByClientIdQuery request, CancellationToken cancellationToken)
        => _paymentRepository.FindPaymentsByClientId(request.ClientId, cancellationToken);
}
