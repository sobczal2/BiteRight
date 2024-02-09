using System;
using System.Threading.Tasks;
using BiteRight.Application.Commands.Products.Create;
using BiteRight.Application.Commands.Products.Dispose;
using BiteRight.Application.Queries.Products.ListCurrent;
using BiteRight.Web.Authorization;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BiteRight.Web.Controllers;

public class ProductsController : WebController
{
    public ProductsController(
        IMediator mediator
    )
        : base(mediator)
    {
    }

    [HttpPost]
    [AuthorizeUserExists]
    [ProducesResponseType(typeof(CreateResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(
        [FromBody] CreateRequest request
    )
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }

    [HttpGet("current")]
    [AuthorizeUserExists]
    [ProducesResponseType(typeof(ListCurrentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ListCurrent(
        [FromQuery] ListCurrentRequest request
    )
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }

    [HttpPost("{productId:guid}/dispose")]
    [AuthorizeUserExists]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Dispose(Guid productId)
    {
        var request = new DisposeRequest(productId);
        var response = await Mediator.Send(request);
        return Ok(response);
    }
}