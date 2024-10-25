using System.Net;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using CreditCard.Application.ViewModels;
using CreditCard.Application.Queries.GetAllCreditCard;
using CreditCard.Application.Queries.GetCustomersById;
using CreditCard.Application.Queries.GetCreditCardByCustomerId;
using Swashbuckle.AspNetCore.Annotations;

namespace CreditCard.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "v1")]
[Route("api/v{version:apiVersion}/credit-card")]
public class CreditCardController : ControllerBase
{
    private readonly IMediator _mediator;

    public CreditCardController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet()]
    [SwaggerOperation(Summary = "Busca os cartoes de crédito disponíveis).")]
    [ProducesResponseType(typeof(IEnumerable<CreditCardViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetAll([FromQuery] Guid customerId)
    {
        var result = await _mediator.Send(new GetCreditCardByCustomerIdQuery(customerId));
        return Ok(result);
    }

    [HttpGet("customers")]
    [SwaggerOperation(Summary = "Busca todos os cartoes de crédito de um cliente.")]
    [ProducesResponseType(typeof(IEnumerable<CreditCardViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetByCustomerId([FromQuery] Guid customerId)
    {
        var result = await _mediator.Send(new GetCustomerByIdQuery(customerId));
        return result != null ? Ok(result) : NotFound();
    }
}
