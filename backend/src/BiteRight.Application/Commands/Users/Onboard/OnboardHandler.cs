using BiteRight.Application.Commands.Common;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Abstracts.Repositories;
using BiteRight.Domain.Users;
using BiteRight.Domain.Users.Exceptions;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BiteRight.Application.Commands.Users.Onboard;

public class OnboardHandler : CommandHandlerBase<OnboardRequest, OnboardResponse>
{
    private readonly IIdentityManager _identityManager;
    private readonly IDomainEventFactory _domainEventFactory;
    private readonly IIdentityProvider _identityProvider;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUserRepository _userRepository;
    private readonly IStringLocalizer<Resources.Resources.Users.Users> _localizer;

    public OnboardHandler(
        IIdentityManager identityManager,
        IDomainEventFactory domainEventFactory,
        IIdentityProvider identityProvider,
        IDateTimeProvider dateTimeProvider,
        IUserRepository userRepository,
        IStringLocalizer<Resources.Resources.Users.Users> localizer
    )
    {
        _identityManager = identityManager;
        _domainEventFactory = domainEventFactory;
        _identityProvider = identityProvider;
        _dateTimeProvider = dateTimeProvider;
        _userRepository = userRepository;
        _localizer = localizer;
    }

    protected override async Task<OnboardResponse> HandleImpl(
        OnboardRequest request,
        CancellationToken cancellationToken
    )
    {
        var currentIdentityId = _identityProvider.RequireCurrent();
        var existingUser = await _userRepository.FindByIdentityId(currentIdentityId, cancellationToken);
        
        if (existingUser != null)
        {
            throw ValidationException(
                _localizer[nameof(Resources.Resources.Users.Users.user_already_exists)]
            );
        }
        
        var (email, isVerified) = await _identityManager.GetEmail(currentIdentityId, cancellationToken);
        
        if (!isVerified)
        {
            throw ValidationException(
                _localizer[nameof(Resources.Resources.Users.Users.email_not_verified)]
            );
        }
        var username = Username.Create(request.Username);

        var existsByUsername = await _userRepository.ExistsByUsername(username, cancellationToken);
        if (existsByUsername)
        {
            throw ValidationException(
                nameof(OnboardRequest.Username),
                _localizer[nameof(Resources.Resources.Users.Users.username_in_use)]
            );
        }

        var existsByEmail = await _userRepository.ExistsByEmail(email, cancellationToken);
        if (existsByEmail)
        {
            throw ValidationException(
                _localizer[nameof(Resources.Resources.Users.Users.email_in_use)]
            );
        }

        var user = User.Create(
            currentIdentityId,
            Username.Create(request.Username),
            email,
            _domainEventFactory,
            _dateTimeProvider
        );

        _userRepository.Add(user);

        return new OnboardResponse();
    }

    protected override ValidationException MapExceptionToValidationException(
        Exception exception
    )
    {
        return exception switch
        {
            EmailNotValidException _ => ValidationException(
                _localizer[nameof(Resources.Resources.Users.Users.email_not_valid)]
            ),
            UsernameEmptyException _ => ValidationException(
                nameof(OnboardRequest.Username),
                _localizer[nameof(Resources.Resources.Users.Users.username_empty)]
            ),
            UsernameInvalidLengthException usernameLengthNotValidException => ValidationException(
                nameof(OnboardRequest.Username),
                string.Format(_localizer[nameof(Resources.Resources.Users.Users.username_length_not_valid)], usernameLengthNotValidException.MinLength, usernameLengthNotValidException.MaxLength)
            ),
            UsernameInvalidCharactersException usernameCharactersNotValidException => ValidationException(
                nameof(OnboardRequest.Username),
                string.Format(_localizer[nameof(Resources.Resources.Users.Users.username_characters_not_valid)], usernameCharactersNotValidException.ValidCharacters)
            ),
            _ => base.MapExceptionToValidationException(exception)
        };
    }
}