// # ==============================================================================
// # Solution: BiteRight
// # File: RestoreRequest.cs
// # Author: Łukasz Sobczak
// # Created: 15-02-2024
// # ==============================================================================

using System;
using MediatR;

namespace BiteRight.Application.Commands.Products.Restore;

public record RestoreRequest(Guid ProductId) : IRequest<Unit>;