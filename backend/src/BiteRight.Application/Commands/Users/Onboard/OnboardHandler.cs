// # ==============================================================================
// # Solution: BiteRight
// # File: OnboardHandler.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using System.Threading;
using System.Threading.Tasks;
using BiteRight.Application.Common;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Abstracts.Repositories;
using BiteRight.Domain.Users;
using BiteRight.Domain.Users.Exceptions;
using BiteRight.Infrastructure.Configuration.Currencies;
using BiteRight.Infrastructure.Database;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;

#endregion

namespace BiteRight.Application.Commands.Users.Onboard;

public class OnboardHandler : CommandHandlerBase<OnboardRequest>
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IIdentityManager _identityManager;
    private readonly IIdentityProvider _identityProvider;
    private readonly IStringLocalizer<Resources.Resources.Users.Users> _localizer;
    private readonly IUserRepository _userRepository;

    public OnboardHandler(
        IIdentityManager identityManager,
        IIdentityProvider identityProvider,
        IDateTimeProvider dateTimeProvider,
        IUserRepository userRepository,
        IStringLocalizer<Resources.Resources.Users.Users> localizer,
        AppDbContext appAppDbContext
    )
        : base(appAppDbContext)
    {
        _identityManager = identityManager;
        _identityProvider = identityProvider;
        _dateTimeProvider = dateTimeProvider;
        _userRepository = userRepository;
        _localizer = localizer;
    }

    protected override async Task<Unit> HandleImpl(
        OnboardRequest request,
        CancellationToken cancellationToken
    )
    {
        var currentIdentityId = _identityProvider.RequireCurrent();
        var existingUser = await _userRepository.FindByIdentityId(currentIdentityId, cancellationToken);

        if (existingUser != null)
            throw ValidationException(
                _localizer[nameof(Resources.Resources.Users.Users.user_already_exists)]
            );

        var (email, isVerified) = await _identityManager.GetEmail(currentIdentityId, cancellationToken);

        if (!isVerified)
            throw ValidationException(
                _localizer[nameof(Resources.Resources.Users.Users.email_not_verified)]
            );

        var username = Username.Create(request.Username);

        var existsByUsername = await _userRepository.ExistsByUsername(username, cancellationToken);
        if (existsByUsername)
            throw ValidationException(
                _localizer[nameof(Resources.Resources.Users.Users.username)],
                _localizer[nameof(Resources.Resources.Users.Users.username_in_use)]
            );

        var existsByEmail = await _userRepository.ExistsByEmail(email, cancellationToken);
        if (existsByEmail)
            throw ValidationException(
                _localizer[nameof(Resources.Resources.Users.Users.email_in_use)]
            );

        if (!TimeZoneInfo.TryFindSystemTimeZoneById(request.TimeZoneId, out var timeZone))
            throw ValidationException(
                nameof(OnboardRequest.TimeZoneId),
                _localizer[nameof(Resources.Resources.Users.Users.time_zone_id_not_found)]
            );

        var userId = new UserId();

        var profile = Profile.Create(
            userId,
            CurrencyConfiguration.USD.Id,
            timeZone
        );

        var user = User.Create(
            currentIdentityId,
            Username.Create(request.Username),
            email,
            profile,
            _dateTimeProvider.UtcNow,
            userId
        );

        _userRepository.Add(user);

        return Unit.Value;
    }

    protected override ValidationException MapExceptionToValidationException(
        Exception exception
    )
    {
        return exception switch
        {
            EmailNotValidException => ValidationException(
                _localizer[nameof(Resources.Resources.Users.Users.email_not_valid)]
            ),
            UsernameEmptyException => ValidationException(
                nameof(OnboardRequest.Username),
                _localizer[nameof(Resources.Resources.Users.Users.username_empty)]
            ),
            UsernameInvalidLengthException usernameLengthNotValidException => ValidationException(
                nameof(OnboardRequest.Username),
                string.Format(_localizer[nameof(Resources.Resources.Users.Users.username_length_not_valid)],
                    usernameLengthNotValidException.MinLength, usernameLengthNotValidException.MaxLength)
            ),
            UsernameInvalidCharactersException usernameCharactersNotValidException => ValidationException(
                nameof(OnboardRequest.Username),
                string.Format(_localizer[nameof(Resources.Resources.Users.Users.username_characters_not_valid)],
                    usernameCharactersNotValidException.ValidCharacters)
            ),
            _ => base.MapExceptionToValidationException(exception)
        };
    }
}