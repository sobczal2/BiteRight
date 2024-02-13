// # ==============================================================================
// # Solution: BiteRight
// # File: DisposeRequest.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using MediatR;

#endregion

namespace BiteRight.Application.Commands.Products.Dispose;

public record DisposeRequest(
    Guid ProductId
) : IRequest<Unit>;