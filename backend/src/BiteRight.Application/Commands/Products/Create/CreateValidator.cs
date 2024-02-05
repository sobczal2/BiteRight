using BiteRight.Application.Dtos.Products;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BiteRight.Application.Commands.Products.Create;

public class CreateValidator : AbstractValidator<CreateRequest>
{
    public CreateValidator(
        IStringLocalizer<Resources.Resources.Products.Products> productsLocalizer,
        IStringLocalizer<Resources.Resources.Currencies.Currencies> currenciesLocalizer,
        IStringLocalizer<Resources.Resources.Categories.Categories> categoriesLocalizer
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
            .WithMessage(_ => currenciesLocalizer[nameof(Resources.Resources.Currencies.Currencies.currency_empty)]);

        RuleFor(x => x.ExpirationDate)
            .NotNull()
            .When(x => x.ExpirationDateKind != ExpirationDateKindDto.Infinite &&
                       x.ExpirationDateKind != ExpirationDateKindDto.Unknown)
            .WithMessage(_ => currenciesLocalizer[nameof(Resources.Resources.Currencies.Currencies.expiration_date_empty)]);
        
        RuleFor(x => x.ExpirationDateKind)
            .IsInEnum()
            .WithMessage(_ => productsLocalizer[nameof(Resources.Resources.Products.Products.expiration_date_kind_invalid)]);
        
        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .WithMessage(_ => categoriesLocalizer[nameof(Resources.Resources.Categories.Categories.category_empty)]);
    }
}