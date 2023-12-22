using BiteRight.Web.Responses;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BiteRight.Web.Filters;

public class ValidationExceptionFilter : IExceptionFilter
{
    public void OnException(
        ExceptionContext context
    )
    {
        if (context.Exception is ValidationException exception)
        {
            var response = ErrorResponse.FromValidationException(exception);
            context.Result = new BadRequestObjectResult(response);
            context.ExceptionHandled = true;
        }
    }
}