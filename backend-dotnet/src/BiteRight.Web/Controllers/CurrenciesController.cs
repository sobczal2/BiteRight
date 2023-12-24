using System.Threading.Tasks;
using BiteRight.Application.Queries.Currencies.List;
using BiteRight.Web.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BiteRight.Web.Controllers;

public class CurrenciesController : WebController
{
    protected CurrenciesController(
        IMediator mediator
    )
        : base(mediator)
    {
    }
    
    [HttpGet]
    [AuthorizeUserExists]
    public async Task<IActionResult> List()
    {
        var request = new ListRequest();
        var response = await Mediator.Send(request);
        return Ok(response);
    }
}