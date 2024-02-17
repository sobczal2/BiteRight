// # ==============================================================================
// # Solution: BiteRight
// # File: ProductsController.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using System.Threading.Tasks;
using BiteRight.Application.Commands.Products.ChangeAmount;
using BiteRight.Application.Commands.Products.Create;
using BiteRight.Application.Commands.Products.Dispose;
using BiteRight.Application.Commands.Products.Restore;
using BiteRight.Application.Dtos.Products;
using BiteRight.Application.Queries.Products.List;
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
    /// <returns>New product id.</returns>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /Products
    ///     {
    ///        "Name": "Example Product",
    ///        "Description": "This is an example product description.",
    ///        "Price": 19.99,
    ///        "CurrencyId": "3B56A6DE-3B41-4B10-934F-469CA12F4FE3",
    ///        "ExpirationDate": "2024-12-31",
    ///        "ExpirationDateKind": 2,
    ///        "CategoryId": "E8C78317-70AC-4051-805E-ECE2BB37656F",
    ///        "maximumAmountValue": 100,
    ///        "amountUnitId": "B6D4D4DD-C035-4047-B8EE-48937CB1F368"
    ///     }
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
        [FromQuery] ProductSortingStrategy sortingStrategy
    )
    {
        var request = new ListCurrentRequest
        {
            SortingStrategy = sortingStrategy
        };
        var response = await Mediator.Send(request);
        return Ok(response);
    }

    [HttpPut("{productId:guid}/dispose")]
    [AuthorizeUserExists]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Dispose(
        Guid productId
    )
    {
        var request = new DisposeRequest(productId);
        var response = await Mediator.Send(request);
        return Ok(response);
    }

    [HttpPut("{productId:guid}/restore")]
    [AuthorizeUserExists]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Restore(
        Guid productId
    )
    {
        var request = new RestoreRequest(productId);
        var response = await Mediator.Send(request);
        return Ok(response);
    }

    [HttpPost("search")]
    [AuthorizeUserExists]
    [ProducesResponseType(typeof(SearchResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Search(
        [FromBody] SearchRequest request
    )
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }

    [HttpPut("{productId:guid}/amount")]
    [AuthorizeUserExists]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangeAmount(
        Guid productId,
        [FromBody] ChangeAmountRequest request
    )
    {
        request.ProductId = productId;
        var response = await Mediator.Send(request);
        return Ok(response);
    }
}