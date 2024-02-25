// # ==============================================================================
// # Solution: BiteRight
// # File: GetDetailsValidator.cs
// # Author: Łukasz Sobczak
// # Created: 25-02-2024
// # ==============================================================================

using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BiteRight.Application.Queries.Products.GetDetails;

public class GetDetailsValidator : AbstractValidator<GetDetailsRequest>
{
    public GetDetailsValidator(
        IStringLocalizer<Resources.Resources.Products.Products> productsLocalizer
        )
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage(productsLocalizer[nameof(Resources.Resources.Products.Products.product_id_empty)]);
    }
}