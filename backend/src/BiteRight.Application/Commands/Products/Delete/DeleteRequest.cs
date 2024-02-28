// # ==============================================================================
// # Solution: BiteRight
// # File: DeleteRequest.cs
// # Author: Łukasz Sobczak
// # Created: 27-02-2024
// # ==============================================================================

using System;
using MediatR;

namespace BiteRight.Application.Commands.Products.Delete;

public class DeleteRequest : IRequest<DeleteResponse>
{
    public Guid ProductId { get; }
    
    public DeleteRequest(Guid productId)
    {
        ProductId = productId;
    }
}