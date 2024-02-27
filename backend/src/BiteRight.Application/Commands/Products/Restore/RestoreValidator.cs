// # ==============================================================================
// # Solution: BiteRight
// # File: RestoreValidator.cs
// # Author: Łukasz Sobczak
// # Created: 15-02-2024
// # ==============================================================================

#region

using FluentValidation;

#endregion

namespace BiteRight.Application.Commands.Products.Restore;

public class RestoreValidator : AbstractValidator<RestoreRequest>
{
    public RestoreValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage(nameof(Resources.Resources.Products.Products.product_id_empty));
    }
}