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
    
    CategoryCreatedEvent CreateCategoryCreatedEvent(
        CategoryId categoryId
    );
    
    CurrencyCreatedEvent CreateCurrencyCreatedEvent(
        CurrencyId currencyId
    );
    
    ProductCreatedEvent CreateProductCreatedEvent(
        ProductId productId
    );
    
    UserProfileUpdatedEvent CreateUserProfileUpdatedEvent(
        IdentityId identityId
    );
}