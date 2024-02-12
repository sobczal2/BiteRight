// # ==============================================================================
// # Solution: BiteRight
// # File: GetPhotoRequest.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using MediatR;

#endregion

namespace BiteRight.Application.Queries.Categories.GetPhoto;

public class GetPhotoRequest : IRequest<GetPhotoResponse>
{
    public GetPhotoRequest(Guid categoryId)
    {
        CategoryId = categoryId;
    }

    public Guid CategoryId { get; }
}