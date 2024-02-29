// # ==============================================================================
// # Solution: BiteRight
// # File: CountriesController.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Threading;
using System.Threading.Tasks;
using BiteRight.Application.Queries.Countries.List;
using BiteRight.Web.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace BiteRight.Web.Controllers;

public class CountriesController : WebController
{
    public CountriesController(
        IMediator mediator
    )
        : base(mediator)
    {
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(ListResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> List(CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new ListRequest(), cancellationToken);
        return Ok(response);
    }
}