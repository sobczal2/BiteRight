// # ==============================================================================
// # Solution: BiteRight
// # File: IdentityIdHeaderMiddleware.cs
// # Author: Łukasz Sobczak
// # Created: 29-02-2024
// # ==============================================================================

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BiteRight.Web.Middleware;

public class IdentityIdHeaderMiddleware : IMiddleware
{
    public const string IdentityIdHeader = "X-Identity-Id";
    public const string DefaultIdentityId = "anonymous";
    public Task InvokeAsync(
        HttpContext context,
        RequestDelegate next
    )
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var identityId = context.User.Identity.Name;
            context.Request.Headers.Append(IdentityIdHeader, identityId);
        }
        else
        {
            context.Request.Headers.Append(IdentityIdHeader, DefaultIdentityId);
        }

        return next(context);
    }
}