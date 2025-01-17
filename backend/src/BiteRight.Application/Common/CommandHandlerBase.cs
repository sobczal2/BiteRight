// # ==============================================================================
// # Solution: BiteRight
// # File: CommandHandlerBase.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using System.Threading;
using System.Threading.Tasks;
using BiteRight.Infrastructure.Database;
using MediatR;

#endregion

namespace BiteRight.Application.Common;

public abstract class CommandHandlerBase<TRequest, TResponse> : HandlerBase<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    protected CommandHandlerBase(
        AppDbContext appDbContext
    )
    {
        AppDbContext = appDbContext;
    }

    protected AppDbContext AppDbContext { get; }

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