// # ==============================================================================
// # Solution: BiteRight
// # File: WebController.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using MediatR;
using Microsoft.AspNetCore.Mvc;

#endregion

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