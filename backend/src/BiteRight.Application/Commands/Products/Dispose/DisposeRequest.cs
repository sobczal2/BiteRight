using System;
using MediatR;

namespace BiteRight.Application.Commands.Products.Dispose;

public record DisposeRequest(
    Guid ProductId
) : IRequest<Unit>;