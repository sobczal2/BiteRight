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

    /// <summary>
    /// Create a new product.
    /// </summary>
    /// <param name="request"></param>
    /// <returns>New product id.</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /Products
    ///     {
    ///       "Name": "Example Product",
    ///       "Description": "This is an example product description.",
    ///       "Price": 19.99,
    ///       "CurrencyId": "3B56A6DE-3B41-4B10-934F-469CA12F4FE3",
    ///       "ExpirationDate": "2024-12-31",
    ///       "ExpirationDateKind": 2,
    ///       "CategoryId": "E8C78317-70AC-4051-805E-ECE2BB37656F",
    ///       "maximumAmountValue": 100,
    ///       "amountUnitId": "B6D4D4DD-C035-4047-B8EE-48937CB1F368"
    ///     }
    ///
    /// </remarks>
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