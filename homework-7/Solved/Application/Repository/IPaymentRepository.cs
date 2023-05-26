using NanoPaymentSystem.Domain;

namespace NanoPaymentSystem.Application.Repository;

public interface IPaymentRepository
{
    Task SavePayment(Payment payment, CancellationToken cancellationToken);

    Task<Payment> GetPaymentById(Guid id, CancellationToken cancellationToken);

    Task<List<Payment>> FindPaymentsByClientId(string clientId, CancellationToken cancellationToken);

    Task<List<Payment>> FindPaymentsByOrderId(string orderId, CancellationToken cancellationToken);

    Task UpdatePayment(Payment payment, CancellationToken cancellationToken);

    Task InsertTransactionOutbox(Guid paymentId, CancellationToken cancellationToken);

    Task DeleteTransactionOutbox(Guid paymentId, CancellationToken cancellationToken);

    Task<List<PaymentOutbox>> FindByPayments(CancellationToken cancellationToken);
}
