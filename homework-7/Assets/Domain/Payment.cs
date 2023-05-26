using MediatR;
using NanoPaymentSystem.Domain.DomainEvents;
using NanoPaymentSystem.Domain.Exceptions;
using NanoPaymentSystem.Domain.LifecycleData;
using NanoPaymentSystem.Domain.Lifecycles;

namespace NanoPaymentSystem.Domain;

public sealed class Payment
{
    public readonly LifecycleCollection Lifecycle = new();

    public readonly List<INotification> DomainEvents = new();
 
    private Payment()
    {
        ClientId = string.Empty;
        OrderId = string.Empty;
        Price = new Price(0, string.Empty);
    }

    public Payment(PaymentCreatedData createdData)
    {
        Id = Guid.NewGuid();
        ClientId = createdData.ClientId;
        OrderId = createdData.OrderId;
        Price = createdData.Price;
        Status = PaymentStatus.New;

        Lifecycle.Add(new PaymentCreatedIteration(Id, Lifecycle.GetNextVersion(), PaymentStatus.New, createdData));

        DomainEvents.Add(new PaymentCreatedEvent(PaymentStatus.New, Id, createdData));
    }

    public void StartProcessing(PaymentProcessingStartedData processingStartedData)
    {
        Status = PaymentStatus.Processing;
        BankCard = processingStartedData.BankCard;

        Lifecycle.Add(new PaymentProcessingStartedIteration(Id, Lifecycle.GetNextVersion(), PaymentStatus.Processing, processingStartedData));

        DomainEvents.Add(new PaymentProcessingStartedEvent(PaymentStatus.Processing, Id, processingStartedData));
    }

    public void Authorize(PaymentAuthorizedData authorizedData)
    {
        Status = PaymentStatus.Authorized;
        ProviderPaymentId = authorizedData.ProviderPaymentId;

        Lifecycle.Add(new PaymentAuthorizedIteration(Id, Lifecycle.GetNextVersion(), PaymentStatus.Authorized, authorizedData));

        DomainEvents.Add(new PaymentAuthorizedEvent(
            status: PaymentStatus.Authorized,
            paymentId: Id,
            @event: authorizedData));
    }

    public void Reject(PaymentRejectedData paymentRejectedData)
    {
        Status = PaymentStatus.Rejected;
        Message = paymentRejectedData.Message;

        Lifecycle.Add(new PaymentRejectedIteration(Id, Lifecycle.GetNextVersion(), PaymentStatus.Rejected, paymentRejectedData));

        DomainEvents.Add(new PaymentRejectedEvent(PaymentStatus.Rejected, Id, paymentRejectedData));
    }

    public void RequestCancel(PaymentRequestCancelData paymentRequestCancelData)
    {
        Status = PaymentStatus.CancelRequested;
        Message = paymentRequestCancelData.Message;

        Lifecycle.Add(new PaymentRequestCancelIteration(Id, Lifecycle.GetNextVersion(), PaymentStatus.CancelRequested, paymentRequestCancelData));
    }

    public void Cancel(PaymentCanceledData paymentCanceledData)
    {
        Status = PaymentStatus.Cancelled;

        Lifecycle.Add(new PaymentCanceledIteration(Id, Lifecycle.GetNextVersion(), PaymentStatus.Cancelled, paymentCanceledData));

        DomainEvents.Add(new PaymentCancelledEvent(PaymentStatus.Cancelled, Id, paymentCanceledData));
    }

    public static Payment Load(List<PaymentLifecycle> lifecycle)
    {
        if (lifecycle.Count == 0)
        {
            throw new PaymentNotFoundException();
        }

        var payment = new Payment();

        payment.Lifecycle.Load(lifecycle);

        foreach (var lifetimeIteration in lifecycle)
        {
            payment.ApplyIteration(lifetimeIteration);
        }

        return payment;
    }

    public void RollbackCancel()
    {
        Status = PaymentStatus.Authorized;

        Lifecycle.Add(new PaymentAuthorizedIteration(
            Id,
            Lifecycle.GetNextVersion(),
            PaymentStatus.Authorized,
            new PaymentAuthorizedData(ProviderPaymentId!)));
    }

    private void ApplyIteration(PaymentLifecycle paymentLifecycle)
    {
        switch (paymentLifecycle.LifecycleType)
        {
            case PaymentStatus.New:
                var createdData = (PaymentCreatedData)paymentLifecycle.Data;
                Id = paymentLifecycle.EntityId;
                ClientId = createdData.ClientId;
                OrderId = createdData.OrderId;
                Price = createdData.Price;
                Status = PaymentStatus.New;
                return;
            case PaymentStatus.Processing:
                var processingData = (PaymentProcessingStartedData)paymentLifecycle.Data;
                Status = PaymentStatus.Processing;
                BankCard = processingData.BankCard;
                return;
            case PaymentStatus.Authorized:
                var authorizedData = (PaymentAuthorizedData)paymentLifecycle.Data;
                Status = PaymentStatus.Authorized;
                ProviderPaymentId = authorizedData.ProviderPaymentId;
                break;
            case PaymentStatus.Rejected:
                var rejectedData = (PaymentRejectedData)paymentLifecycle.Data;
                Status = PaymentStatus.Rejected;
                Message = rejectedData.Message;
                break;
            case PaymentStatus.Cancelled:
                var _ = (PaymentCanceledData)paymentLifecycle.Data;
                Status = PaymentStatus.Cancelled;
                break;
            case PaymentStatus.CancelRequested:
                var cancelRequestedData = (PaymentRequestCancelData)paymentLifecycle.Data;
                Status = PaymentStatus.CancelRequested;
                Message = cancelRequestedData.Message;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public Guid Id { get; private set; }

    public string ClientId { get; private set; }

    public string OrderId { get; private set; }

    public Price Price { get; private set; }

    public PaymentStatus Status { get; private set; }

    public string? Message { get; private set; }

    public CardInfo? BankCard { get; private set; }

    public string? ProviderPaymentId { get; private set; }

    public bool IsTransient()
        => Status == PaymentStatus.New && !Lifecycle.GetTransient().Any();
}