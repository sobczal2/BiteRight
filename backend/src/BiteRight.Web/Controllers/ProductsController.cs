// # ==============================================================================
// # Solution: BiteRight
// # File: ProductsController.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using System.Threading;
using System.Threading.Tasks;
using BiteRight.Application.Commands.Products.ChangeAmount;
using BiteRight.Application.Commands.Products.Create;
using BiteRight.Application.Commands.Products.Delete;
using BiteRight.Application.Commands.Products.Dispose;
using BiteRight.Application.Commands.Products.Edit;
using BiteRight.Application.Commands.Products.Restore;
using BiteRight.Application.Dtos.Products;
using BiteRight.Application.Queries.Products.GetDetails;
using BiteRight.Application.Queries.Products.ListCurrent;
using BiteRight.Application.Queries.Products.Search;
using BiteRight.Web.Authorization;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

#endregion

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
    ///     Create a new product.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>New product id.</returns>
    /// <remarks>
    ///     Sample request:
    ///     POST /Products
    ///     {
    ///     "Name": "Example Product",
    ///     "Description": "This is an example product description.",
    ///     "Price": 19.99,
    ///     "CurrencyId": "3B56A6DE-3B41-4B10-934F-469CA12F4FE3",
    ///     "ExpirationDate": "2024-12-31",
    ///     "ExpirationDateKind": 2,
    ///     "CategoryId": "E8C78317-70AC-4051-805E-ECE2BB37656F",
    ///     "maximumAmountValue": 100,
    ///     "amountUnitId": "B6D4D4DD-C035-4047-B8EE-48937CB1F368"
    ///     }
    /// </remarks>
    [HttpPost]
    [AuthorizeUserExists]
    [ProducesResponseType(typeof(CreateResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(
        [FromBody] CreateRequest request,
        CancellationToken cancellationToken
    )
    {
        var response = await Mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpGet("current")]
    [AuthorizeUserExists]
    [ProducesResponseType(typeof(ListCurrentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ListCurrent(
        [FromQuery] ProductSortingStrategy sortingStrategy,
        CancellationToken cancellationToken
    )
    {
        var request = new ListCurrentRequest
        {
            SortingStrategy = sortingStrategy
        };
        var response = await Mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPut("{productId:guid}/dispose")]
    [AuthorizeUserExists]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Dispose(
        [FromRoute] Guid productId,
        CancellationToken cancellationToken
    )
    {
        var request = new DisposeRequest(productId);
        var response = await Mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPut("{productId:guid}/restore")]
    [AuthorizeUserExists]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Restore(
        [FromRoute] Guid productId,
        CancellationToken cancellationToken
    )
    {
        var request = new RestoreRequest(productId);
        var response = await Mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPost("search")]
    [AuthorizeUserExists]
    [ProducesResponseType(typeof(SearchResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Search(
        [FromBody] SearchRequest request,
        CancellationToken cancellationToken
    )
    {
        var response = await Mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPut("{productId:guid}/amount")]
    [AuthorizeUserExists]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangeAmount(
        [FromRoute] Guid productId,
        [FromBody] ChangeAmountRequest request,
        CancellationToken cancellationToken
    )
    {
        request.ProductId = productId;
        var response = await Mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpGet("{productId:guid}/details")]
    [AuthorizeUserExists]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetDetails(
        [FromRoute] Guid productId,
        CancellationToken cancellationToken
    )
    {
        var request = new GetDetailsRequest(productId);
        var response = await Mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPut("{productId:guid}")]
    [AuthorizeUserExists]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Edit(
        [FromRoute] Guid productId,
        [FromBody] EditRequest request,
        CancellationToken cancellationToken
    )
    {
        request.ProductId = productId;
        var response = await Mediator.Send(request, cancellationToken);
        return Ok(response);
    }
    
    [HttpDelete("{productId:guid}")]
    [AuthorizeUserExists]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid productId,
        CancellationToken cancellationToken
    )
    {
        var request = new DeleteRequest(productId);
        var response = await Mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}