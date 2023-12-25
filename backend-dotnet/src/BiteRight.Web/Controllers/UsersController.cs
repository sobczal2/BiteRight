using System.Threading.Tasks;
using BiteRight.Application.Commands.Users.Onboard;
using BiteRight.Web.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<IActionResult> Onboard(OnboardRequest request)
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }
}