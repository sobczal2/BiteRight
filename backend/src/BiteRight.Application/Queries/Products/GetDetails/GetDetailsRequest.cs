// # ==============================================================================
// # Solution: BiteRight
// # File: GetDetailsRequest.cs
// # Author: Łukasz Sobczak
// # Created: 25-02-2024
// # ==============================================================================

#region

using System;
using MediatR;

#endregion

namespace BiteRight.Application.Queries.Products.GetDetails;

public class GetDetailsRequest : IRequest<GetDetailsResponse>
{
    public GetDetailsRequest(
        Guid productId
    )
    {
        ProductId = productId;
    }

    public Guid ProductId { get; }
}