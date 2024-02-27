// # ==============================================================================
// # Solution: BiteRight
// # File: GetDetailsRequest.cs
// # Author: Łukasz Sobczak
// # Created: 25-02-2024
// # ==============================================================================

using System;
using MediatR;

namespace BiteRight.Application.Queries.Products.GetDetails;

public class GetDetailsRequest : IRequest<GetDetailsResponse>
{
    public Guid ProductId { get; }

    public GetDetailsRequest(
        Guid productId
    )
    {
        ProductId = productId;
    }
}