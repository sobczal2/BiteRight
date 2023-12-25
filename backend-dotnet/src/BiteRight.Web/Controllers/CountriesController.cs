using System.Threading.Tasks;
using BiteRight.Application.Queries.Countries.List;
using BiteRight.Web.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    [AuthorizeUserExists]
    public async Task<IActionResult> List()
    {
        var response = await Mediator.Send(new ListRequest());
        return Ok(response);
    }
}