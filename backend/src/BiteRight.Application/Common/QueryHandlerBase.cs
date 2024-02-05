using MediatR;

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