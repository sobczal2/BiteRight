using BiteRight.Domain.Common;
using BiteRight.Domain.Countries;
using BiteRight.Domain.Countries.Events;
using BiteRight.Domain.Languages;
using BiteRight.Domain.Languages.Events;
using BiteRight.Domain.Users;
using BiteRight.Domain.Users.Events;

namespace BiteRight.Domain.Abstracts.Common;

public interface IDomainEventFactory
{
    UserCreatedEvent CreateUserCreatedEvent(
        IdentityId identityId
    );

    LanguageCreatedEvent CreateLanguageCreatedEvent(
        LanguageId languageId
    );

    CountryCreatedEvent CreateCountryCreatedEvent(
        CountryId countryId
    );
}