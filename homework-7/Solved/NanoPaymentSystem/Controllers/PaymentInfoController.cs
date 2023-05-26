using MediatR;
using Microsoft.AspNetCore.Mvc;
using NanoPaymentSystem.Application.Application.QueryPayment;
using NanoPaymentSystem.Contracts;

namespace NanoPaymentSystem.Controllers;

[Controller]
[Route("[controller]")]
public class PaymentInfoController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <inheritdoc />
    public PaymentInfoController(IMediator mediator)
        => _mediator = mediator;

    [HttpGet("byClient/{clientId}")]
    public async Task<ActionResult<List<PaymentResponse>>> FindByClientId(string clientId, CancellationToken cancellationToken)
    {
        var payments = await _mediator.Send(new FindPaymentsByClientIdQuery(clientId), cancellationToken);

        return Ok(payments);
    }

    [HttpGet("byOrder/{orderId}")]
    public async Task<ActionResult<List<PaymentResponse>>> FindByOrderId(string orderId, CancellationToken cancellationToken)
    {
        var payments = await _mediator.Send(new FindPaymentsByOrderIdQuery(orderId), cancellationToken);

        return Ok(payments);
    }
}
