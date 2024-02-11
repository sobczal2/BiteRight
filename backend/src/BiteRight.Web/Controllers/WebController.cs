using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BiteRight.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WebController : ControllerBase
{
    protected IMediator Mediator { get; }

    protected WebController(
        IMediator mediator
    )
    {
        Mediator = mediator;
    }
}