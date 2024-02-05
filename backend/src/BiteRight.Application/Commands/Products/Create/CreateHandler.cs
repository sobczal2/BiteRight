using BiteRight.Application.Common;
using BiteRight.Application.Common.Exceptions;
using BiteRight.Application.Dtos.Products;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Abstracts.Repositories;
using BiteRight.Domain.Products;
using BiteRight.Domain.Products.Exceptions;
using BiteRight.Infrastructure.Database;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Name = BiteRight.Domain.Products.Name;

namespace BiteRight.Application.Commands.Products.Create;

public class CreateHandler : HandlerBase<CreateRequest, CreateResponse>
{
    private readonly IDomainEventFactory _domainEventFactory;
    private readonly IIdentityProvider _identityProvider;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUserRepository _userRepository;
    private readonly ICurrencyRepository _currencyRepository;
    private readonly IProductRepository _productRepository;
    private readonly AppDbContext _dbContext;
    private readonly IStringLocalizer<Resources.Resources.Products.Products> _productsLocalizer;
    private readonly IStringLocalizer<Resources.Resources.Currencies.Currencies> _currenciesLocalizer;

    public CreateHandler(
        IDomainEventFactory domainEventFactory,
        IIdentityProvider identityProvider,
        IDateTimeProvider dateTimeProvider,
        IUserRepository userRepository,
        ICurrencyRepository currencyRepository,
        IProductRepository productRepository,
        AppDbContext dbContext,
        IStringLocalizer<Resources.Resources.Products.Products> productsLocalizer,
        IStringLocalizer<Resources.Resources.Currencies.Currencies> currenciesLocalizer
    )
    {
        _domainEventFactory = domainEventFactory;
        _identityProvider = identityProvider;
        _dateTimeProvider = dateTimeProvider;
        _userRepository = userRepository;
        _currencyRepository = currencyRepository;
        _productRepository = productRepository;
        _dbContext = dbContext;
        _productsLocalizer = productsLocalizer;
        _currenciesLocalizer = currenciesLocalizer;
    }

    protected override async Task<CreateResponse> HandleImpl(
        CreateRequest request,
        CancellationToken cancellationToken
    )
    {
        var currentIdentityId = _identityProvider.RequireCurrent();
        var user = await _userRepository.FindByIdentityId(currentIdentityId, cancellationToken)
                   ?? throw new InternalErrorException();

        var name = Name.Create(request.Name);
        var description = Description.Create(request.Description);

        Price? price = null;
        if (request.CurrencyId is not null && request.Price.HasValue)
        {
            var currency = await _currencyRepository.FindById(request.CurrencyId, cancellationToken)
                           ?? throw ValidationException(
                               _currenciesLocalizer[
                                   nameof(Resources.Resources.Currencies.Currencies.currency_not_found)]);
            price = Price.Create(request.Price.Value, currency);
        }

        ExpirationDate expirationDate;

        switch (request.ExpirationDateKind)
        {
            case ExpirationDateKindDto.Infinite:
                expirationDate = ExpirationDate.CreateInfinite();
                break;
            case ExpirationDateKindDto.BestBefore:
                expirationDate = ExpirationDate.CreateBestBefore(request.ExpirationDate!.Value);
                break;
            case ExpirationDateKindDto.UseBy:
                expirationDate = ExpirationDate.CreateUseBy(request.ExpirationDate!.Value);
                break;
            default:
                expirationDate = ExpirationDate.CreateUnknown(request.ExpirationDate);
                break;
        }

        var addedDateTime = AddedDateTime.Create(_dateTimeProvider.UtcNow);
        var usage = Usage.CreateFull();
        var product = Product.Create(
            name,
            description,
            price,
            expirationDate,
            addedDateTime,
            usage,
            user,
            _domainEventFactory
        );

        _productRepository.Add(product);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new CreateResponse(product.Id);
    }

    protected override ValidationException MapExceptionToValidationException(
        Exception exception
    )
    {
        return exception switch
        {
            NameEmptyException => ValidationException(
                nameof(CreateRequest.Name),
                _productsLocalizer[nameof(Resources.Resources.Products.Products.name_empty)]
            ),
            NameInvalidLengthException e => ValidationException(
                nameof(CreateRequest.Name),
                string.Format(
                    _productsLocalizer[nameof(Resources.Resources.Products.Products.name_length_not_valid)],
                    e.MinLength,
                    e.MaxLength
                )
            ),
            NameInvalidCharactersException e => ValidationException(
                nameof(CreateRequest.Name),
                string.Format(
                    _productsLocalizer[nameof(Resources.Resources.Products.Products.name_characters_not_valid)],
                    e.ValidCharacters
                )
            ),
            DescriptionInvalidLengthException e => ValidationException(
                nameof(CreateRequest.Description),
                string.Format(
                    _productsLocalizer[nameof(Resources.Resources.Products.Products.description_length_not_valid)],
                    e.MinLength,
                    e.MaxLength
                )
            ),
            DescriptionInvalidCharactersException e => ValidationException(
                nameof(CreateRequest.Description),
                string.Format(
                    _productsLocalizer[nameof(Resources.Resources.Products.Products.description_characters_not_valid)],
                    e.ValidCharacters
                )
            ),
            PriceInvalidValueException e => ValidationException(
                nameof(CreateRequest.Price),
                string.Format(
                    _productsLocalizer[nameof(Resources.Resources.Products.Products.price_not_valid)],
                    e.MinValue,
                    e.MaxValue
                )
            ),
            ExpirationDateInfiniteValueException => ValidationException(
                nameof(CreateRequest.ExpirationDateKind),
                _productsLocalizer[nameof(Resources.Resources.Products.Products.expiration_date_infinite)]
            ),
        };
    }
}