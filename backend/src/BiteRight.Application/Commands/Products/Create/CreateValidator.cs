// # ==============================================================================
// # Solution: BiteRight
// # File: CreateValidator.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using BiteRight.Application.Dtos.Products;
using FluentValidation;
using Microsoft.Extensions.Localization;

#endregion

namespace BiteRight.Application.Commands.Products.Create;

public class CreateValidator : AbstractValidator<CreateRequest>
{
    public CreateValidator(
        // ReSharper disable once SuggestBaseTypeForParameterInConstructor
        IStringLocalizer<Resources.Resources.Products.Products> productsLocalizer,
        // ReSharper disable once SuggestBaseTypeForParameterInConstructor
        IStringLocalizer<Resources.Resources.Currencies.Currencies> currenciesLocalizer,
        // ReSharper disable once SuggestBaseTypeForParameterInConstructor
        IStringLocalizer<Resources.Resources.Categories.Categories> categoriesLocalizer
    )
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(_ => productsLocalizer[nameof(Resources.Resources.Products.Products.name_empty)]);

        RuleFor(x => x.Description)
            .NotNull()
            .WithMessage(_ => productsLocalizer[nameof(Resources.Resources.Products.Products.description_null)]);

        RuleFor(x => x.PriceValue)
            .NotNull()
            .When(x => x.PriceValue.HasValue)
            .WithMessage(_ => productsLocalizer[nameof(Resources.Resources.Products.Products.price_empty)]);

        RuleFor(x => x.PriceCurrencyId)
            .NotEmpty()
            .When(x => x.PriceValue.HasValue)
            .WithMessage(_ => currenciesLocalizer[nameof(Resources.Resources.Currencies.Currencies.currency_id_empty)]);

        RuleFor(x => x.ExpirationDate)
            .NotNull()
            .When(x => x.ExpirationDateKind != ExpirationDateKindDto.Infinite &&
                       x.ExpirationDateKind != ExpirationDateKindDto.Unknown)
            .WithMessage(_ => productsLocalizer[nameof(Resources.Resources.Products.Products.expiration_date_empty)]);

        RuleFor(x => x.ExpirationDate)
            .Null()
            .When(x => x.ExpirationDateKind is ExpirationDateKindDto.Infinite or ExpirationDateKindDto.Unknown)
            .WithMessage(_ =>
                productsLocalizer[nameof(Resources.Resources.Products.Products.expiration_date_not_null)]);

        RuleFor(x => x.ExpirationDateKind)
            .IsInEnum()
            .WithMessage(_ =>
                productsLocalizer[nameof(Resources.Resources.Products.Products.expiration_date_kind_invalid)]);

        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .WithMessage(_ => categoriesLocalizer[nameof(Resources.Resources.Categories.Categories.category_id_empty)]);

        RuleFor(x => x.AmountUnitId)
            .NotEmpty()
            .WithMessage(_ => productsLocalizer[nameof(Resources.Resources.Products.Products.amount_unit_id_empty)]);
    }
}