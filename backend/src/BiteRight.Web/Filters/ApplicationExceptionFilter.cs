// # ==============================================================================
// # Solution: BiteRight
// # File: ApplicationExceptionFilter.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using BiteRight.Application.Common.Exceptions;
using BiteRight.Resources.Resources.Common;
using BiteRight.Web.Responses;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

#endregion

namespace BiteRight.Web.Filters;

public class ApplicationExceptionFilter : IExceptionFilter
{
    private readonly IHostEnvironment _environment;
    private readonly IStringLocalizer<Common> _localizer;
    private readonly ILogger<ApplicationExceptionFilter> _logger;

    public ApplicationExceptionFilter(
        IHostEnvironment environment,
        ILogger<ApplicationExceptionFilter> logger,
        IStringLocalizer<Common> localizer
    )
    {
        _environment = environment;
        _logger = logger;
        _localizer = localizer;
    }

    public void OnException(
        ExceptionContext context
    )
    {
        switch (context.Exception)
        {
            case ValidationException exception:
                HandleValidationException(context, exception);
                break;
            case NotFoundException exception:
                HandleNotFoundException(context, exception);
                break;
            case InternalErrorException exception:
                HandleInternalErrorException(context, exception);
                break;
            default:
                HandleUnknownException(context);
                break;
        }
    }

    private static void HandleValidationException(
        ExceptionContext context,
        ValidationException exception
    )
    {
        var response = ErrorResponse.FromValidationException(exception);
        context.Result = new BadRequestObjectResult(response);
        context.ExceptionHandled = true;
    }

    private void HandleNotFoundException(
        ExceptionContext context,
        NotFoundException _
    )
    {
        var response = ErrorResponse.FromMessage(_localizer[nameof(Common.not_found)]);
        context.Result = new NotFoundObjectResult(response);
        context.ExceptionHandled = true;
    }

    private void HandleInternalErrorException(
        ExceptionContext context,
        InternalErrorException exception
    )
    {
        _logger.LogError(exception, "Internal error");
        var response = ErrorResponse.FromMessage(_localizer[nameof(Common.internal_error)]);
        context.Result = new ObjectResult(response)
        {
            StatusCode = 500
        };
        context.ExceptionHandled = true;
    }

    private void HandleUnknownException(
        ExceptionContext context
    )
    {
        if (_environment.IsDevelopment()) return;

        _logger.LogError(context.Exception, "Unknown exception");
        var response = ErrorResponse.FromMessage(_localizer[nameof(Common.unknown_error)]);
        context.Result = new ObjectResult(response)
        {
            StatusCode = 500
        };
        context.ExceptionHandled = true;
    }
}