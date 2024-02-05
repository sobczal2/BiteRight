using System.Threading.Tasks;
using BiteRight.Application.Commands.Products.Create;
using BiteRight.Web.Authorization;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BiteRight.Web.Controllers;

public class ProductsController : WebController
{
    public ProductsController(
        IMediator mediator
    )
        : base(mediator)
    {
    }

    [HttpPost]
    [AuthorizeUserExists]
    [ProducesResponseType(typeof(CreateResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(
        [FromBody] CreateRequest request
    )
    {
        var response = await Mediator.Send(request);
        return Ok(response);
    }
}