// # ==============================================================================
// # Solution: BiteRight
// # File: ErrorResponse.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Microsoft.Extensions.Localization;

#endregion

namespace BiteRight.Web.Responses;

public class ErrorResponse
{
    public ErrorResponse(
        string message
    )
    {
        Message = message;
        Errors = new Dictionary<string, List<string>>();
    }

    public ErrorResponse(
        string message,
        Dictionary<string, List<string>> errors
    )
    {
        Message = message;
        Errors = errors;
    }

    public string Message { get; set; }
    public Dictionary<string, List<string>> Errors { get; set; }

    public static ErrorResponse FromMessage(
        string message
    )
    {
        return new ErrorResponse(message);
    }

    public static ErrorResponse FromValidationException(
        ValidationException exception,
        IStringLocalizer<Resources.Resources.Common.Common> commonLocalizer
    )
    {
        var errors = exception.Errors
            .GroupBy(x => x.PropertyName)
            .ToDictionary(
                x => x.Key,
                x => x.Select(y => y.ErrorMessage).ToList()
            );
        return new ErrorResponse(
            commonLocalizer[nameof(Resources.Resources.Common.Common.validation_error)],
            errors
        );
    }
}