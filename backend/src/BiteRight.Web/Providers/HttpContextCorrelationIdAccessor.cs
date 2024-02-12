// # ==============================================================================
// # Solution: BiteRight
// # File: HttpContextCorrelationIdAccessor.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using BiteRight.Domain.Abstracts.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

#endregion

namespace BiteRight.Web.Providers;

public class HttpContextCorrelationIdAccessor : ICorrelationIdAccessor
{
    public HttpContextCorrelationIdAccessor(
        IHttpContextAccessor httpContextAccessor,
        ILogger<HttpContextCorrelationIdAccessor> logger
    )
    {
        var correlationId = httpContextAccessor.HttpContext?.Items["CorrelationId"] as string;

        switch (correlationId)
        {
            case null:
                logger.LogWarning("CorrelationId is null");
                CorrelationId = Guid.NewGuid();
                break;
            case var s when Guid.TryParse(s, out var guid):
                CorrelationId = guid;
                break;
            default:
                logger.LogWarning("CorrelationId is not a valid Guid");
                CorrelationId = Guid.NewGuid();
                break;
        }
    }

    public Guid CorrelationId { get; }
}