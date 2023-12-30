using System.Threading;
using System.Threading.Tasks;
using BiteRight.Application.Dtos.Categories;
using BiteRight.Application.Dtos.Common;
using BiteRight.Application.Queries.Categories.Search;
using BiteRight.Web.Authorization;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BiteRight.Web.Controllers;

public class CategoriesController : WebController
{
    public CategoriesController(
        IMediator mediator
    )
        : base(mediator)
    {
    }
    
    [HttpGet("search")]
    [AuthorizeUserExists]
    [ProducesResponseType(typeof(SearchResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Search(
        [FromQuery] string? query,
        [FromQuery] int pageNumber,
        [FromQuery] int pageSize,
        CancellationToken cancellationToken
    )
    {
        var response = await Mediator.Send(
            new SearchRequest(
                query ?? string.Empty,
                new PaginationParams(pageNumber, pageSize)
            ),
            cancellationToken
        );

        return Ok(response);
    }
}