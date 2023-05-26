using NanoPaymentSystem.Application.PaymentProvider;

namespace NanoPaymentSystem.PaymentProviders;

internal sealed class FakePaymentProvider : IPaymentProvider
{
    /// <inheritdoc />
    public Task<AuthorizePaymentResponse> Authorize(AuthorizePaymentRequest request, CancellationToken cancellationToken)
    {
        if (request.Amount > 100_000)
        {
            return Task.FromResult(new AuthorizePaymentResponse(false, "Insufficient funds", null));
        }

        if (request.Amount < 1_000)
        {
            return Task.FromResult(new AuthorizePaymentResponse(false, "General decline", null));
        }

        return Task.FromResult(new AuthorizePaymentResponse(true, "Success", Guid.NewGuid().ToString("D")));
    }

    /// <inheritdoc />
    public Task<CancelPaymentResponse> Cancel(CancelPaymentRequest request, CancellationToken cancellationToken)
    {
        var random = new Random().Next(0, 99);

        if (random < 100)
        {
            return Task.FromResult(new CancelPaymentResponse(false, "Cancel failed"));
        }

        return Task.FromResult(new CancelPaymentResponse(true, "Cancel succeeded"));
    }
}
