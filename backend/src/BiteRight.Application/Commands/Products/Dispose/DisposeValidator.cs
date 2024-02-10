using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BiteRight.Application.Commands.Products.Dispose;

public class DisposeValidator : AbstractValidator<DisposeRequest>
{
    public DisposeValidator(
        IStringLocalizer<Resources.Resources.Products.Products> productLocalizer
    )
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage(productLocalizer[nameof(Resources.Resources.Products.Products.product_id_empty)]);
    }
}