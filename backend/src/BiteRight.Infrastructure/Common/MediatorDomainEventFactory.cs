using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Categories;
using BiteRight.Domain.Categories.Events;
using BiteRight.Domain.Countries;
using BiteRight.Domain.Countries.Events;
using BiteRight.Domain.Currencies;
using BiteRight.Domain.Currencies.Events;
using BiteRight.Domain.Languages;
using BiteRight.Domain.Languages.Events;
using BiteRight.Domain.Product;
using BiteRight.Domain.Product.Events;
using BiteRight.Domain.Users;
using BiteRight.Domain.Users.Events;

namespace BiteRight.Infrastructure.Common;

public class MediatorDomainEventFactory : IDomainEventFactory
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ICorrelationIdAccessor _correlationIdAccessor;

    public MediatorDomainEventFactory(
        IDateTimeProvider dateTimeProvider,
        ICorrelationIdAccessor correlationIdAccessor
    )
    {
        _dateTimeProvider = dateTimeProvider;
        _correlationIdAccessor = correlationIdAccessor;
    }

    public UserCreatedEvent CreateUserCreatedEvent(
        IdentityId identityId
    )
    {
        return new UserCreatedEvent(
            _dateTimeProvider.UtcNow,
            _correlationIdAccessor.CorrelationId,
            identityId
        );
    }

    public LanguageCreatedEvent CreateLanguageCreatedEvent(
        LanguageId languageId
    )
    {
        return new LanguageCreatedEvent(
            _dateTimeProvider.UtcNow,
            _correlationIdAccessor.CorrelationId,
            languageId
        );
    }

    public CountryCreatedEvent CreateCountryCreatedEvent(
        CountryId countryId
    )
    {
        return new CountryCreatedEvent(
            _dateTimeProvider.UtcNow,
            _correlationIdAccessor.CorrelationId,
            countryId
        );
    }

    public CategoryCreatedEvent CreateCategoryCreatedEvent(
        CategoryId categoryId
    )
    {
        return new CategoryCreatedEvent(
            _dateTimeProvider.UtcNow,
            _correlationIdAccessor.CorrelationId,
            categoryId
        );
    }

    public CurrencyCreatedEvent CreateCurrencyCreatedEvent(
        CurrencyId currencyId
    )
    {
        return new CurrencyCreatedEvent(
            _dateTimeProvider.UtcNow,
            _correlationIdAccessor.CorrelationId,
            currencyId
        );
    }

    public ProductCreatedEvent CreateProductCreatedEvent(
        ProductId productId
    )
    {
        return new ProductCreatedEvent(
            _dateTimeProvider.UtcNow,
            _correlationIdAccessor.CorrelationId,
            productId
        );
    }
}