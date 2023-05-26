using MediatR;
using NanoPaymentSystem.Application.EventStore;
using NanoPaymentSystem.Domain;

namespace NanoPaymentSystem.Application.Repository;

internal class PaymentInfrastructure : IPaymentInfrastructure
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IEventStore _eventStore;
    private readonly IMediator _mediator;

    public PaymentInfrastructure(IPaymentRepository paymentRepository, IEventStore eventStore, IMediator mediator)
    {
        _paymentRepository = paymentRepository;
        _eventStore = eventStore;
        _mediator = mediator;
    }

    /// <inheritdoc />
    public async Task SavePayment(Payment payment, CancellationToken cancellationToken)
    {
        await _eventStore.Save(payment.Lifecycle.GetTransient(), cancellationToken);

        if (payment.IsTransient())
        {
            await _paymentRepository.SavePayment(payment, cancellationToken);
        }
        else
        {
            await _paymentRepository.UpdatePayment(payment, cancellationToken);
        }

        foreach (var paymentDomainEvent in payment.DomainEvents)
        {
            await _mediator.Publish(paymentDomainEvent, cancellationToken);
        }
    }

    /// <inheritdoc />
    public async Task<Payment> GetPaymentById(Guid id, CancellationToken cancellationToken)
    {
        var lifecycle =await _eventStore.LoadEvents(id, cancellationToken);
        return Payment.Load(lifecycle.ToList());
    }

    /// <inheritdoc />
    public Task<List<Payment>> FindPaymentsByClientId(string clientId, CancellationToken cancellationToken)
        => _paymentRepository.FindPaymentsByClientId(clientId, cancellationToken);

    /// <inheritdoc />
    public Task<List<Payment>> FindPaymentsByOrderId(string orderId, CancellationToken cancellationToken)
        => _paymentRepository.FindPaymentsByOrderId(orderId, cancellationToken);
}
