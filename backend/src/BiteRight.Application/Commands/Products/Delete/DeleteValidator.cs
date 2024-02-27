// # ==============================================================================
// # Solution: BiteRight
// # File: DeleteValidator.cs
// # Author: Łukasz Sobczak
// # Created: 27-02-2024
// # ==============================================================================

using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BiteRight.Application.Commands.Products.Delete;

public class DeleteValidator : AbstractValidator<DeleteRequest>
{
    public DeleteValidator(
        IStringLocalizer<Resources.Resources.Products.Products> productLocalizer
    )
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage(productLocalizer[nameof(Resources.Resources.Products.Products.product_id_empty)]);
    }
}