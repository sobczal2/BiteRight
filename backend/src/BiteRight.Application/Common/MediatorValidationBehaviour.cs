// # ==============================================================================
// # Solution: BiteRight
// # File: MediatorValidationBehaviour.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

#endregion

namespace BiteRight.Application.Common;

public class MediatorValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : notnull
{
    private readonly IValidator<TRequest>? _validator;

    public MediatorValidationBehaviour(
        IEnumerable<IValidator<TRequest>> validators
    )
    {
        _validator = validators.FirstOrDefault();
    }

    public Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        _validator?.ValidateAndThrow(request);

        return next();
    }
}