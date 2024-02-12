// # ==============================================================================
// # Solution: BiteRight
// # File: LanguagesController.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Threading.Tasks;
using BiteRight.Application.Queries.Languages.List;
using BiteRight.Web.Authorization;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace BiteRight.Web.Controllers;

public class LanguagesController : WebController
{
    public LanguagesController(
        IMediator mediator
    )
        : base(mediator)
    {
    }

    [HttpGet]
    [AuthorizeUserExists]
    [ProducesResponseType(typeof(ListResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> List()
    {
        var response = await Mediator.Send(new ListRequest());
        return Ok(response);
    }
}