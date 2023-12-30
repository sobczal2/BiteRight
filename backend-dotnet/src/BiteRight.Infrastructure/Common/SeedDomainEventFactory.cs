using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Categories;
using BiteRight.Domain.Categories.Events;
using BiteRight.Domain.Countries;
using BiteRight.Domain.Countries.Events;
using BiteRight.Domain.Languages;
using BiteRight.Domain.Languages.Events;
using BiteRight.Domain.Users;
using BiteRight.Domain.Users.Events;

namespace BiteRight.Infrastructure.Common;

public class SeedDomainEventFactory : IDomainEventFactory
{
    public UserCreatedEvent CreateUserCreatedEvent(
        IdentityId identityId
    )
    {
        return new UserCreatedEvent(
            DateTime.UtcNow,
            Guid.NewGuid(),
            identityId
        );
    }

    public LanguageCreatedEvent CreateLanguageCreatedEvent(
        LanguageId languageId
    )
    {
        return new LanguageCreatedEvent(
            DateTime.UtcNow,
            Guid.NewGuid(),
            languageId
        );
    }

    public CountryCreatedEvent CreateCountryCreatedEvent(
        CountryId countryId
    )
    {
        return new CountryCreatedEvent(
            DateTime.UtcNow,
            Guid.NewGuid(),
            countryId
        );
    }

    public CategoryCreatedEvent CreateCategoryCreatedEvent(
        CategoryId categoryId
    )
    {
        return new CategoryCreatedEvent(
            DateTime.UtcNow,
            Guid.NewGuid(),
            categoryId
        );
    }
}