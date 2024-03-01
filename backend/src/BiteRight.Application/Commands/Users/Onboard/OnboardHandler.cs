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

public class OnboardHandler : CommandHandlerBase<OnboardRequest, OnboardResponse>
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IIdentityManager _identityManager;
    private readonly IIdentityProvider _identityProvider;
    private readonly IStringLocalizer<Resources.Resources.Users.Users> _usersLocalizer;
    private readonly IStringLocalizer<Resources.Resources.Currencies.Currencies> _currenciesLocalizer;
    private readonly IUserRepository _userRepository;
    private readonly ICurrencyRepository _currencyRepository;

    public OnboardHandler(
        IIdentityManager identityManager,
        IIdentityProvider identityProvider,
        IDateTimeProvider dateTimeProvider,
        IUserRepository userRepository,
        ICurrencyRepository currencyRepository,
        IStringLocalizer<Resources.Resources.Users.Users> usersLocalizer,
        IStringLocalizer<Resources.Resources.Currencies.Currencies> currenciesLocalizer,
        AppDbContext appAppDbContext
    )
        : base(appAppDbContext)
    {
        _identityManager = identityManager;
        _identityProvider = identityProvider;
        _dateTimeProvider = dateTimeProvider;
        _userRepository = userRepository;
        _currencyRepository = currencyRepository;
        _usersLocalizer = usersLocalizer;
        _currenciesLocalizer = currenciesLocalizer;
    }

    protected override async Task<OnboardResponse> HandleImpl(
        OnboardRequest request,
        CancellationToken cancellationToken
    )
    {
        var currentIdentityId = _identityProvider.RequireCurrent();
        var existingUser =
            await _userRepository.FindByIdentityId(currentIdentityId, cancellationToken: cancellationToken);

        if (existingUser != null)
            throw ValidationException(
                _usersLocalizer[nameof(Resources.Resources.Users.Users.user_already_exists)]
            );

        var (email, isVerified) = await _identityManager.GetEmail(currentIdentityId, cancellationToken);

        if (!isVerified)
            throw ValidationException(
                _usersLocalizer[nameof(Resources.Resources.Users.Users.email_not_verified)]
            );

        var username = Username.Create(request.Username);

        var existsByUsername = await _userRepository.ExistsByUsername(username, cancellationToken);
        if (existsByUsername)
            throw ValidationException(
                _usersLocalizer[nameof(Resources.Resources.Users.Users.username)],
                _usersLocalizer[nameof(Resources.Resources.Users.Users.username_in_use)]
            );

        var existsByEmail = await _userRepository.ExistsByEmail(email, cancellationToken);
        if (existsByEmail)
            throw ValidationException(
                _usersLocalizer[nameof(Resources.Resources.Users.Users.email_in_use)]
            );
        
        var currencyExists = await _currencyRepository.ExistsById(request.CurrencyId, cancellationToken);
        if (!currencyExists)
            throw ValidationException(
                nameof(OnboardRequest.CurrencyId),
                _currenciesLocalizer[nameof(Resources.Resources.Currencies.Currencies.currency_not_found)]
            );

        if (!TimeZoneInfo.TryFindSystemTimeZoneById(request.TimeZoneId, out var timeZone))
            throw ValidationException(
                nameof(OnboardRequest.TimeZoneId),
                _usersLocalizer[nameof(Resources.Resources.Users.Users.time_zone_id_not_found)]
            );

        var userId = new UserId();

        var profile = Profile.Create(
            userId,
            request.CurrencyId,
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

        return new OnboardResponse();
    }

    protected override ValidationException MapExceptionToValidationException(
        Exception exception
    )
    {
        return exception switch
        {
            EmailNotValidException => ValidationException(
                _usersLocalizer[nameof(Resources.Resources.Users.Users.email_not_valid)]
            ),
            UsernameEmptyException => ValidationException(
                nameof(OnboardRequest.Username),
                _usersLocalizer[nameof(Resources.Resources.Users.Users.username_empty)]
            ),
            UsernameInvalidLengthException usernameLengthNotValidException => ValidationException(
                nameof(OnboardRequest.Username),
                string.Format(_usersLocalizer[nameof(Resources.Resources.Users.Users.username_length_not_valid)],
                    usernameLengthNotValidException.MinLength, usernameLengthNotValidException.MaxLength)
            ),
            UsernameInvalidCharactersException usernameCharactersNotValidException => ValidationException(
                nameof(OnboardRequest.Username),
                string.Format(_usersLocalizer[nameof(Resources.Resources.Users.Users.username_characters_not_valid)],
                    usernameCharactersNotValidException.ValidCharacters)
            ),
            _ => base.MapExceptionToValidationException(exception)
        };
    }
}