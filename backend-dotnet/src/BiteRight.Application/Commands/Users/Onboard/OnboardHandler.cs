using BiteRight.Application.Commands.Common;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Abstracts.Services;
using BiteRight.Domain.Users;
using BiteRight.Domain.Users.Exceptions;
using BiteRight.Infrastructure.Database;
using FluentValidation;
using MediatR;

namespace BiteRight.Application.Commands.Users.Onboard;

public class OnboardHandler : CommandHandlerBase<OnboardRequest, OnboardResponse>
{
    private readonly IIdentityManager _identityManager;
    private readonly IDomainEventFactory _domainEventFactory;
    private readonly IIdentityAccessor _identityAccessor;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly AppDbContext _appDbContext;
    private readonly IUserService _userService;

    public OnboardHandler(
        IIdentityManager identityManager,
        IDomainEventFactory domainEventFactory,
        IIdentityAccessor identityAccessor,
        IDateTimeProvider dateTimeProvider,
        AppDbContext appDbContext,
        IUserService userService
    )
    {
        _identityManager = identityManager;
        _domainEventFactory = domainEventFactory;
        _identityAccessor = identityAccessor;
        _dateTimeProvider = dateTimeProvider;
        _appDbContext = appDbContext;
        _userService = userService;
    }

    protected override async Task<OnboardResponse> HandleImpl(
        OnboardRequest request,
        CancellationToken cancellationToken
    )
    {
        var currentIdentityId = _identityAccessor.RequireCurrent();
        var (email, isVerified) = await _identityManager.GetEmail(currentIdentityId);
        var username = Username.Create(request.Username);

        var isUsernameAvailable = await _userService.IsUsernameAvailable(username);
        if (!isUsernameAvailable)
        {
            throw ValidationException(
                nameof(OnboardRequest.Username),
                "Username already in use"
            );
        }

        var isEmailAvailable = await _userService.IsEmailAvailable(email);
        if (!isEmailAvailable)
        {
            throw ValidationException(
                "Email already in use"
            );
        }

        var user = User.Create(
            currentIdentityId,
            Username.Create(request.Username),
            email,
            isVerified,
            _domainEventFactory,
            _dateTimeProvider
        );

        _appDbContext.Users.Add(user);

        await _appDbContext.SaveChangesAsync(cancellationToken);

        return new OnboardResponse();
    }

    protected override ValidationException MapExceptionToValidationException(
        Exception exception
    )
    {
        return exception switch
        {
            UsernameNotValidException usernameNotValidException => ValidationException(
                nameof(OnboardRequest.Username),
                usernameNotValidException.Message
            ),
            _ => base.MapExceptionToValidationException(exception)
        };
    }
}