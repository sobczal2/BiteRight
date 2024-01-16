using System.Collections.Generic;
using System.Linq;
using BiteRight.Application.Common.Exceptions;
using FluentValidation;

namespace BiteRight.Web.Responses;

public class ErrorResponse
{
    public string Message { get; set; }
    public Dictionary<string, List<string>> Errors { get; set; }

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

    public static ErrorResponse FromMessage(
        string message
    )
    {
        return new ErrorResponse(message);
    }

    public static ErrorResponse FromValidationException(
        ValidationException exception
    )
    {
        var errors = exception.Errors.ToDictionary(error => error.PropertyName,
            error => new List<string> { error.ErrorMessage });
        return new ErrorResponse(exception.Message, errors);
    }
}