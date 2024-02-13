// # ==============================================================================
// # Solution: BiteRight
// # File: QueryHandlerBase.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

#endregion

namespace BiteRight.Application.Common;

public abstract class QueryHandlerBase<TRequest, TResponse> : HandlerBase<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public override async Task<TResponse> Handle(
        TRequest request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            return await HandleImpl(request, cancellationToken);
        }
        catch (Exception exception)
        {
            throw MapExceptionToValidationException(exception);
        }
    }
}

public abstract class QueryHandlerBase<TRequest> : QueryHandlerBase<TRequest, Unit>
    where TRequest : IRequest<Unit>
{
}