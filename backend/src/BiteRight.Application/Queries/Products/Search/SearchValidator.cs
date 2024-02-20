// # ==============================================================================
// # Solution: BiteRight
// # File: ListValidator.cs
// # Author: Łukasz Sobczak
// # Created: 16-02-2024
// # ==============================================================================

using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BiteRight.Application.Queries.Products.Search;

public class SearchValidator : AbstractValidator<SearchRequest>
{
    public SearchValidator(
        IStringLocalizer<Resources.Resources.Common.Common> commonLocalizer,
        IStringLocalizer<Resources.Resources.Categories.Categories> categoriesLocalizer,
        IStringLocalizer<Resources.Resources.Products.Products> productsLocalizer
    )
    {
        const int maxQueryLength = 64;
        RuleFor(x => x.Query)
            .MaximumLength(maxQueryLength)
            .WithMessage(string.Format(
                commonLocalizer[nameof(Resources.Resources.Common.Common.query_too_long)], maxQueryLength));

        RuleFor(x => x.FilteringParams)
            .NotNull()
            .WithMessage(commonLocalizer[nameof(Resources.Resources.Common.Common.filtering_params_null)]);

        RuleFor(x => x.FilteringParams)
            .SetValidator(new FilteringParamsValidator(categoriesLocalizer));

        RuleFor(x => x.SortingStrategy)
            .IsInEnum()
            .WithMessage(productsLocalizer[nameof(Resources.Resources.Products.Products.sorting_strategy_invalid)]);
    }
}

public class FilteringParamsValidator : AbstractValidator<FilteringParams>
{
    public FilteringParamsValidator(
        IStringLocalizer<Resources.Resources.Categories.Categories> categoriesLocalizer
    )
    {
        const int maxCategoryIds = 32;
        RuleFor(x => x.CategoryIds)
            .Must(x => x.Count <= maxCategoryIds)
            .WithMessage(string.Format(
                categoriesLocalizer[nameof(Resources.Resources.Categories.Categories.category_ids_too_long)],
                maxCategoryIds));
    }
}