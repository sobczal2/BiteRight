using BiteRight.Infrastructure.Database;
using MediatR;

namespace BiteRight.Application.Common;

public abstract class CommandHandlerBase<TRequest, TResponse> : HandlerBase<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    protected AppDbContext AppDbContext { get; }

    protected CommandHandlerBase(
        AppDbContext appDbContext
    )
    {
        AppDbContext = appDbContext;
    }

    public override async Task<TResponse> Handle(
        TRequest request,
        CancellationToken cancellationToken
    )
    {
        await AppDbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var result = await HandleImpl(request, cancellationToken);
            await AppDbContext.SaveChangesAsync(cancellationToken);
            await AppDbContext.Database.CommitTransactionAsync(cancellationToken);
            return result;
        }
        catch (Exception exception)
        {
            await AppDbContext.Database.RollbackTransactionAsync(cancellationToken);
            throw MapExceptionToValidationException(exception);
        }
    }
}

public abstract class CommandHandlerBase<TRequest> : CommandHandlerBase<TRequest, Unit>
    where TRequest : IRequest<Unit>
{
    protected CommandHandlerBase(
        AppDbContext appDbContext
    )
        : base(appDbContext)
    {
    }
}