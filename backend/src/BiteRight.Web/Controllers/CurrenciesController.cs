// # ==============================================================================
// # Solution: BiteRight
// # File: CurrenciesController.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Threading;
using System.Threading.Tasks;
using BiteRight.Application.Queries.Currencies.GetDefault;
using BiteRight.Application.Queries.Currencies.Search;
using BiteRight.Web.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace BiteRight.Web.Controllers;

public class CurrenciesController : WebController
{
    public CurrenciesController(
        IMediator mediator
    )
        : base(mediator)
    {
    }

    [HttpPost("search")]
    [Authorize]
    [ProducesResponseType(typeof(SearchResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Search(
        [FromBody] SearchRequest request,
        CancellationToken cancellationToken
    )
    {
        var response = await Mediator.Send(
            request,
            cancellationToken
        );

        return Ok(response);
    }

    [HttpGet("default")]
    [Authorize]
    [ProducesResponseType(typeof(GetDefaultResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetDefault(
        CancellationToken cancellationToken
    )
    {
        var response = await Mediator.Send(
            new GetDefaultRequest(),
            cancellationToken
        );

        return Ok(response);
    }
}