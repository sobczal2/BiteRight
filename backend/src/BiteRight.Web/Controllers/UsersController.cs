using System.Threading.Tasks;
using BiteRight.Application.Commands.Users.Onboard;
using BiteRight.Application.Commands.Users.UpdateProfile;
using BiteRight.Application.Queries.Users.Me;
using BiteRight.Web.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        OnboardRequest request
    )
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }

    [HttpGet("me")]
    [AuthorizeNamePresent]
    [ProducesResponseType(typeof(MeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Me()
    {
        var request = new MeRequest();
        var response = await Mediator.Send(request);
        return Ok(response);
    }

    [HttpPut("profile")]
    [AuthorizeUserExists]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateProfile(
        [FromBody] UpdateProfileRequest request
    )
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }
}