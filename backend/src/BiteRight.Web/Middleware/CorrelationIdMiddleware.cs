using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BiteRight.Web.Middleware;

public class CorrelationIdMiddleware : IMiddleware
{
    public async Task InvokeAsync(
        HttpContext context,
        RequestDelegate next
    )
    {
        if (!context.Request.Headers.TryGetValue("X-Correlation-ID", out var existingCorrelationId))
        {
            existingCorrelationId = Guid.NewGuid().ToString("N");
        }

        context.Items["CorrelationId"] = existingCorrelationId;

        context.Response.OnStarting(() =>
        {
            context.Response.Headers["X-Correlation-ID"] = existingCorrelationId;
            return Task.CompletedTask;
        });

        using (Serilog.Context.LogContext.PushProperty("CorrelationId", existingCorrelationId))
        {
            await next(context);
        }
    }
}