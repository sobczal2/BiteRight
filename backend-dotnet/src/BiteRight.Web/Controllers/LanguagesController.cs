using System.Threading.Tasks;
using BiteRight.Application.Queries.Languages.List;
using BiteRight.Web.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> List()
    {
        var response = await Mediator.Send(new ListRequest());
        return Ok(response);
    }
}