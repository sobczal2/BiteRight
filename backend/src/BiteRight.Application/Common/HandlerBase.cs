// # ==============================================================================
// # Solution: BiteRight
// # File: HandlerBase.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

#endregion

namespace BiteRight.Application.Common;

public abstract class HandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public abstract Task<TResponse> Handle(
        TRequest request,
        CancellationToken cancellationToken
    );


    protected abstract Task<TResponse> HandleImpl(
        TRequest request,
        CancellationToken cancellationToken
    );

    protected virtual ValidationException MapExceptionToValidationException(
        Exception exception
    )
    {
        return exception switch
        {
            ValidationException validationException => validationException,
            AggregateException { InnerExceptions: [_, ..] innerExceptions } =>
                MapExceptionToValidationException(innerExceptions.First()),
            _ => throw exception
        };
    }

    protected ValidationException ValidationException(
        string propertyName,
        string errorMessage
    )
    {
        return new ValidationException(new List<ValidationFailure>
        {
            new(propertyName, errorMessage)
        });
    }

    protected ValidationException ValidationException(
        string errorMessage
    )
    {
        return new ValidationException(errorMessage);
    }
}

public abstract class HandlerBase<TRequest> : HandlerBase<TRequest, Unit>
    where TRequest : IRequest<Unit>;