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
using BiteRight.Application.Dtos.Categories;
using BiteRight.Application.Queries.Categories.GetDefault;
using BiteRight.Application.Queries.Categories.GetPhoto;
using BiteRight.Application.Queries.Categories.Search;
using BiteRight.Web.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

    [HttpPost("search")]
    [Authorize]
    [ProducesResponseType(typeof(SearchResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Search(
        [FromBody] SearchRequest request,
        CancellationToken cancellationToken
    )
    {
        var response = await Mediator.Send(
            request,
            cancellationToken
        );

        return Ok(response);
    }

    [HttpGet("{categoryId:guid}/photo")]
    [Authorize]
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

    [HttpGet("default")]
    [Authorize]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetDefault(
        CancellationToken cancellationToken
    )
    {
        var response = await Mediator.Send(
            new GetDefaultRequest(),
            cancellationToken
        );

        return Ok(response);
    }
}