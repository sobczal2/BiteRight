// # ==============================================================================
// # Solution: BiteRight
// # File: ChangeAmountValidator.cs
// # Author: Łukasz Sobczak
// # Created: 17-02-2024
// # ==============================================================================

using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BiteRight.Application.Commands.Products.ChangeAmount;

public class ChangeAmountValidator : AbstractValidator<ChangeAmountRequest>
{
    public ChangeAmountValidator(
        IStringLocalizer<Resources.Resources.Products.Products> productsLocalizer
    )
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage(productsLocalizer[Resources.Resources.Products.Products.product_id_empty]);
    }
}