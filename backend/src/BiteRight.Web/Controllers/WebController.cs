using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BiteRight.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WebController : ControllerBase
{
    protected WebController(
        IMediator mediator
    )
    {
        Mediator = mediator;
    }

    protected IMediator Mediator { get; }
}