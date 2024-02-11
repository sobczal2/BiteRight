using BiteRight.Application.Common;
using BiteRight.Application.Common.Exceptions;
using BiteRight.Application.Dtos.Products;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Abstracts.Repositories;
using BiteRight.Domain.Products;
using BiteRight.Domain.Products.Exceptions;
using BiteRight.Infrastructure.Database;
using BiteRight.Resources.Resources.Categories;
using BiteRight.Resources.Resources.Currencies;
using BiteRight.Resources.Resources.Units;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Name = BiteRight.Domain.Products.Name;

namespace BiteRight.Application.Commands.Products.Create;

public class CreateHandler : CommandHandlerBase<CreateRequest, CreateResponse>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IStringLocalizer<Currencies> _currenciesLocalizer;
    private readonly ICurrencyRepository _currencyRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IIdentityProvider _identityProvider;
    private readonly ILanguageProvider _languageProvider;
    private readonly IProductRepository _productRepository;
    private readonly IStringLocalizer<Resources.Resources.Products.Products> _productsLocalizer;
    private readonly IUnitRepository _unitRepository;
    private readonly IStringLocalizer<Units> _unitsLocalizer;
    private readonly IUserRepository _userRepository;

    public CreateHandler(
        IIdentityProvider identityProvider,
        IDateTimeProvider dateTimeProvider,
        IUserRepository userRepository,
        ICurrencyRepository currencyRepository,
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        IUnitRepository unitRepository,
        AppDbContext appDbContext,
        ILanguageProvider languageProvider,
        IStringLocalizer<Resources.Resources.Products.Products> productsLocalizer,
        IStringLocalizer<Currencies> currenciesLocalizer,
        IStringLocalizer<Units> unitsLocalizer
    )
        : base(appDbContext)
    {
        _identityProvider = identityProvider;
        _dateTimeProvider = dateTimeProvider;
        _userRepository = userRepository;
        _currencyRepository = currencyRepository;
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _unitRepository = unitRepository;
        _languageProvider = languageProvider;
        _productsLocalizer = productsLocalizer;
        _currenciesLocalizer = currenciesLocalizer;
        _unitsLocalizer = unitsLocalizer;
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
                                   nameof(Currencies.currency_not_found)]);
            price = Price.Create(request.Price.Value, currency);
        }

        var expirationDate = request.ExpirationDateKind switch
        {
            ExpirationDateKindDto.Infinite => ExpirationDate.CreateInfinite(),
            ExpirationDateKindDto.BestBefore => ExpirationDate.CreateBestBefore(request.ExpirationDate!.Value),
            ExpirationDateKindDto.UseBy => ExpirationDate.CreateUseBy(request.ExpirationDate!.Value),
            ExpirationDateKindDto.Unknown => ExpirationDate.CreateUnknown(),
            _ => throw new ArgumentOutOfRangeException()
        };

        var languageId = await _languageProvider.RequireCurrentId(cancellationToken);

        var category = await _categoryRepository.FindById(request.CategoryId, languageId, cancellationToken)
                       ?? throw ValidationException(
                           _productsLocalizer[
                               nameof(Categories.category_not_found)]
                       );

        var amountUnit = await _unitRepository.FindById(request.AmountUnitId, languageId, cancellationToken)
                         ?? throw ValidationException(
                             _unitsLocalizer[
                                 nameof(Units.unit_not_found)]
                         );

        var amount = Amount.CreateFull(amountUnit.Id, request.MaximumAmountValue);

        var product = Product.Create(
            name,
            description,
            price,
            expirationDate,
            category.Id,
            user.Id,
            amount,
            _dateTimeProvider.UtcNow
        );

        _productRepository.Add(product);

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
            AmountCurrentValueLessThanZeroException => ValidationException(
                nameof(CreateRequest.MaximumAmountValue),
                _productsLocalizer[nameof(Resources.Resources.Products.Products.amount_current_value_less_than_zero)]
            ),
            AmountMaxValueLessThanZeroException => ValidationException(
                nameof(CreateRequest.MaximumAmountValue),
                _productsLocalizer[nameof(Resources.Resources.Products.Products.amount_max_value_less_than_zero)]
            ),
            AmountCurrentValueGreaterThanMaxValueException => ValidationException(
                nameof(CreateRequest.MaximumAmountValue),
                _productsLocalizer[
                    nameof(Resources.Resources.Products.Products.amount_current_value_greater_than_max_value)]
            ),

            _ => base.MapExceptionToValidationException(exception)
        };
    }
}