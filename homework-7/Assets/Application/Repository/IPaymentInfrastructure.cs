using NanoPaymentSystem.Domain;

namespace NanoPaymentSystem.Application.Repository;

internal interface IPaymentInfrastructure
{
    Task SavePayment(Payment payment, CancellationToken cancellationToken);

    Task<Payment> GetPaymentById(Guid id, CancellationToken cancellationToken);

    Task<List<Payment>> FindPaymentsByClientId(string clientId, CancellationToken cancellationToken);

    Task<List<Payment>> FindPaymentsByOrderId(string orderId, CancellationToken cancellationToken);
}
