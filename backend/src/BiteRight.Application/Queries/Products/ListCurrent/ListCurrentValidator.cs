// # ==============================================================================
// # Solution: BiteRight
// # File: ListCurrentValidator.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using FluentValidation;
using Microsoft.Extensions.Localization;

#endregion

namespace BiteRight.Application.Queries.Products.ListCurrent;

public class ListCurrentValidator : AbstractValidator<ListCurrentRequest>
{
    public ListCurrentValidator(
        IStringLocalizer<Resources.Resources.Products.Products> productsLocalizer
    )
    {
        RuleFor(x => x.SortingStrategy)
            .IsInEnum()
            .WithMessage(_ =>
                productsLocalizer[nameof(Resources.Resources.Products.Products.sorting_strategy_invalid)]);
    }
}