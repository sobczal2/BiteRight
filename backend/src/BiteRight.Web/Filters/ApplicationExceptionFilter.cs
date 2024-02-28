// # ==============================================================================
// # Solution: BiteRight
// # File: ApplicationExceptionFilter.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using BiteRight.Application.Common.Exceptions;
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
    private readonly IStringLocalizer<Resources.Resources.Common.Common> _commonLocalizer;
    private readonly IHostEnvironment _environment;
    private readonly ILogger<ApplicationExceptionFilter> _logger;

    public ApplicationExceptionFilter(
        IHostEnvironment environment,
        ILogger<ApplicationExceptionFilter> logger,
        IStringLocalizer<Resources.Resources.Common.Common> commonLocalizer
    )
    {
        _environment = environment;
        _logger = logger;
        _commonLocalizer = commonLocalizer;
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

    private void HandleValidationException(
        ExceptionContext context,
        ValidationException exception
    )
    {
        var response = ErrorResponse.FromValidationException(exception, _commonLocalizer);
        context.Result = new BadRequestObjectResult(response);
        context.ExceptionHandled = true;
    }

    private void HandleNotFoundException(
        ExceptionContext context,
        NotFoundException _
    )
    {
        var response = ErrorResponse.FromMessage(_commonLocalizer[nameof(Resources.Resources.Common.Common.not_found)]);
        context.Result = new NotFoundObjectResult(response);
        context.ExceptionHandled = true;
    }

    private void HandleInternalErrorException(
        ExceptionContext context,
        InternalErrorException exception
    )
    {
        _logger.LogError(exception, "Internal error");
        var response =
            ErrorResponse.FromMessage(_commonLocalizer[nameof(Resources.Resources.Common.Common.internal_error)]);
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
        var response =
            ErrorResponse.FromMessage(_commonLocalizer[nameof(Resources.Resources.Common.Common.unknown_error)]);
        context.Result = new ObjectResult(response)
        {
            StatusCode = 500
        };
        context.ExceptionHandled = true;
    }
}