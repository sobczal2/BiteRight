using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace BiteRight.Application.Commands.Common;

public abstract class CommandHandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : class
{
    public Task<TResponse> Handle(
        TRequest request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            return HandleImpl(request, cancellationToken);
        }
        catch (Exception exception)
        {
            return Task.FromException<TResponse>(exception);
        }
    }
    
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
            AggregateException aggregateException => MapExceptionToValidationException(aggregateException.InnerException),
            _ => throw exception
        };
    }
    
    protected ValidationException ValidationException(string propertyName, string errorMessage)
    {
        return new ValidationException(new List<ValidationFailure>
        {
            new(propertyName, errorMessage)
        });
    }
    
    protected ValidationException ValidationException(string errorMessage)
    {
        return new ValidationException(errorMessage);
    }
}