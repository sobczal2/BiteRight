using BiteRight.Application.Commands.Common;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Abstracts.Services;
using BiteRight.Domain.Users;
using BiteRight.Domain.Users.Exceptions;
using BiteRight.Infrastructure.Database;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace BiteRight.Application.Commands.Users.Onboard;

public class OnboardHandler : CommandHandlerBase<OnboardRequest, OnboardResponse>
{
    private readonly IIdentityManager _identityManager;
    private readonly IDomainEventFactory _domainEventFactory;
    private readonly IIdentityAccessor _identityAccessor;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly AppDbContext _appDbContext;
    private readonly IUserService _userService;
    private readonly IStringLocalizer<Resources.Resources.Onboard.Onboard> _localizer;

    public OnboardHandler(
        IIdentityManager identityManager,
        IDomainEventFactory domainEventFactory,
        IIdentityAccessor identityAccessor,
        IDateTimeProvider dateTimeProvider,
        AppDbContext appDbContext,
        IUserService userService,
        IStringLocalizer<Resources.Resources.Onboard.Onboard> localizer
    )
    {
        _identityManager = identityManager;
        _domainEventFactory = domainEventFactory;
        _identityAccessor = identityAccessor;
        _dateTimeProvider = dateTimeProvider;
        _appDbContext = appDbContext;
        _userService = userService;
        _localizer = localizer;
    }

    protected override async Task<OnboardResponse> HandleImpl(
        OnboardRequest request,
        CancellationToken cancellationToken
    )
    {
        var currentIdentityId = _identityAccessor.RequireCurrent();
        var existingUser = await _appDbContext.Users.FirstOrDefaultAsync(
            x => x.IdentityId == currentIdentityId,
            cancellationToken
        );
        
        if (existingUser != null)
        {
            throw ValidationException(
                _localizer[nameof(Resources.Resources.Onboard.Onboard_en.user_already_exists)]
            );
        }
        
        var (email, isVerified) = await _identityManager.GetEmail(currentIdentityId, cancellationToken);
        
        if (!isVerified)
        {
            throw ValidationException(
                _localizer[nameof(Resources.Resources.Onboard.Onboard_en.email_not_verified)]
            );
        }
        var username = Username.Create(request.Username);

        var isUsernameAvailable = await _userService.IsUsernameAvailable(username, cancellationToken);
        if (!isUsernameAvailable)
        {
            throw ValidationException(
                nameof(OnboardRequest.Username),
                _localizer[nameof(Resources.Resources.Onboard.Onboard_en.username_in_use)]
            );
        }

        var isEmailAvailable = await _userService.IsEmailAvailable(email, cancellationToken);
        if (!isEmailAvailable)
        {
            throw ValidationException(
                _localizer[nameof(Resources.Resources.Onboard.Onboard_en.email_in_use)]
            );
        }

        var user = User.Create(
            currentIdentityId,
            Username.Create(request.Username),
            email,
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
            EmailNotValidException _ => ValidationException(
                _localizer[nameof(Resources.Resources.Onboard.Onboard_en.email_not_valid)]
            ),
            UsernameEmptyException _ => ValidationException(
                nameof(OnboardRequest.Username),
                _localizer[nameof(Resources.Resources.Onboard.Onboard_en.username_empty)]
            ),
            UsernameInvalidLengthException usernameLengthNotValidException => ValidationException(
                nameof(OnboardRequest.Username),
                string.Format(_localizer[nameof(Resources.Resources.Onboard.Onboard_en.username_length_not_valid)], usernameLengthNotValidException.MinLength, usernameLengthNotValidException.MaxLength)
            ),
            UsernameInvalidCharactersException usernameCharactersNotValidException => ValidationException(
                nameof(OnboardRequest.Username),
                string.Format(_localizer[nameof(Resources.Resources.Onboard.Onboard_en.username_characters_not_valid)], usernameCharactersNotValidException.ValidCharacters)
            ),
            _ => base.MapExceptionToValidationException(exception)
        };
    }
}