// # ==============================================================================
// # Solution: BiteRight
// # File: CategoriesController.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using System.Threading;
using System.Threading.Tasks;
using BiteRight.Application.Dtos.Common;
using BiteRight.Application.Queries.Categories.GetPhoto;
using BiteRight.Application.Queries.Categories.Search;
using BiteRight.Web.Authorization;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

#endregion

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

    // TODO: add authorization
    [HttpGet("{categoryId:guid}/photo")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPhoto(
        [FromRoute] Guid categoryId,
        CancellationToken cancellationToken
    )
    {
        var photo = await Mediator.Send(
            new GetPhotoRequest(categoryId),
            cancellationToken
        );

        return new FileStreamResult(photo.PhotoStream, photo.ContentType)
        {
            FileDownloadName = photo.FileName
        };
    }
}