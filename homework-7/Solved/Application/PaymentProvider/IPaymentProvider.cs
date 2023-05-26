namespace NanoPaymentSystem.Application.PaymentProvider;

public interface IPaymentProvider
{
    Task<AuthorizePaymentResponse> Authorize(AuthorizePaymentRequest request, CancellationToken cancellationToken);

    Task<CancelPaymentResponse> Cancel(CancelPaymentRequest request, CancellationToken cancellationToken);
}