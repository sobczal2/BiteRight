// # ==============================================================================
// # Solution: BiteRight
// # File: EditHandler.cs
// # Author: Łukasz Sobczak
// # Created: 26-02-2024
// # ==============================================================================

#region

using System;
using System.Threading;
using System.Threading.Tasks;
using BiteRight.Application.Common;
using BiteRight.Application.Dtos.Products;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Abstracts.Repositories;
using BiteRight.Domain.Products;
using BiteRight.Domain.Products.Exceptions;
using BiteRight.Infrastructure.Database;
using FluentValidation;
using Microsoft.Extensions.Localization;

#endregion

namespace BiteRight.Application.Commands.Products.Edit;

public class EditHandler : CommandHandlerBase<EditRequest, EditResponse>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IStringLocalizer<Resources.Resources.Currencies.Currencies> _currenciesLocalizer;
    private readonly ICurrencyRepository _currencyRepository;
    private readonly IIdentityProvider _identityProvider;
    private readonly ILanguageProvider _languageProvider;
    private readonly IProductRepository _productRepository;
    private readonly IStringLocalizer<Resources.Resources.Products.Products> _productsLocalizer;
    private readonly IUnitRepository _unitRepository;
    private readonly IStringLocalizer<Resources.Resources.Units.Units> _unitsLocalizer;

    public EditHandler(
        IIdentityProvider identityProvider,
        ILanguageProvider languageProvider,
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        ICurrencyRepository currencyRepository,
        IUnitRepository unitRepository,
        IStringLocalizer<Resources.Resources.Products.Products> productsLocalizer,
        IStringLocalizer<Resources.Resources.Currencies.Currencies> currenciesLocalizer,
        IStringLocalizer<Resources.Resources.Units.Units> unitsLocalizer,
        AppDbContext appDbContext
    )
        : base(appDbContext)
    {
        _identityProvider = identityProvider;
        _languageProvider = languageProvider;
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _currencyRepository = currencyRepository;
        _unitRepository = unitRepository;
        _productsLocalizer = productsLocalizer;
        _currenciesLocalizer = currenciesLocalizer;
        _unitsLocalizer = unitsLocalizer;
    }

    protected override async Task<EditResponse> HandleImpl(
        EditRequest request,
        CancellationToken cancellationToken
    )
    {
        var user = await _identityProvider.RequireCurrentUser(cancellationToken);

        var existingProduct = await _productRepository.FindById(request.ProductId, cancellationToken);

        if (existingProduct is null || !Equals(existingProduct.CreatedById, user.Id))
            throw ValidationException(
                nameof(request.ProductId),
                _productsLocalizer[nameof(Resources.Resources.Products.Products.product_not_found)]
            );

        existingProduct.UpdateName(request.Name);
        existingProduct.UpdateDescription(request.Description);


        if (request.PriceCurrencyId is not null && request.PriceValue.HasValue)
        {
            var currency = await _currencyRepository.FindById(request.PriceCurrencyId, cancellationToken);
            if (currency is null)
                throw ValidationException(
                    nameof(EditRequest.PriceCurrencyId),
                    _currenciesLocalizer[
                        nameof(Resources.Resources.Currencies.Currencies.currency_not_found)]);

            existingProduct.UpdatePrice(request.PriceValue.Value, currency.Id);
        }
        else
        {
            existingProduct.ClearPrice();
        }

        var expirationDate = request.ExpirationDateKind switch
        {
            ExpirationDateKindDto.Infinite => ExpirationDate.CreateInfinite(),
            ExpirationDateKindDto.BestBefore => ExpirationDate.CreateBestBefore(request.ExpirationDate!.Value),
            ExpirationDateKindDto.UseBy => ExpirationDate.CreateUseBy(request.ExpirationDate!.Value),
            ExpirationDateKindDto.Unknown => ExpirationDate.CreateUnknown(),
            _ => throw new ArgumentOutOfRangeException()
        };

        existingProduct.UpdateExpirationDate(expirationDate);

        var languageId = await _languageProvider.RequireCurrentId(cancellationToken);

        var category = await _categoryRepository.FindById(request.CategoryId, languageId, cancellationToken)
                       ?? throw ValidationException(
                           nameof(EditRequest.CategoryId),
                           _productsLocalizer[
                               nameof(Resources.Resources.Categories.Categories.category_not_found)]
                       );

        existingProduct.UpdateCategory(category.Id);

        var amountUnit = await _unitRepository.FindById(request.AmountUnitId, languageId, cancellationToken)
                         ?? throw ValidationException(
                             nameof(EditRequest.AmountUnitId),
                             _unitsLocalizer[
                                 nameof(Resources.Resources.Units.Units.unit_not_found)]
                         );

        existingProduct.UpdateAmount(
            request.AmountCurrentValue,
            request.AmountMaxValue,
            amountUnit.Id
        );

        return new EditResponse();
    }

    protected override ValidationException MapExceptionToValidationException(
        Exception exception
    )
    {
        return exception switch
        {
            NameEmptyException => ValidationException(
                nameof(EditRequest.Name),
                _productsLocalizer[nameof(Resources.Resources.Products.Products.name_empty)]
            ),
            NameInvalidLengthException e => ValidationException(
                nameof(EditRequest.Name),
                string.Format(
                    _productsLocalizer[nameof(Resources.Resources.Products.Products.name_length_not_valid)],
                    e.MinLength,
                    e.MaxLength
                )
            ),
            NameInvalidCharactersException e => ValidationException(
                nameof(EditRequest.Name),
                string.Format(
                    _productsLocalizer[nameof(Resources.Resources.Products.Products.name_characters_not_valid)],
                    e.ValidCharacters
                )
            ),
            DescriptionInvalidLengthException e => ValidationException(
                nameof(EditRequest.Description),
                string.Format(
                    _productsLocalizer[nameof(Resources.Resources.Products.Products.description_length_not_valid)],
                    e.MaxLength
                )
            ),
            DescriptionInvalidCharactersException e => ValidationException(
                nameof(EditRequest.Description),
                string.Format(
                    _productsLocalizer[nameof(Resources.Resources.Products.Products.description_characters_not_valid)],
                    e.ValidCharacters
                )
            ),
            PriceInvalidValueException e => ValidationException(
                nameof(EditRequest.PriceValue),
                string.Format(
                    _productsLocalizer[nameof(Resources.Resources.Products.Products.price_not_valid)],
                    e.MinValue,
                    e.MaxValue
                )
            ),
            ExpirationDateUnknownValueException => ValidationException(
                nameof(EditRequest.ExpirationDate),
                _productsLocalizer[nameof(Resources.Resources.Products.Products.expiration_date_invalid_for_unknown)]
            ),
            ExpirationDateInfiniteValueException => ValidationException(
                nameof(EditRequest.ExpirationDate),
                _productsLocalizer[nameof(Resources.Resources.Products.Products.expiration_date_invalid_for_infinite)]
            ),
            ExpirationDateBestBeforeValueException => ValidationException(
                nameof(EditRequest.ExpirationDate),
                _productsLocalizer[
                    nameof(Resources.Resources.Products.Products.expiration_date_invalid_for_best_before)]
            ),
            ExpirationDateUseByValueException => ValidationException(
                nameof(EditRequest.ExpirationDate),
                _productsLocalizer[nameof(Resources.Resources.Products.Products.expiration_date_invalid_for_use_by)]
            ),
            AmountCurrentValueInvalidValueException e => ValidationException(
                nameof(EditRequest.AmountMaxValue),
                string.Format(
                    _productsLocalizer
                        [nameof(Resources.Resources.Products.Products.amount_current_value_invalid_value)],
                    e.MinValue,
                    e.MaxValue
                )
            ),
            AmountMaxValueInvalidValueException e => ValidationException(
                nameof(EditRequest.AmountMaxValue),
                string.Format(
                    _productsLocalizer[nameof(Resources.Resources.Products.Products.amount_max_value_invalid_value)],
                    e.MinValue,
                    e.MaxValue
                )
            ),
            AmountCurrentValueGreaterThanMaxValueException => ValidationException(
                nameof(EditRequest.AmountMaxValue),
                _productsLocalizer[
                    nameof(Resources.Resources.Products.Products.amount_current_value_greater_than_max_value)]
            ),

            _ => base.MapExceptionToValidationException(exception)
        };
    }
}