// # ==============================================================================
// # Solution: BiteRight
// # File: UsersController.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Threading;
using System.Threading.Tasks;
using BiteRight.Application.Commands.Users.Onboard;
using BiteRight.Application.Commands.Users.UpdateProfile;
using BiteRight.Application.Queries.Users.Me;
using BiteRight.Web.Authorization;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace BiteRight.Web.Controllers;

public class UsersController : WebController
{
    public UsersController(
        IMediator mediator
    )
        : base(mediator)
    {
    }

    [HttpPost("onboard")]
    [AuthorizeNamePresent]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Onboard(
        [FromBody] OnboardRequest request,
        CancellationToken cancellationToken
    )
    {
        var response = await Mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpGet("me")]
    [AuthorizeNamePresent]
    [ProducesResponseType(typeof(MeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Me(
        CancellationToken cancellationToken
    )
    {
        var request = new MeRequest();
        var response = await Mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPut("profile")]
    [AuthorizeUserExists]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateProfile(
        [FromBody] UpdateProfileRequest request,
        CancellationToken cancellationToken
    )
    {
        var response = await Mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}