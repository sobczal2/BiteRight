using FluentValidation;
using MediatR;

namespace BiteRight.Application.Common;

public class MediatorValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : notnull
{
    private readonly IValidator<TRequest> _validator;

    public MediatorValidationBehaviour(
        IValidator<TRequest> validator
    )
    {
        _validator = validator;
    }

    public Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        _validator.ValidateAndThrow(request);
        
        return next();
    }
}