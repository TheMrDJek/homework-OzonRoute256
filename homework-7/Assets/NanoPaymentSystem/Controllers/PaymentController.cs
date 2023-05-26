using MediatR;
using Microsoft.AspNetCore.Mvc;
using NanoPaymentSystem.Application.Application.AuthorizePayment;
using NanoPaymentSystem.Application.Application.CancelPayment;
using NanoPaymentSystem.Application.Application.CreatePayment;
using NanoPaymentSystem.Application.Application.QueryPayment;
using NanoPaymentSystem.Contracts;
using NanoPaymentSystem.Domain.Exceptions;
using PaymentStatus = NanoPaymentSystem.Contracts.PaymentStatus;

namespace NanoPaymentSystem.Controllers;

[Controller]
[Route("[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <inheritdoc />
    public PaymentController(IMediator mediator)
        => _mediator = mediator;

    [HttpPost("create")]
    public async Task<ActionResult<CreatePaymentResponse>> CreatePayment([FromBody] CreatePaymentRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreatePaymentCommand(
            clientId: request.ClientId,
            orderId: request.OrderId,
            amount: request.Amount,
            currencyCode: request.CurrencyCode), cancellationToken);

        return new ActionResult<CreatePaymentResponse>(new CreatePaymentResponse(result.PaymentId));
    }

    [HttpPost("authorize/{paymentId}")]
    public async Task<ActionResult<AuthorizePaymentResponse>> AuthorizePayment(
        Guid paymentId,
        [FromBody] AuthorizePaymentRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new AuthorizePaymentCommand(
                    id: paymentId,
                    cardNumber: request.CardNumber,
                    cardExpirationMonth: request.ExpirationMonth,
                    cardExpirationYear: request.ExpirationYear),
                cancellationToken);

            return new ActionResult<AuthorizePaymentResponse>(new AuthorizePaymentResponse(result.IsSuccess, result.Message));
        }
        catch (PaymentNotFoundException)
        {
            return NotFound();
        }
        catch (PaymentIncorrectStatusException)
        {
            return BadRequest();
        }
    }

    [HttpPost("cancel/{paymentId}")]
    public async Task<ActionResult<CancelPaymentResponse>> CancelPayment(
        Guid paymentId,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new CancelPaymentCommand(id: paymentId), cancellationToken);

            return new ActionResult<CancelPaymentResponse>(new CancelPaymentResponse(result.IsSuccess, result.Message));
        }
        catch (PaymentNotFoundException)
        {
            return NotFound();
        }
        catch (PaymentIncorrectStatusException)
        {
            return BadRequest();
        }
    }

    [HttpGet("{paymentId}")]
    public async Task<ActionResult<PaymentResponse>> GetPayment(Guid paymentId, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new GetPaymentByIdQuery(paymentId), cancellationToken);
            return new ActionResult<PaymentResponse>(new PaymentResponse(
                id: result.Id,
                clientId: result.ClientId,
                orderId: result.OrderId,
                amount: result.Price.Amount,
                currencyCode: result.Price.CurrencyCode,
                status: (PaymentStatus)(int)result.Status,
                message: result.Message,
                bankCard: result.BankCard != null
                    ? new CardInfoResponse(
                        first6: result.BankCard.First6,
                        last4: result.BankCard.Last4,
                        expirationMonth: result.BankCard.ExpirationMonth,
                        expirationYear: result.BankCard.ExpirationYear)
                    : null,
                providerPaymentId: result.ProviderPaymentId));
        }
        catch (PaymentNotFoundException)
        {
            return NotFound();
        }
    }
}