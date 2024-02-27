// # ==============================================================================
// # Solution: BiteRight
// # File: CurrenciesController.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Threading;
using System.Threading.Tasks;
using BiteRight.Application.Queries.Currencies.Search;
using BiteRight.Web.Authorization;
using MediatR;
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
    [AuthorizeUserExists]
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
}