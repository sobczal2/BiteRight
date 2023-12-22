using System.Threading.Tasks;
using BiteRight.Infrastructure.Database;
using Microsoft.AspNetCore.Http;

namespace BiteRight.Web.Middleware;

public class TransactionMiddleware : IMiddleware
{
    private readonly AppDbContext _dbContext;

    public TransactionMiddleware(
        AppDbContext dbContext
    )
    {
        _dbContext = dbContext;
    }

    public async Task InvokeAsync(
        HttpContext context,
        RequestDelegate next
    )
    {
        await _dbContext.Database.BeginTransactionAsync(context.RequestAborted);
        try
        {
            await next(context);
            await _dbContext.Database.CommitTransactionAsync(context.RequestAborted);
        }
        catch
        {
            await _dbContext.Database.RollbackTransactionAsync(context.RequestAborted);
            throw;
        }
    }
}