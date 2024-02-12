// # ==============================================================================
// # Solution: BiteRight
// # File: CreateValidator.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using BiteRight.Application.Dtos.Products;
using BiteRight.Resources.Resources.Categories;
using BiteRight.Resources.Resources.Currencies;
using FluentValidation;
using Microsoft.Extensions.Localization;

#endregion

namespace BiteRight.Application.Commands.Products.Create;

public class CreateValidator : AbstractValidator<CreateRequest>
{
    public CreateValidator(
        IStringLocalizer<Resources.Resources.Products.Products> productsLocalizer,
        IStringLocalizer<Currencies> currenciesLocalizer,
        IStringLocalizer<Categories> categoriesLocalizer
    )
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(_ => productsLocalizer[nameof(Resources.Resources.Products.Products.name_empty)]);

        RuleFor(x => x.Description)
            .NotNull()
            .WithMessage(_ => productsLocalizer[nameof(Resources.Resources.Products.Products.description_null)]);

        RuleFor(x => x.Price)
            .NotNull()
            .When(x => x.Price.HasValue)
            .WithMessage(_ => productsLocalizer[nameof(Resources.Resources.Products.Products.price_empty)]);

        RuleFor(x => x.CurrencyId)
            .NotEmpty()
            .When(x => x.Price.HasValue)
            .WithMessage(_ => currenciesLocalizer[nameof(Currencies.currency_id_empty)]);

        RuleFor(x => x.ExpirationDate)
            .NotNull()
            .When(x => x.ExpirationDateKind != ExpirationDateKindDto.Infinite &&
                       x.ExpirationDateKind != ExpirationDateKindDto.Unknown)
            .WithMessage(_ => productsLocalizer[nameof(Resources.Resources.Products.Products.expiration_date_empty)]);

        RuleFor(x => x.ExpirationDate)
            .Null()
            .When(x => x.ExpirationDateKind == ExpirationDateKindDto.Infinite ||
                       x.ExpirationDateKind == ExpirationDateKindDto.Unknown)
            .WithMessage(_ =>
                productsLocalizer[nameof(Resources.Resources.Products.Products.expiration_date_not_null)]);

        RuleFor(x => x.ExpirationDateKind)
            .IsInEnum()
            .WithMessage(_ =>
                productsLocalizer[nameof(Resources.Resources.Products.Products.expiration_date_kind_invalid)]);

        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .WithMessage(_ => categoriesLocalizer[nameof(Categories.category_id_empty)]);
    }
}