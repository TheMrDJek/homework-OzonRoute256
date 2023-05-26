using MediatR;
using NanoPaymentSystem.Application.EventStore;
using NanoPaymentSystem.Application.PaymentProvider;
using NanoPaymentSystem.Application.Repository;
using NanoPaymentSystem.Domain;
using NanoPaymentSystem.Domain.LifecycleData;

namespace NanoPaymentSystem.Application.Application.CancelPayment;

internal sealed class CancelPaymentCommandHandler : IRequestHandler<CancelPaymentCommand, CancelPaymentResult>
{
    private readonly IPaymentProvider _paymentProvider;
    private readonly IPaymentInfrastructure _paymentInfrastructure;

    public CancelPaymentCommandHandler(IPaymentProvider paymentProvider, IPaymentInfrastructure paymentInfrastructure)
    {
        _paymentProvider = paymentProvider;
        _paymentInfrastructure = paymentInfrastructure;
    }


    /// <inheritdoc />
    public async Task<CancelPaymentResult> Handle(CancelPaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = await _paymentInfrastructure.GetPaymentById(request.Id, cancellationToken);

        var cancelResult = await _paymentProvider.Cancel(new CancelPaymentRequest(payment.ProviderPaymentId!), cancellationToken);

        payment.RequestCancel(new PaymentRequestCancelData(cancelResult.Message));

        if (cancelResult.IsSuccess)
        {
            payment.Cancel(new PaymentCanceledData());
        }
        else
        {
            payment.RollbackCancel();
        }

        await _paymentInfrastructure.SavePayment(payment, cancellationToken);

        return new CancelPaymentResult(cancelResult.IsSuccess, cancelResult.Message);
    }
}
